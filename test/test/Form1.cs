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

namespace test
{
    public partial class Form1 : Form
    {
        private string default_prj_out = @"qgis_file\default_output.prj";
        private string default_qgs = @"qgis_file\default_project.qgs";

        private string default_support_shp = @"qgis_file\shape_file_default\Supports.shp";
        private string default_lignes_shp = @"qgis_file\shape_file_default\Lignes.shp";

        private string support_folder = @"qgis_file\Support";

        private string mapping_file = @"qgis_file\MAPPING.xlsx";

        private string source_cadastre_map = "contextualWMSLegend=0&amp;crs=EPSG:3857&amp;dpiMode=7&amp;featureCount=10&amp;format=image/png&amp;layers=CP.CadastralParcel&amp;layers=CLOTURE&amp;layers=HYDRO&amp;layers=DETAIL_TOPO&amp;layers=VOIE_COMMUNICATION&amp;layers=BU.Building&amp;layers=BORNE_REPERE&amp;maxHeight=800&amp;maxWidth=1200&amp;styles&amp;styles&amp;styles&amp;styles&amp;styles&amp;styles&amp;styles&amp;url=https://inspire.cadastre.gouv.fr/scpc";// /31507.wms?";

        private string output_folder;

        const double zone_space_distance = 60;

        XmlDocument pcm_doc;
        ProjectionInfo info_input;
        ProjectionInfo info_output;
        Shapefile shapefile;
        MappingAttribute mapping_attribute;
        PCMReader pcm_reader;

        Shapefile support_shapefile;


        public Form1()
        {
            InitializeComponent();
            mapping_attribute = new MappingAttribute();
            pcm_reader = new PCMReader();

            info_output = ProjectionInfo.Open(default_prj_out);
            if (!mapping_attribute.readFileExcel(mapping_file))
            {
                //mapping_data = mapping_attribute.mapping_data;
                Application.Exit();
            }
        }



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

                        //selectForm_Commited(null);
                    }
                    catch
                    {

                    }
                    return;
                }

            }

        }

        #region create ShapeFile
        public void createSupportFile()
        {
            try
            {
                if (pcm_reader != null)
                {
                    shapefile = Shapefile.OpenFile(default_support_shp);
                    info_input = shapefile.Projection;
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

                    shapefile.Attributes.Table.Clear();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        public void createCableFile()
        {
            try
            {
                if (pcm_reader != null)
                {
                    shapefile = Shapefile.OpenFile(default_lignes_shp);
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

                    shapefile.Attributes.Table.Clear();
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
            catch (Exception ex)
            {
                throw ex;
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
                    shapefile.Attributes.Table.Clear();
                    zoneAttribute zone_attr = pcm_reader.zone_attribute;
                    zone_attr.date = this.dtExport.Value.ToString();

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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                createSupportFile();
                createCableFile();
                createZoneFile();
                createQGSFile();
                createSupportFolder();

                MessageBox.Show("Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}

