using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Projections;
using DotSpatial.Data;
using NUnit.Framework;
using System.Xml;
using System.Collections;
using DotSpatial.Topology;
using System.Globalization;
using System.IO;


namespace comacExport
{
    public partial class Form1 : Form
    {
        private string default_prj_in = @"qgis_file\default_input.prj";

        private string default_prj_out = @"qgis_file\default_output.prj";
        private string default_qgs = @"qgis_file\default_project.qgs";

        private string default_support_shp = @"qgis_file\shape_file_default\SUPPORT.shp";
        private string default_lignes_shp = @"qgis_file\shape_file_default\Lignes.shp";

        private string support_folder = @"qgis_file\Support";
        private string style_css = @"qgis_file\style.css";
        private string operator_logo_folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\COMAC Report Tool\Logo\operator";
        private string distributor_logo_folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\COMAC Report Tool\Logo\distributor";

        private string output_folder;
        private string input_folder;
        private string photos_folder_input;

        private string pdf_input_path;
        private string excel_input_path;

        string logoName;

        const double zone_space_distance = 60;

        XmlDocument pcm_doc;
        ProjectionInfo info_input;
        ProjectionInfo info_output;
        Shapefile shapefile;
        PCMReader pcm_reader;



        public Form1()
        {
            InitializeComponent();

            pcm_reader = new PCMReader();

            info_input = ProjectionInfo.Open(default_prj_in);
            info_output = ProjectionInfo.Open(default_prj_out);

            //if (!mapping_attribute.readFileExcel(mapping_file))
            //{
            //    //mapping_data = mapping_attribute.mapping_data;
            //    Application.Exit();
            //}

            loadListLogo();
        }

        public void loadListLogo()
        {
            try
            {
                DirectoryInfo srcFolder = new DirectoryInfo(operator_logo_folder);
                FileInfo[] listFile = srcFolder.GetFiles();
                cbbOperator.Items.Clear();
                if (listFile.Length > 0)
                {
                    foreach (FileInfo newPath in listFile)
                    {
                        string logoName = newPath.Name.Split(new char[] { '.' })[0];
                        cbbOperator.Items.Add(logoName);
                    }
                }
                else
                {
                    MessageBox.Show("Not have logo in list", "Error");
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("Can not load list logo", "Error");
                Application.Exit();
            }
        }

        #region custom windows style
        //drag window
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    System.Drawing.Point pos = new System.Drawing.Point(m.LParam.ToInt32());
                    pos = this.PointToClient(pos);
                    if (pos.Y < 30)
                    {
                        m.Result = (IntPtr)0x02;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MAXIMIZEBOX = 0x00010000;
                var cp = base.CreateParams;
                cp.Style &= ~WS_MAXIMIZEBOX;
                return cp;
            }
        }

        #endregion

        #region load pcm file
        /// <summary>
        /// import pcm file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog pcmOpenFileDialog = new OpenFileDialog())
            {
                pcmOpenFileDialog.Filter = "PCM|*.pcm";
                pcmOpenFileDialog.Title = "Import PCM File";
                pcmOpenFileDialog.Multiselect = false;
                //openFileDialog1.InitialDirectory = lastLogDir ?? Properties.Settings.Default.LogDir;

                if (pcmOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    output_folder = Path.GetDirectoryName(pcmOpenFileDialog.FileName) + "\\Livrable";
                    input_folder = Path.GetDirectoryName(pcmOpenFileDialog.FileName); //chua pcm
                    photos_folder_input = pcmOpenFileDialog.FileName.Split(new char[] { '.' })[0] + "_Photos";
                    pcm_doc = new XmlDocument();
                    try
                    {
                        if (pcm_reader.openFile(pcmOpenFileDialog.FileName))
                        {
                            this.txtOperatingCorrespondent.Text = pcm_reader.operating_corespondent;
                            this.txtStreet.Text = pcm_reader.street;
                            this.txtDescription.Text = pcm_reader.description;
                            this.txtReference.Text = pcm_reader.reference;
                            this.txtInsee_casd1.Text = pcm_reader.insee_cadastre_1;

                            this.txtPhoneNumber.Text = pcm_reader.phone_number;
                            this.txtCommon.Text = pcm_reader.common;
                            this.txtComment.Text = pcm_reader.comment;
                            this.txtNumberEplans.Text = pcm_reader.number_eplans;
                            this.txtInsee_casd2.Text = pcm_reader.insee_cadastre_2;

                            this.txtDirSource.Text = pcmOpenFileDialog.FileName;
                        }

                        this.btnExport.Enabled = true;

                        //selectForm_Commited(null);
                        cbbOperator.Enabled = true;
                        cbbOperator.SelectedIndex = -1;
                    }
                    catch
                    {

                    }

                    return;
                }

            }

        }
        #endregion

        #region create ShapeFile
        public void createSupportFile()
        {
            try
            {
                if (pcm_reader != null)
                {
                    shapefile = Shapefile.OpenFile(default_support_shp);
                    shapefile.Reproject(info_input);
                    //info_input = shapefile.Projection;
                    shapefile.Features.Clear();


                    foreach (var support_attribute in pcm_reader.list_support_attribute)
                    {
                        IFeatureSet fs = new FeatureSet(FeatureType.Point);
                        fs.DataTable.Columns.Add("Test", typeof(string));
                        IFeature feature = fs.AddFeature(new DotSpatial.Topology.Point(
                                           double.Parse(support_attribute.x, CultureInfo.CreateSpecificCulture("fr-FR")),
                                           double.Parse(support_attribute.y, CultureInfo.CreateSpecificCulture("fr-FR"))));
                        //feature.
                        shapefile.Features.Add(feature);
                    }

                    shapefile.Reproject(info_output);//change coordinator

                    //get zone postion
                    foreach (var feature in shapefile.Features.ToList())
                    {
                        var geo = feature.BasicGeometry;
                        var p = geo as Point;
                        double x = p.X;
                        double y = p.Y;

                        if (x < pcm_reader.zone_top_left.X)
                            pcm_reader.zone_top_left.X = x;
                        if (x > pcm_reader.zone_bottom_right.X)
                            pcm_reader.zone_bottom_right.X = x;

                        if (y < pcm_reader.zone_top_left.Y)
                            pcm_reader.zone_top_left.Y = y;
                        if (y > pcm_reader.zone_bottom_right.Y)
                            pcm_reader.zone_bottom_right.Y = y;
                    }
                    //end get zone position


                    shapefile.Attributes.Table = new DataTable();
                    foreach (var support_attribute in pcm_reader.list_support_attribute)
                    {
                        AttributeShapefile.updateAttributetoSupportLayer(ref shapefile, support_attribute);
                    }



                    if (output_folder != null)
                        shapefile.SaveAs(output_folder + "\\Ressources\\SUPPORT.shp", true);
                    else
                        MessageBox.Show("Import pcm file, please!", "Error");
                }
                else
                {
                    MessageBox.Show("Import pcm file, please!", "Error");
                }
            }
            catch
            {
                throw new Exception("Can not create support shp");
            }
        }

        public void createCableFile()
        {
            try
            {
                if (pcm_reader != null)
                {
                    shapefile = Shapefile.OpenFile(default_lignes_shp);
                    shapefile.Reproject(info_input);
                    shapefile.Features.Clear();
                    List<string> list_ext1_ext2_conducteur = new List<string>();
                    foreach (var cable_attribute in pcm_reader.list_cable_attribute)
                    {
                        string ext1 = cable_attribute.ext_1;
                        string ext2 = cable_attribute.ext_2;
                        string ext1_ext2_conducteur = ext1 + "_" + ext2 + "_" + cable_attribute.conducteur;
                        string ext2_ext1_conducteur = ext2 + "_" + ext1 + "_" + cable_attribute.conducteur;
                        if (list_ext1_ext2_conducteur.Contains(ext1_ext2_conducteur) || list_ext1_ext2_conducteur.Contains(ext2_ext1_conducteur))
                            continue;
                        list_ext1_ext2_conducteur.Add(ext1_ext2_conducteur);
                        double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
                        //lấy tọa độ 2 cột
                        foreach (var support_attribute in pcm_reader.list_support_attribute)
                        {
                            if (support_attribute.nom == ext1)
                            {
                                x1 = double.Parse(support_attribute.x, CultureInfo.CreateSpecificCulture("fr-FR"));
                                y1 = double.Parse(support_attribute.y, CultureInfo.CreateSpecificCulture("fr-FR"));
                            }
                            else if (support_attribute.nom == ext2)
                            {
                                x2 = double.Parse(support_attribute.x, CultureInfo.CreateSpecificCulture("fr-FR"));
                                y2 = double.Parse(support_attribute.y, CultureInfo.CreateSpecificCulture("fr-FR"));
                            }
                        }

                        if ((cable_attribute.type == "T" || cable_attribute.type == "TV" || cable_attribute.type == "FO") && cable_attribute.a_poser == "F") //kiểm tra xem đã có dây nào chưa
                        {
                            IFeatureSet fs = new FeatureSet(FeatureType.Line);
                            fs.DataTable.Columns.Add("Test", typeof(string));
                            Coordinate coord1 = new Coordinate(x1, y1);
                            Coordinate coord4 = new Coordinate(x2, y2);
                            DotSpatial.Topology.Point p_coord1 = new DotSpatial.Topology.Point(x1, y1);
                            DotSpatial.Topology.Point p_coord4 = new DotSpatial.Topology.Point(x2, y2);

                            DotSpatial.Topology.Point p_coord2 = CustomPosition.findPointwithAngleandDistance(p_coord1, p_coord4, 45, 2);
                            DotSpatial.Topology.Point p_coord3 = CustomPosition.findPointwithAngleandDistance(p_coord4, p_coord1, 45, 2);
                            Coordinate coord2 = new Coordinate(p_coord2.X, p_coord2.Y);
                            Coordinate coord3 = new Coordinate(p_coord3.X, p_coord3.Y);
                            IFeature feature = fs.AddFeature(new DotSpatial.Topology.LineString(new[] { coord1, coord2, coord3, coord4 }));
                            //feature add
                            shapefile.Features.Add(feature);

                        }
                        else if ((cable_attribute.type == "T" || cable_attribute.type == "TV" || cable_attribute.type == "FO") && cable_attribute.a_poser == "T") //nếu đã có dây nối rồi thì vẽ thêm đường bên ngoài
                        {
                            IFeatureSet fs = new FeatureSet(FeatureType.Line);
                            fs.DataTable.Columns.Add("Test", typeof(string));
                            Coordinate coord1 = new Coordinate(x1, y1);
                            Coordinate coord4 = new Coordinate(x2, y2);
                            DotSpatial.Topology.Point p_coord1 = new DotSpatial.Topology.Point(x1, y1);
                            DotSpatial.Topology.Point p_coord4 = new DotSpatial.Topology.Point(x2, y2);

                            DotSpatial.Topology.Point p_coord2 = CustomPosition.findPointwithAngleandDistance(p_coord1, p_coord4, -45, 2);
                            DotSpatial.Topology.Point p_coord3 = CustomPosition.findPointwithAngleandDistance(p_coord4, p_coord1, -45, 2);
                            Coordinate coord2 = new Coordinate(p_coord2.X, p_coord2.Y);
                            Coordinate coord3 = new Coordinate(p_coord3.X, p_coord3.Y);
                            IFeature feature = fs.AddFeature(new DotSpatial.Topology.LineString(new[] { coord1, coord2, coord3, coord4 }));
                            //feature add
                            shapefile.Features.Add(feature);
                        }
                        else
                        {
                            IFeatureSet fs = new FeatureSet(FeatureType.Line);
                            fs.DataTable.Columns.Add("Test", typeof(string));
                            Coordinate coord1 = new Coordinate(x1, y1);
                            Coordinate coord2 = new Coordinate(x2, y2);
                            IFeature feature = fs.AddFeature(new DotSpatial.Topology.LineString(new[] { coord1, coord2 }));
                            //feature add
                            shapefile.Features.Add(feature);
                        }
                    }
                    shapefile.Reproject(info_output);//change coordinator

                    shapefile.Attributes.Table = new DataTable();
                    list_ext1_ext2_conducteur = new List<string>();
                    foreach (var cable_attribute in pcm_reader.list_cable_attribute)
                    {
                        string ext1 = cable_attribute.ext_1;
                        string ext2 = cable_attribute.ext_2;
                        string ext1_ext2_conducteur = ext1 + "_" + ext2 + "_" + cable_attribute.conducteur;
                        string ext2_ext1_conducteur = ext2 + "_" + ext1 + "_" + cable_attribute.conducteur;
                        if (list_ext1_ext2_conducteur.Contains(ext1_ext2_conducteur) || list_ext1_ext2_conducteur.Contains(ext2_ext1_conducteur))
                            continue;
                        list_ext1_ext2_conducteur.Add(ext1_ext2_conducteur);

                        AttributeShapefile.updateAttributetoCableLayer(ref shapefile, cable_attribute);
                    }


                    if (output_folder != null)
                        shapefile.SaveAs(output_folder + "\\Ressources\\CABLE.shp", true);
                    else
                        MessageBox.Show("Import pcm file, please!", "Error");
                }
                else
                {
                    MessageBox.Show("Import pcm file, please!", "Error");
                }
            }
            catch
            {
                throw new Exception("Can not create cable shp");
            }
        }


        public void createZoneFile()
        {
            try
            {
                if (pcm_reader != null)
                {
                    shapefile = new PolygonShapefile()
                    {
                        Projection = info_output
                    };

                    var fs = new FeatureSet(FeatureType.Polygon);

                    List<Coordinate> coord = new List<Coordinate>
                    {
                        new Coordinate(pcm_reader.zone_top_left.X - zone_space_distance, pcm_reader.zone_top_left.Y - zone_space_distance),
                        new Coordinate(pcm_reader.zone_bottom_right.X + zone_space_distance, pcm_reader.zone_top_left.Y - zone_space_distance),
                        new Coordinate(pcm_reader.zone_bottom_right.X + zone_space_distance, pcm_reader.zone_bottom_right.Y + zone_space_distance),
                        new Coordinate(pcm_reader.zone_top_left.X - zone_space_distance, pcm_reader.zone_bottom_right.Y + zone_space_distance) //repeat the first coordinate to make sure the polygon is closed
                    };

                    Polygon x = new Polygon(coord.ToArray());
                    IFeature feature = fs.AddFeature(new DotSpatial.Topology.Polygon(new LinearRing(coord.ToArray())));
                    //feature add
                    shapefile.Features.Add(feature);


                    //attribute
                    shapefile.Attributes.Table = new DataTable();
                    pcm_reader.zone_attribute.date = this.dtExport.Value.ToString();
                    zoneAttribute zone_attr = pcm_reader.zone_attribute;
                    AttributeShapefile.updateAttributetoZoneLayer(ref shapefile, zone_attr);

                    if (output_folder != null)
                        shapefile.SaveAs(output_folder + "\\Ressources\\ZONE.shp", true);
                    else
                        MessageBox.Show("Import pcm file, please!", "Error");
                }
                else
                {
                    MessageBox.Show("Import pcm file, please!", "Error");
                }
            }
            catch
            {
                throw new Exception("Can not create Zone shp");
            }
        }

        #endregion

        #region create QGS file
        public void createQGSFile()
        {
            XmlDocument qgsFile = new XmlDocument();
            try
            {
                qgsFile.Load(default_qgs);

                //replace map
                XmlNodeList listNodes = qgsFile.SelectNodes("//layer-tree-group[@name='CADASTRE']");
                foreach (XmlNode subListNode in listNodes)
                {
                    foreach (XmlNode Node in subListNode)
                    {
                        if (Node.Attributes["name"] != null)
                        {
                            if (Node.Attributes["name"].Value == "cadastre-wms")
                            {
                                string dataSourceText = Node.Attributes["source"].Value;
                                Node.Attributes["source"].Value = dataSourceText.Replace("cadastre1", pcm_reader.insee_cadastre_1);
                            }
                            if (Node.Attributes["name"].Value == "cadastre-wms2")
                            {
                                string dataSourceText = Node.Attributes["source"].Value;
                                Node.Attributes["source"].Value = dataSourceText.Replace("cadastre2", "01002");
                            }

                        }
                    }
                }

                listNodes = qgsFile.SelectNodes("//maplayer//datasource");
                foreach (XmlNode subListNode in listNodes)
                {
                    foreach (XmlNode Node in subListNode)
                    {
                        string dataSourceText = Node.InnerText;
                        if (dataSourceText.Contains("https://inspire.cadastre.gouv.fr/scpc/cadastre1.wms?"))
                        {
                            Node.InnerText = dataSourceText.Replace("cadastre1", pcm_reader.insee_cadastre_1);
                        }
                        if (dataSourceText.Contains("https://inspire.cadastre.gouv.fr/scpc/cadastre2.wms?"))
                        {
                            Node.InnerText = dataSourceText.Replace("cadastre2", "01002");
                        }
                    }
                }



                qgsFile.Save(output_folder + "\\Livrable.qgs");
            }
            catch
            {
                throw new Exception("Can not create qgs file");
            }
        }
        #endregion

        #region create support folder
        //Copy all the files & Replaces any files with the same name
        public void createSupportFolder()
        {
            try
            {
                DirectoryInfo srcFolder = new DirectoryInfo(support_folder);
                string destFolder = output_folder + "\\Ressources\\Support";
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                foreach (FileInfo newPath in srcFolder.GetFiles())
                {
                    string temppath = Path.Combine(destFolder, newPath.Name);
                    newPath.CopyTo(temppath, true);
                }
            }
            catch
            {
                throw new Exception("Can not create support folder");
            }
        }

        #endregion

        #region create logo folder
        //Copy all the files & Replaces any files with the same name
        public void createLogoFolder()
        {
            try
            {
                //copy operator logo

                DirectoryInfo srcFolder = new DirectoryInfo(operator_logo_folder);
                string destFolder = output_folder + "\\Ressources\\Logo";
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                foreach (FileInfo newPath in srcFolder.GetFiles())
                {
                    if (newPath.Name.Contains(logoName))
                    {
                        string temppath = Path.Combine(destFolder, "LOGO_" + newPath.Name);
                        newPath.CopyTo(temppath, true);
                    }
                }

                //copy distributor logo
                DirectoryInfo srcdisFolder = new DirectoryInfo(distributor_logo_folder);
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                foreach (FileInfo newPath in srcdisFolder.GetFiles())
                {
                    string temppath = Path.Combine(destFolder, newPath.Name);
                    newPath.CopyTo(temppath, true);
                }
            }
            catch
            {
                throw new Exception("Can not create logo folder");
            }
        }

        #endregion

        #region create photos folder
        //Copy all the files & Replaces any files with the same name
        public void createPhotosFolder()
        {
            try
            {
                DirectoryInfo srcFolder = new DirectoryInfo(photos_folder_input);
                string destFolder = output_folder + "\\Ressources\\Photos";
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);

                foreach (FileInfo newPath in srcFolder.GetFiles())
                {
                    foreach (var attribute in pcm_reader.list_support_attribute)
                    {
                        if (newPath.Name.Contains(attribute.nom) && attribute.gene_etiq == "T")
                        {
                            string temppath = Path.Combine(destFolder, newPath.Name);
                            newPath.CopyTo(temppath, true);
                        }
                    }

                }
            }
            catch
            {
                throw new Exception("Can not Create Photo Folder");
            }
        }

        #endregion

        #region create Web
        public void createWebEtude()
        {
            try
            {
                WebGenerator.createEtudeWeb(pcm_reader, output_folder + "\\Ressources\\etude.html");
                createCSSFile();
            }
            catch
            {
                throw new Exception("Can not create Web html");
            }
        }

        public void createCSSFile()
        {
            try
            {
                string destFolder = output_folder + "\\Ressources";
                FileInfo cssFile = new FileInfo(style_css);
                string temppath = Path.Combine(destFolder, cssFile.Name);
                cssFile.CopyTo(temppath, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region create ExcelFile
        public void createExcelTemplate()
        {
            ConvertToExcel.exportTemplateExcel(pcm_reader, output_folder + "\\" + pcm_reader.zone_attribute.ref_etude + "_Template.xlsx", photos_folder_input);
        }
        #endregion

        #region async function
        public async Task ExportQgisProject()
        {
            await Task.Run(() =>
            {
                try
                {
                    enableControl(false);
                    this.updateStatus("Exporting qgis project...");

                    createSupportFile();
                    createCableFile();
                    createZoneFile();
                    createQGSFile();
                    createSupportFolder();
                    createWebEtude();
                    createPhotosFolder();
                    createLogoFolder();
                    createExcelTemplate();

                    enableControl(true);
                    this.updateStatus("Export qgis project successfully");
                }
                catch (Exception ex)
                {
                    enableControl(true);
                    this.updateStatus("Export qgis project fail!",false);
                    MessageBox.Show(ex.ToString(), "Error");
                }
            });
        }


        public async Task ExportExcelFile(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    enableControl(false);
                    this.updateStatus("Exporting Excel Report...");

                    ConvertToExcel.exportExcelReport(pdf_input_path, excel_input_path, path);

                    enableControl(true);
                    this.updateStatus("Export Excel Report successfully");
                }
                catch
                {
                    enableControl(true);
                    this.updateStatus("Export Excel Report fail!",false);
                    MessageBox.Show("Can not export Excel Report", "Error");
                }
            });
        }

        #endregion

        #region button click
        private void btnImportLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog logoFileDialog = new OpenFileDialog())
            {
                logoFileDialog.Filter = "JPGLogo|*.jpg";
                logoFileDialog.Title = "Import Logo";
                logoFileDialog.Multiselect = false;
                //openFileDialog1.InitialDirectory = lastLogDir ?? Properties.Settings.Default.LogDir;

                if (logoFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileInfo logoFile = new FileInfo(logoFileDialog.FileName);
                        string temppath = Path.Combine(operator_logo_folder, logoFile.Name);
                        logoFile.CopyTo(temppath, true);
                        loadListLogo();
                        this.updateStatus("Import logo success!");
                    }
                    catch (Exception ex)
                    {
                        this.updateStatus("Import logo fail!");
                        MessageBox.Show("Can not import Logo", "Error");
                    }

                    return;
                }

            }
        }

        /// <summary>
        /// export qgis project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            //check time license
            DateTime expireDate = new DateTime(2020, 5, 18);//18/05/2020\
            if (DateTime.Now > expireDate)
                Application.Exit();
            //end check

            if (cbbOperator.Text == "")
            {
                MessageBox.Show("Please select operateur", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExportQgisProject().ConfigureAwait(true);
        }


        private void btnImportPDF_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog pdfFileDialog = new OpenFileDialog())
            {
                pdfFileDialog.Filter = "PDF Report|*.PDF";
                pdfFileDialog.Title = "Import PDF Report File";
                pdfFileDialog.Multiselect = false;

                if (pdfFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pdf_input_path = pdfFileDialog.FileName;
                        this.lbPdf.Text = pdf_input_path;
                    }
                    catch
                    {
                        MessageBox.Show("Can not Open PDF File", "Error");
                    }

                    return;
                }

            }
        }

        private void btnImportExcelTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog excelFileDialog = new OpenFileDialog())
            {
                excelFileDialog.Filter = "Excel Template|*.XLSX";
                excelFileDialog.Title = "Import Excel Template File";
                excelFileDialog.Multiselect = false;

                if (excelFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        excel_input_path = excelFileDialog.FileName;
                        this.lbExcelTemplate.Text = excel_input_path;
                    }
                    catch
                    {
                        MessageBox.Show("Can not Open Excel File", "Error");
                    }

                    return;
                }

            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (pdf_input_path == null || excel_input_path == null)
            {
                MessageBox.Show("Please import pdf and excel template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SaveFileDialog excelSaveFileDialog = new SaveFileDialog())
            {
                excelSaveFileDialog.Filter = "Excel Template|*.XLSX";
                excelSaveFileDialog.Title = "Import Excel Template File";

                if (excelSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportExcelFile(excelSaveFileDialog.FileName).ConfigureAwait(true);
                    return;
                }

            }

        }

        #endregion

        #region textChanged event
        private void cbbOperator_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text != "")
            {
                pcm_reader.operator_pcm = (sender as ComboBox).Text;
                lock (new object())
                {
                    logoName = this.cbbOperator.Text;
                }
            }

        }

        private void btnExit_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnExit.BackColor = System.Drawing.Color.Red;
        }

        private void btnExit_MouseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            this.btnExit.BackColor = System.Drawing.Color.Silver;
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            this.btnExit.BackColor = System.Drawing.Color.Gray;
        }

        //text change

        private void txtOperatingCorrespondent_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.operating_corespondent = txtOperatingCorrespondent.Text;
        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.street = txtStreet.Text;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.description = txtDescription.Text;
        }

        private void txtReference_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.reference = txtReference.Text;
        }

        private void txtInsee_casd1_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.insee_cadastre_1 = txtInsee_casd1.Text;
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.phone_number = txtPhoneNumber.Text;
        }

        private void txtCommon_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.common = txtCommon.Text;
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.comment = txtComment.Text;
        }

        private void txtNumberEplans_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.number_eplans = txtNumberEplans.Text;
        }

        private void txtInsee_casd2_TextChanged(object sender, EventArgs e)
        {
            pcm_reader.insee_cadastre_2 = txtInsee_casd2.Text;
        }

        #endregion

        public void updateStatus(string stt, bool success = true)
        {
            BeginInvoke(new Action(() =>
            {
                lock (new object())
                {
                    this.lbProcess.Text = stt;
                    if(success == true)
                        this.lbProcess.ForeColor = System.Drawing.Color.OliveDrab;
                    else
                        this.lbProcess.ForeColor = System.Drawing.Color.Red;
                }
            }));
        }

        public void enableControl(bool enb= true)
        {
            BeginInvoke(new Action(() =>
            {
                lock(new object())
                {
                    this.txtOperatingCorrespondent.Enabled = enb;
                    this.txtStreet.Enabled = enb;
                    this.txtDescription.Enabled = enb;
                    this.txtReference.Enabled = enb;
                    this.txtInsee_casd1.Enabled = enb;

                    this.txtPhoneNumber.Enabled = enb;
                    this.txtCommon.Enabled = enb;
                    this.txtComment.Enabled = enb;
                    this.txtNumberEplans.Enabled = enb;
                    this.txtInsee_casd2.Enabled = enb;

                    this.txtDirSource.Enabled = enb;
                    this.dtExport.Enabled = enb;

                    this.btnImportLogo.Enabled = enb;
                    this.btnExport.Enabled = enb;
                    this.btnImportPdf.Enabled = enb;
                    this.btnImportExcelTemplate.Enabled = enb;
                    this.btnExportExcel.Enabled = enb;

                    this.btnOpenFile.Enabled = enb;
                    this.cbbOperator.Enabled = enb;
                    
                }
            }));
            
        }
    }
}

