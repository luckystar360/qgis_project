using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
namespace test
{
    public class MappingAttribute
    {
        public Hashtable mapping_data;
        public MappingAttribute()
        {
            mapping_data = new Hashtable();
        }

        public bool readFileExcel(string path)
        {
            try
            {             
                FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read); 
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //mapping_data = excelReader.AsDataSet();
                try
                {
                    while (excelReader.Read())
                    {
                        string namePCM = excelReader.GetString(0);
                        string nameQGS = excelReader.GetString(1);
                        if(!mapping_data.ContainsKey(namePCM))
                            mapping_data.Add(namePCM, nameQGS);
                    }
                }
                catch
                {
                    MessageBox.Show("Mapping file Error!!!");
                    return false;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Can't load Mapping File (.xlsx)");
                return false; 
            }
        }

        public string Mapping(string pcmNodeName)
        {
            if(mapping_data.ContainsKey(pcmNodeName))
            {
                return mapping_data[pcmNodeName].ToString();
            }   
            else
                return pcmNodeName;
        }

    }
}
