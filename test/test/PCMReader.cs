using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace test
{
    public class PCMReader
    {
        private XmlDocument pcm_doc;

        public string version_pcm { get; set; }

        public string operator_pcm { get; set; }
        public string operating_corespondent { get; set; }
        public string street { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string insee_cadastre_1 { get; set; }

        public string energy_distributor { get; set; }
        public string phone_number { get; set; }
        public string common { get; set; }
        public string comment { get; set; }
        public string number_eplans { get; set; }
        public string insee_cadastre_2 { get; set; }

        public string pcm_num_affai { get; set; }

        public Point zone_top_left { get; set; }
        public Point zone_bottom_right { get; set; }

        public List<supportAttribute> list_support_attribute;
        public List<cableAttribute> list_cable_attribute;
        public zoneAttribute zone_attribute
        {
            get
            {
                return new zoneAttribute()
                {
                    operateur = operator_pcm,
                    date="",
                    ref_etude = reference,
                    num_affai = pcm_num_affai,
                    nom = operating_corespondent,
                    telephone = phone_number,
                    mobile = "",
                    email = "",
                    pj = "",
                    adresse = street,
                    commune = common,
                    insee = insee_cadastre_1,
                    be = "",
                    distrener = energy_distributor
                };
            }
        }

        
        public bool openFile(string path)
        {
            pcm_doc = new XmlDocument();
            try
            {
                pcm_doc.Load(path);
                list_support_attribute = new List<supportAttribute>();
                list_cable_attribute   = new List<cableAttribute>();
                parsePCMasSingleNode(pcm_doc);
                parsePCMasSupportAttribute(pcm_doc);
                parsePCMasCableAttribute(pcm_doc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void parsePCMasSingleNode(XmlDocument pcm_doc)
        {
            version_pcm = getSingleNode("Version");
            operator_pcm = getSingleNode("Operateur");
            operating_corespondent = getSingleNode("NomCorrespondant");
            street = getSingleNode("Rue");
            description = getSingleNode("Description");
            reference = getSingleNode("NumEtude");
            insee_cadastre_1 = getSingleNode("Insee");

            energy_distributor = getSingleNode("DistEnergie");
            phone_number = getSingleNode("TelCorrespondant");
            common = getSingleNode("Commune");
            comment = getSingleNode("Commentaire");

            pcm_num_affai = getSingleNode("NumAffaire");
            //number_eplans          = getSigleNode("Operateur");
            //insee_cadastre_2       = getSigleNode("Operateur");
        }

        private void parsePCMasSupportAttribute(XmlDocument pcm_doc)
        {
            try
            {
                XmlNodeList listSupportsNode = pcm_doc.SelectNodes("//Supports");
                int id = 1;
                foreach (XmlNode SupportsNode in listSupportsNode)
                {
                    if (SupportsNode.ParentNode.Name != "Etude")
                        continue;
                    zone_top_left = new Point(100000000, 10000000);
                    zone_bottom_right = new Point(0, 0);

                    foreach (XmlNode SupportNode in SupportsNode)
                    {
                        supportAttribute spAttribute = new supportAttribute();
                        spAttribute.propriete = "ENEDIS";
                        spAttribute.id = (id++).ToString();
                        foreach (XmlNode attributeNode in SupportNode)
                        {
                            string nodeName = attributeNode.Name;
                            string nodeValue = attributeNode.InnerText;
                            switch (nodeName)
                            {
                                case "Nom":
                                    spAttribute.nom = nodeValue;
                                    break;
                                case "optBranchementsTelExistants":
                                    spAttribute.exist = nodeValue;
                                    break;
                                case "Nature":
                                    spAttribute.nature = nodeValue;
                                    break;
                                case "Hauteur":
                                    spAttribute.hauteur = nodeValue;
                                    break;
                                case "Classe":
                                    spAttribute.classe = nodeValue;
                                    break;
                                case "Effort":
                                    spAttribute.effort = nodeValue;
                                    break;
                                case "Annee":
                                    spAttribute.annee = nodeValue;
                                    break;
                                case "Orientation":
                                    spAttribute.orientatio = nodeValue;
                                    break;
                                case "Commentaire":
                                    spAttribute.descriptio = nodeValue;
                                    break;
                                case "BranchementsTV":
                                    spAttribute.branche_tv = nodeValue;
                                    break;
                                case "PresenceEP":
                                    spAttribute.pres_ep = nodeValue;
                                    break;
                                case "Etat":
                                    spAttribute.etat_vis = nodeValue;
                                    break;
                                case "TraverseExistante1":
                                    spAttribute.mat_exis = nodeValue;
                                    break;
                                case "TraverseAPoser2":
                                    spAttribute.mat_a_pos = nodeValue;
                                    break;
                                case "APoser":
                                    spAttribute.a_poser = nodeValue;
                                    break;
                                case "X":
                                    spAttribute.x = nodeValue;
                                    break;
                                case "Y":
                                    spAttribute.y = nodeValue;
                                    break;

                                default:
                                    break;
                            }
                        }

                        list_support_attribute.Add(spAttribute);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot get support attribute", "Error");
            }
        }

        private void parsePCMasCableAttribute(XmlDocument pcm_doc)
        {
            try
            {
                XmlNodeList listPorteesNode = pcm_doc.SelectNodes("//Portees");
                foreach (XmlNode PorteesNode in listPorteesNode)
                {
                    if (PorteesNode.ParentNode.Name != "Etude")
                        continue;
                    foreach (XmlNode PorteeNode in PorteesNode) //tìm được số khoảng cách có dây. = số cột - 1 
                    {

                        //tìm các loại dây có trên đoạn đó: vd:LignesBT , LignesTCF
                        XmlNodeList listLignesBTNode = pcm_doc.SelectNodes("//LignesBT");
                        foreach (XmlNode LignesBTNode in listLignesBTNode)
                        {
                            if (LignesBTNode.ParentNode.Name != "Etude")
                                continue;
                            foreach (XmlNode LigneBTNode in LignesBTNode) //tìm được số khoảng cách có dây. = số cột - 1 
                            {

                                cableAttribute cableAttribute = new cableAttribute();
                                cableAttribute.ext_1 = getValueinNodeChild(PorteeNode, "SuppD");
                                cableAttribute.ext_2 = getValueinNodeChild(PorteeNode, "SuppG");

                                //kiểm tra loại dây này nối những cột nào
                                List<string> list_ext = new List<string>();
                                XmlNodeList listSupportLignesBTNode = pcm_doc.SelectNodes("//LignesBT//LigneBT//Supports//Support");
                                foreach (XmlNode nodeSupport in listSupportLignesBTNode)
                                {
                                    list_ext.Add(nodeSupport.InnerText);
                                }

                                if (!list_ext.Contains(cableAttribute.ext_1) || !list_ext.Contains(cableAttribute.ext_2))
                                    continue;
                                
                                //kết thúc kiểm tra

                                cableAttribute.a_poser = getValueinNodeChild(LigneBTNode, "APoser");
                                if (cableAttribute.a_poser == "1") cableAttribute.a_poser = "T";
                                else
                                    cableAttribute.a_poser = "F";
                                cableAttribute.conducteur = getValueinNodeChild(LigneBTNode, "Conducteur");
                                cableAttribute.type = getTypefromConducteur(cableAttribute.conducteur);
                                cableAttribute.longueur = getValueinNodeChild(PorteeNode, "Longueur");
                                cableAttribute.angle = getValueinNodeChild(PorteeNode, "Angle");
                                cableAttribute.route = getValueinNodeChild(PorteeNode, "Route");
                                list_cable_attribute.Add(cableAttribute);

                            }
                        }

                        XmlNodeList listLignesTCFNode = pcm_doc.SelectNodes("//LignesTCF");
                        foreach (XmlNode LignesTCFNode in listLignesTCFNode)
                        {
                            if (LignesTCFNode.ParentNode.Name != "Etude")
                                continue;
                            foreach (XmlNode LigneTCFNode in LignesTCFNode) //tìm được số khoảng cách có dây. = số cột - 1 
                            {
                                cableAttribute cableAttribute = new cableAttribute();
                                cableAttribute.ext_1 = getValueinNodeChild(PorteeNode, "SuppD");
                                cableAttribute.ext_2 = getValueinNodeChild(PorteeNode, "SuppG");

                                //kiểm tra loại dây này nối những cột nào
                                List<string> list_ext = new List<string>();
                                XmlNodeList listSupportLignesBTNode = pcm_doc.SelectNodes("//LignesTCF//LigneTCF//Supports//Support");
                                foreach (XmlNode nodeSupport in listSupportLignesBTNode)
                                {
                                    list_ext.Add(nodeSupport.InnerText);
                                }

                                if (!list_ext.Contains(cableAttribute.ext_1) || !list_ext.Contains(cableAttribute.ext_2))
                                    continue;

                                //kết thúc kiểm tra

                                cableAttribute.a_poser = getValueinNodeChild(LigneTCFNode, "APoser");
                                if (cableAttribute.a_poser == "1") cableAttribute.a_poser = "T";
                                else
                                    cableAttribute.a_poser = "F";
                                cableAttribute.conducteur = getValueinNodeChild(LigneTCFNode, "Cable");
                                cableAttribute.type = getTypefromConducteur(cableAttribute.conducteur);
                                cableAttribute.longueur = getValueinNodeChild(PorteeNode, "Longueur");
                                cableAttribute.angle = getValueinNodeChild(PorteeNode, "Angle");
                                cableAttribute.route = getValueinNodeChild(PorteeNode, "Route");
                                list_cable_attribute.Add(cableAttribute);
                            }
                        }

                        //kết thúc tìm các loại dây. nếu có thêm loại nào nữa thì thêm vào bên trên
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot get cable attribute", "Error");
            }
        }

        

        private string getValueinNodeChild(XmlNode node, string name)
        {
            foreach (XmlNode attributeNode in node)
            {
                string nodeName = attributeNode.Name;
                string nodeValue = attributeNode.InnerText;
                if (nodeName == name)
                    return nodeValue;
            }
            return "";
        }

        private string getSingleNode(string node)
        {
            try
            {
                XmlNodeList listSupportsNode = pcm_doc.SelectNodes("//" + node);
                if (listSupportsNode.Count > 0)
                    return listSupportsNode[0].InnerText;
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        private string getTypefromConducteur(string conducteur)
        {
            if (conducteur.Contains("D-"))
                return "FO";
            else if (conducteur.Contains("BT"))
                return "I";
            else if (conducteur.Contains("/") ||conducteur.Contains("-"))
                return "TV";
            else
                return "N";
        }

    }
}
