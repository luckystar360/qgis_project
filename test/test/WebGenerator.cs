using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public static class WebGenerator
    {
        public static BufferedStream html_buffer { get; set; }

        public static void createEtudeWeb(PCMReader pcm_reader,string pathOutput)
        {     
            html_buffer = new BufferedStream(File.Open(pathOutput, FileMode.Create, FileAccess.ReadWrite, FileShare.None));
            html_buffer.Seek(0, SeekOrigin.Begin); //override file
            createHeader();
            createbody(pcm_reader);
            createFooter();
            html_buffer.Close();
        }

        private static void writeLine(string textLine)
        {
            if (html_buffer != null || html_buffer.CanWrite)
            {
                byte[] data = Encoding.UTF8.GetBytes(textLine + "\n");
                html_buffer.Write(data,0,data.Length);
                html_buffer.Flush();
            }
        }

        private static void createHeader()
        {
            writeLine("<!doctype html><html lang = 'fr'>");
            writeLine("<head>");
            writeLine("<meta http-equiv = 'Content-Type' content = 'text/html;charset=utf-8' />");
            writeLine("<title> Etude ENEDIS </title>");
            writeLine("<link rel = 'stylesheet' href = 'style.css' media = 'all' >");
            writeLine("</head>");
        }

        private static void createbody(PCMReader pcm_reader)
        {
            string num_affaiire = pcm_reader.pcm_num_affai;

            writeLine("<body>");
            writeLine("<div>");
            writeLine("<br>");
            writeLine("<table class='table_legende'>");
            writeLine("<tr>");
            writeLine("<td class='td_legende'>Réf.étude opérateur :</td>");
            writeLine("<td class='td_legende'>" + num_affaiire + "</td>");
            writeLine("<td class='td_legende'>N° affaire Enedis :</td>");
            writeLine("<td class='td_legende'></td>");
            writeLine("</tr>");
            writeLine("</table>");
            writeLine("<br>");

            writeLine("<table class='table_cantons'>");
            writeLine("<tr>");
            writeLine("<th class='th_cantons' colspan='6'>Cantons</th>");
            writeLine("</tr>");
            writeLine("<tr>");
            writeLine("<th class='th_cantons'>Câble</th>");
            writeLine("<th class='th_cantons'>Type</th>");
            writeLine("<th class='th_cantons'>Support définissant les cantons</th>");
            writeLine("<th class='th_cantons'>Portées équivalente</th>");
            writeLine("<th class='th_cantons'>Flèche relevée sur le terrain et portée de référence</th>");
            writeLine("<th class='th_cantons'>Paramètre retenu</th>");
            writeLine("</tr>");

            List<string> list_cable_string = new List<string>();
            List<string> list_cable = new List<string>();
            List<string> list_type = new List<string>();
            List<string> list_suport = new List<string>();
            List<string> list_porteq = new List<string>();
            List<string> list_refrence = new List<string>();
            List<string> list_param = new List<string>();

            foreach (var attribute in pcm_reader.list_web_attribute)
            {
                writeLine("<tr>");
                writeLine("<td class='td_cantons'>" + attribute.cable + "</td>");
                writeLine("<td class='td_cantons'>" + attribute.type + "</td>");
                writeLine("<td class='td_cantons'>" + attribute.list_support + "</td>");
                writeLine("<td class='td_cantons'>" + attribute.portee_eq + "</td>");
                writeLine("<td class='td_cantons'>" + attribute.reference + "</td>");
                writeLine("<td class='td_cantons'>" + attribute.param + "</td>");
                writeLine("</tr>");
            }
            writeLine("</table>");
            writeLine("</div>");

            writeLine("<div>");
            writeLine("<br>");
            writeLine("<table class='table_legende'>");
            writeLine("<tr>");
            writeLine("<td class='td_legende'>Réf.étude opérateur :</td>");
            writeLine("<td class='td_legende'>" + num_affaiire + "</td>");
            writeLine("<td class='td_legende'>N° affaire Enedis :</td>");
            writeLine("<td class='td_legende'></td>");
            writeLine("</tr>");
            writeLine("</table>");
            writeLine("<br>");

            //image output
            foreach (var attribute in pcm_reader.list_support_attribute)
            {
                if (attribute.gene_etiq == "T")
                {
                    writeLine("<table class='table_pSupport'>");
                    writeLine("<tr>");
                    writeLine("<th colspan = '5' class='th_pSupport'>" + attribute.nom + "</th>");
                    writeLine("</tr>");
                    writeLine("<tr>");
                    writeLine("<td class='td_pSupport' style='background-image: url(Photos/" + attribute.nom + "_1.jpg)'></td>");
                    writeLine("<td class='td_pSupport' style='background-image: url(Photos/" + attribute.nom + "_2.jpg)'></td>");
                    writeLine("<td class='td_pSupport' style='background-image: url(Photos/" + attribute.nom + "_3.jpg)'></td>");
                    writeLine("<td class='td_pSupport' style='background-image: url(Photos/" + attribute.nom + "_4.jpg)'></td>");
                    writeLine("<td class='td_pSupport' style='background-image: url(Photos/" + attribute.nom + "_5.jpg)'></td>");
                    writeLine("</tr>");
                    writeLine("</table>");
                }
            }

            writeLine("</div>");
            writeLine("</body>");
        }

        private static void createFooter()
        {
            writeLine("</html>");
        }
    }
}
