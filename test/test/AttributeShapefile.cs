using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace test
{
    public static class AttributeShapefile
    {
        public static void updateAttributetoSupport(ref Shapefile shapefile, XmlNodeList listSupportsNode, MappingAttribute mapping_attribute)
        {
            shapefile.Attributes.Table.Clear();
            int index_row = 1;
            foreach (XmlNode SupportsNode in listSupportsNode)
            {
                foreach (XmlNode SupportNode in SupportsNode)
                {
                    DataRow newRow = shapefile.Attributes.Table.NewRow();

                    string zeroColumnName = "PROPRIETE";
                    string idColumnName = "ID";
                    if (!shapefile.Attributes.Table.Columns.Contains(zeroColumnName))
                    {
                        DataColumn zeroColumn = new DataColumn(zeroColumnName, typeof(string));
                        zeroColumn.MaxLength = 50;
                        shapefile.Attributes.Table.Columns.Add(zeroColumn);
                    }
                    if (!shapefile.Attributes.Table.Columns.Contains(idColumnName))
                    {
                        DataColumn idColumn = new DataColumn(idColumnName, typeof(string));
                        idColumn.MaxLength = 50;
                        shapefile.Attributes.Table.Columns.Add(idColumn);
                    }
                    newRow[zeroColumnName] = "ENEDIS";
                    newRow[idColumnName] = index_row++;
                    foreach (XmlNode attributeNode in SupportNode)
                    {
                        //Console.WriteLine(attributeNode.Name + ":" + attributeNode.InnerText);
                        string columName = mapping_attribute.Mapping(attributeNode.Name).ToUpper();
                        string nodeValue = attributeNode.InnerText;
                        int index_comma = nodeValue.IndexOf(",");
                        if (index_comma >= 0)
                        {
                            nodeValue = nodeValue.Replace(',', '.');
                        }
                        if (!shapefile.Attributes.Table.Columns.Contains(columName))
                        {
                            DataColumn newCol = new DataColumn(columName, typeof(string));
                            newCol.MaxLength = 50;
                            shapefile.Attributes.Table.Columns.Add(newCol);
                        }
                        newRow[columName] = nodeValue;
                    }
                    shapefile.Attributes.Table.Rows.Add(newRow);
                }
                break;
            }
        }
    }
}
