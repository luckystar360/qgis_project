using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Projections;
using DotSpatial.Data;
using NUnit.Framework;
using System.Xml;
using System.Collections;

namespace test
{
    public partial class Form1 : Form
    {
        private string default_prj_out = @"qgis_file\DEFAULT_OUTPUT.prj";
        private string mapping_file = @"qgis_file\MAPPING.xlsx";
        XmlDocument pcm_doc;
        ProjectionInfo info_output;
        Shapefile shapefile;
        MappingAttribute mapping_attribute;

        public Form1()
        {
            InitializeComponent();
            mapping_attribute = new MappingAttribute();

            info_output = ProjectionInfo.Open(default_prj_out);
            if (!mapping_attribute.readFileExcel(mapping_file))
            {
                //mapping_data = mapping_attribute.mapping_data;
                Application.Exit();
            }
        }

        /// <summary>
        /// import shapefile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog shpOpenFileDialog = new OpenFileDialog())
            {
                shpOpenFileDialog.Filter = "shp|*.shp";
                shpOpenFileDialog.Title = "Import ShapeFile";
                shpOpenFileDialog.Multiselect = false;
                //openFileDialog1.InitialDirectory = lastLogDir ?? Properties.Settings.Default.LogDir;

                if (shpOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //shapeFileOpen_name = shpOpenFileDialog.SafeFileName;
                    shapefile = Shapefile.OpenFile(shpOpenFileDialog.FileName);
                    shapefile.Reproject(info_output);

                    return;
                }
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
                pcmOpenFileDialog.Multiselect = true;
                //openFileDialog1.InitialDirectory = lastLogDir ?? Properties.Settings.Default.LogDir;

                if (pcmOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pcm_doc = new XmlDocument();
                    try
                    {
                        pcm_doc.Load(pcmOpenFileDialog.FileName);

                        //selectForm_Commited(null);
                    }
                    catch
                    {

                    }
                    return;
                }

            }

        }


        /// <summary>
        /// export shape file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                XmlNodeList listSupportsNode = pcm_doc.SelectNodes("//Supports");
                AttributeShapefile.updateAttributetoSupport(ref shapefile, listSupportsNode, mapping_attribute);
            }
            catch
            {
                MessageBox.Show("Error update attribute from pcm to shape file");
            }
            using (SaveFileDialog shpSaveFileDialog = new SaveFileDialog())
                {
                    shpSaveFileDialog.Filter = ".shp(ShapeFile)|*.shp";
                    shpSaveFileDialog.Title = "Save a shapeFile";
                    if (shpSaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (shpSaveFileDialog.FileName != null)
                        {
                            shapefile.SaveAs(shpSaveFileDialog.FileName, true);
                        }
                    }

                }
            

        }

    }
}

