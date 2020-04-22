using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace test
{
    public static class AttributeShapefile
    {
        public static void updateAttributetoSupportLayer(ref Shapefile shapefile, supportAttribute supportNode)
        {
           
            DataRow newRow = shapefile.Attributes.Table.NewRow();
            //reflection
            var properties = supportNode.GetType().GetProperties();
            foreach (PropertyInfo prp in properties)
            {
                string name = prp.Name;
                object value = prp.GetValue(supportNode, new object[] { });
                //Console.WriteLine(attributeNode.Name + ":" + attributeNode.InnerText);
                string columName = name.ToUpper();
                string nodeValue = "";
                if(value != null)
                    nodeValue= value.ToString();
                //int index_comma = nodeValue.IndexOf(",");
                //if (index_comma >= 0)
                //{
                //    nodeValue = nodeValue.Replace(',', '.');
                //}
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

        public static void updateAttributetoCableLayer(ref Shapefile shapefile, cableAttribute cableNode)
        {
            DataRow newRow = shapefile.Attributes.Table.NewRow();

            //reflection
            var properties = cableNode.GetType().GetProperties();
            foreach (PropertyInfo prp in properties)
            {
                string name = prp.Name;
                object value = prp.GetValue(cableNode, new object[] { });
                //Console.WriteLine(attributeNode.Name + ":" + attributeNode.InnerText);
                string columName = name.ToUpper();
                string nodeValue = "";
                if (value != null)
                    nodeValue = value.ToString();
                //int index_comma = nodeValue.IndexOf(",");
                //if (index_comma >= 0)
                //{
                //    nodeValue = nodeValue.Replace(',', '.');
                //}
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

        public static void updateAttributetoZoneLayer(ref Shapefile shapefile, zoneAttribute zoneNode)
        {
            DataRow newRow = shapefile.Attributes.Table.NewRow();
            var properties = zoneNode.GetType().GetProperties();
            foreach (PropertyInfo prp in properties)
            {
                string name = prp.Name;
                object value = prp.GetValue(zoneNode, new object[] { });
                //Console.WriteLine(attributeNode.Name + ":" + attributeNode.InnerText);
                string columName = name.ToUpper();
                string nodeValue = "";
                if (value != null)
                    nodeValue = value.ToString();
                //int index_comma = nodeValue.IndexOf(",");
                //if (index_comma >= 0)
                //{
                //    nodeValue = nodeValue.Replace(',', '.');
                //}
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
    }
}
