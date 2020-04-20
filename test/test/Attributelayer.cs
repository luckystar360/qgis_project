using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class supportAttribute
    {
        public string propriete { get; set; }//
        public string nom { get; set; }
        public string exist { get; set; }
        public string nature { get; set; }
        public string hauteur { get; set; }
        public string classe { get; set; }
        public string effort { get; set; }
        public string annee { get; set; }
        public string orientatio { get; set; }
        public string descriptio { get; set; }
        public string branche_tv { get; set; }
        public string ras_terre { get; set; }//
        public string pres_ep { get; set; }
        public string etat_vis { get; set; }
        public string gene_etiq { get; set; }
        public string mat_exis { get; set; }
        public string mat_a_pos { get; set; }
        public string a_poser { get; set; }

        public string x { get; set; }
        public string y { get; set; }

        public string id { get; set; }
    }

    public class cableAttribute
    {
        public string a_poser { get; set; }
        public string type { get; set; }
        public string conducteur { get; set; }
        public string ext_1 { get; set; }
        public string ext_2 { get; set; }
        public string longueur { get; set; }
        public string angle { get; set; }
        public string route { get; set; }
        public string x_etiq { get; set; }
        public string y_etiq { get; set; }
        public string z_etiq { get; set; }
    }

    public class zoneAttribute
    {
        public string operateur { get; set; }
        public string date { get; set; }
        public string ref_etude { get; set; }
        public string num_affai { get; set; }
        public string nom { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string pj { get; set; }
        public string adresse { get; set; }
        public string commune { get; set; }
        public string insee { get; set; }
        public string be { get; set; }
        public string distrener { get; set; }
    }
}
