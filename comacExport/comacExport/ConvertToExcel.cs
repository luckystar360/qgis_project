using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spire.Pdf;
using Spire.Xls;

using System.Drawing;
using System.Drawing.Imaging;
using Spire.Pdf.Exporting;
using Spire.Pdf.Graphics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace comacExport
{
    public static class ConvertToExcel
    {
        private static Image[] listLogo;//3 logo
        private static Image overViewMap;//
        private static Image specificMap;//
        private static Image supportMap;//
        private static List<KeyValuePair<string, Image>> listImageSupport;

        private static Workbook wbTemplate;
        private static Worksheet sheetTemplate;

        private static Workbook wbInput;
        private static Worksheet sheetInput;

        private static string pdfNom;
        private static string excelTemplateNom;

        static ConvertToExcel()
        {
            openExcelTemplate(@"qgis_file\template.xlsx");
        }

        public static void openExcelTemplate(string path)
        {
            wbTemplate = new Workbook();
            wbTemplate.LoadFromFile(path);

        }

        private static void setSheetName(string name)
        {
            sheetTemplate = wbTemplate.Worksheets[name];
        }

        #region Image function
        private static Image cropImage(Image srcImage, Rectangle cropRect)
        {
            Bitmap src = srcImage as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return target as Image;
        }

        private static Image capturePageImage(int pageNumber, PdfDocument doc)
        {
            Image img = doc.SaveAsImage(pageNumber - 1, PdfImageType.Bitmap);
            return img;
        }
        private static Bitmap imageResize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        #endregion

        #region Parse Text data
        private static string getTextWithLocation(PdfPageBase page, Point point1, Point point2)
        {
            string text = page.ExtractText(new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
            return text.Replace("Evaluation Warning : The document was created with Spire.PDF for .NET.", "");
        }
        #endregion

        #region update data to Excel
        private static void updateToSheet1Template(PCMReader pcmReader)
        {
            try
            {
                setSheetName("Page de Garde");
                sheetTemplate.Range["B7"].Text = pcmReader.zone_attribute.operateur; //operateur
                sheetTemplate.Range["C8"].Text = pcmReader.zone_attribute.ref_etude; //reference
                sheetTemplate.Range["H7"].Text = pcmReader.zone_attribute.date; //date
                                                                                //sheetTemplate.Range["I8"].Text  = getTextWithLocation(page, new Point(516, 159), new Point(579, 173)); //Endis
                sheetTemplate.Range["F9"].Text = pcmReader.zone_attribute.nom;//Nom
                sheetTemplate.Range["B10"].Text = pcmReader.zone_attribute.telephone; ; //Tel
                sheetTemplate.Range["G10"].Text = pcmReader.zone_attribute.mobile; ; //Mobile
                sheetTemplate.Range["B11"].Text = pcmReader.zone_attribute.email; ; //Email
                sheetTemplate.Range["B12"].Text = pcmReader.zone_attribute.pj; //PJ
                sheetTemplate.Range["C13"].Text = pcmReader.zone_attribute.adresse; ; //address
                sheetTemplate.Range["B14"].Text = pcmReader.zone_attribute.commune; //comune
                sheetTemplate.Range["H14"].Text = pcmReader.zone_attribute.insee; ; //insee
                sheetTemplate.Range["D16"].Text = pcmReader.zone_attribute.be; //bureau etude
            }
            catch
            {
                throw new Exception("Cannot generate \"Page de Garde\" Sheet");
            }

        }

        private static void updateToSheet2Template(PCMReader pcmReader)
        {
            try
            {
                setSheetName("Plan moyenne échelle");
                CellRange buffCells = sheetTemplate.Range["A38:J44"];

                //buffCells.Copy(sheetTemplate.Range["A48:J54"]);
                sheetTemplate.Range["B1"].Text = pcmReader.zone_attribute.ref_etude; //

                int col_num = 2;
                int row_num = 38;
                foreach (supportAttribute spAttr in pcmReader.list_support_attribute)
                {
                    if (spAttr.gene_etiq == "T")
                    {
                        buffCells.Copy(sheetTemplate.Range[row_num, 1, row_num + 6, 10]);

                        sheetTemplate.Range[row_num, col_num].Text = spAttr.nom;
                        sheetTemplate.Range[row_num + 1, col_num].Text = spAttr.hauteur + " " + spAttr.classe + " " + spAttr.effort + " " + spAttr.annee;
                        sheetTemplate.Range[row_num + 2, col_num].Text = "Angle piquetage " + spAttr.angle_pi + " gr\n" + "Orientation " + spAttr.orientatio + " gr";
                        sheetTemplate.Range[row_num + 3, col_num].Text = spAttr.etat_vis;
                        sheetTemplate.Range[row_num + 4, col_num].Text = spAttr.mat_exist;
                        sheetTemplate.Range[row_num + 5, col_num].Text = spAttr.mat_a_pos;
                        sheetTemplate.Range[row_num + 6, col_num].Text = spAttr.descriptio;

                        col_num++;
                        if (col_num > 10)//change number to set max col in table
                        {
                            col_num = 2;
                            row_num += 10;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Cannot generate \"Plan moyenne échelle\" Sheet");
            }
        }

        private static void updateToSheet3Template(PCMReader pcmReader)
        {
            try
            {
                setSheetName("Données");
                sheetTemplate.Range["B1"].Text = pcmReader.zone_attribute.ref_etude; //  

                CellRange buffCells = sheetTemplate.Range["A4:H4"];
                int row_num = 4;

                foreach (var attribute in pcmReader.list_web_attribute)
                {
                    buffCells.Copy(sheetTemplate.Range[row_num, 1, row_num, 8]);

                    sheetTemplate.Range[row_num, 1].Text = attribute.cable;
                    sheetTemplate.Range[row_num, 2].Text = attribute.type;
                    sheetTemplate.Range[row_num, 3].Text = (attribute.list_support.Split(new char[] { ',' }).Length - 1).ToString();
                    sheetTemplate.Range[row_num, 4].Text = attribute.list_support;
                    sheetTemplate.Range[row_num, 5].Text = attribute.portee_eq;
                    sheetTemplate.Range[row_num, 6].Text = "";
                    sheetTemplate.Range[row_num, 7].Text = attribute.param;
                    sheetTemplate.Range[row_num, 8].Text = "";

                    row_num++;
                }
            }
            catch
            {
                throw new Exception("Cannot generate \"Données\" Sheet");
            }
        }

        private static void updateToSheet4Template(PCMReader pcmReader, string photoFolderPath)
        {
            try
            {
                setSheetName("Photo Appuis");
                sheetTemplate.Range["D1"].Text = pcmReader.zone_attribute.ref_etude; //  

                CellRange buffCells = sheetTemplate.Range["B2:O8"];
                int row_num = 2;

                foreach (supportAttribute spAttr in pcmReader.list_support_attribute)
                {
                    if (spAttr.gene_etiq == "T")
                    {
                        buffCells.Copy(sheetTemplate.Range[row_num, 2, row_num + 6, 15]);
                        sheetTemplate.Range[row_num, 2].Text = "Support n° " + spAttr.nom;
                        for (int i = 1; i <= 5; i++)
                        {
                            string photoPath = photoFolderPath + String.Format("//{0}_{1}.jpg", spAttr.nom, i);
                            if (!File.Exists(photoPath)) continue;
                            Image photo = Image.FromFile(photoPath);
                            if (photo != null)
                            {
                                photo = imageResize(photo, photo.Width * 75 / photo.Height, 75);
                                sheetTemplate.Pictures.Add(row_num + 2, 3 * i - 1, photo);
                            }
                        }

                        row_num += 8;
                    }
                }
            }
            catch
            {
                throw new Exception("Cannot generate \"Photo Appuis\" Sheet");
            }
        }

        private static void updateToSheet5Template(PCMReader pcmReader)
        {
            try
            {
                setSheetName("Mise en service");
                sheetTemplate.Range["B3"].Text = pcmReader.zone_attribute.operateur; //  
                sheetTemplate.Range["B5"].Text = pcmReader.zone_attribute.adresse;
                sheetTemplate.Range["B6"].Text = pcmReader.zone_attribute.ref_etude;
            }
            catch
            {
                throw new Exception("Cannot generate \"Mise en service\" Sheet");
            }
        }

        public static void exportTemplateExcel(PCMReader pcmReader, string path, string photoFolderPath)
        {
            updateToSheet1Template(pcmReader);
            updateToSheet2Template(pcmReader);
            updateToSheet3Template(pcmReader);
            updateToSheet4Template(pcmReader, photoFolderPath);
            updateToSheet5Template(pcmReader);
            try
            {
                wbTemplate.SaveToFile(path, ExcelVersion.Version2016);
            }
            catch
            {
                throw new Exception("Can not save Excel template file");
            }
        }

        #endregion

        #region pdf reader
        public static void readPdfFile(string path)
        {
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            try
            {
                listLogo = new Image[3];
                listImageSupport = new List<KeyValuePair<string, Image>>();
                doc.LoadFromFile(path);

                //get image
                getOverviewMap(capturePageImage(1, doc));
                getSpecificMap(capturePageImage(2, doc));
                getSupportMap(capturePageImage(2, doc));

                if (doc.Pages.Count > 0 && doc.Pages[0] != null)
                {
                    Image[] images = doc.Pages[0].ExtractImages();
                    getListLogo(images.ToList<Image>());

                    pdfNom = getTextWithLocation(doc.Pages[0], new Point(160, 158), new Point(385,173));
                }

                doc.Close();
            }
            catch
            {
                doc.Close();
                throw new Exception("Can not open pdf file");
            }
        }

        private static void getListLogo(List<Image> listImg)
        {
            listImg.CopyTo(0, listLogo, 0, 3);
        }

        private static void getOverviewMap(Image page1Image)
        {
            overViewMap = cropImage(page1Image, new Rectangle(821, 100, 721, 482));
        }

        private static void getSpecificMap(Image page2Image)
        {
            specificMap = cropImage(page2Image, new Rectangle(198, 90, 1370, 1011));
        }
        private static void getSupportMap(Image page2Image)
        {
            supportMap = cropImage(page2Image, new Rectangle(24, 100, 165, 1000));
        }

        #endregion

        #region excel reader
        private static void openExcelInput(string path)
        {
            try
            {
                wbInput = new Workbook();
                wbInput.LoadFromFile(path);
                setSheetNameExcelInput("Page de Garde");
                excelTemplateNom = sheetInput.Range["C8"].Text;
            }
            catch
            {
                throw new Exception("Can not open Excel template");
            }
        }
        private static void setSheetNameExcelInput(string name)
        {
            sheetInput = wbInput.Worksheets[name];
        }
        #endregion

        #region update image to template Excel
        private static void updateImagetoExcelInput()
        {
            //sheet 1
            try
            {
                setSheetNameExcelInput("Page de Garde");
                sheetInput.Pictures.Add(7, 11, overViewMap);
                if (listLogo[0] != null)
                {
                    Image img = imageResize(listLogo[0], 56 * listLogo[0].Width / listLogo[0].Height, 56);
                    sheetInput.Pictures.Add(1, 1, img);
                }
                if (listLogo[1] != null)
                {
                    Image img = imageResize(listLogo[1], 56 * listLogo[1].Width / listLogo[1].Height, 56);
                    sheetInput.Pictures.Add(1, 6, img);
                }
                if (listLogo[2] != null)
                {
                    Image img = imageResize(listLogo[2], 56 * listLogo[2].Width / listLogo[2].Height, 56);
                    sheetInput.Pictures.Add(1, 8, img);
                }

                //sheet2
                setSheetNameExcelInput("Plan moyenne échelle");
                if (supportMap != null)
                {
                    Image img = imageResize(supportMap, 797 * supportMap.Width / supportMap.Height, 797);
                    sheetInput.Pictures.Add(1, 1, img);
                }
                if (specificMap != null)
                {
                    Image img1 = imageResize(specificMap, 650 * specificMap.Width / specificMap.Height, 650);
                    sheetInput.Pictures.Add(5, 2, img1);
                }
            }
            catch
            {
                throw new Exception("Can not write photo to Excel file");
            }
        }

        private static void SaveExcelFile(string outputPath)
        {
            try
            {
                wbInput.SaveToFile(outputPath, ExcelVersion.Version2016);
            }
            catch
            {
                throw new Exception("Can not save Excel file");
            }
        }

        public static void exportExcelReport(string pdfPath, string excelInputPath, string outputPath)
        {
            readPdfFile(pdfPath);
            openExcelInput(excelInputPath);
            if(!pdfNom.Contains(excelTemplateNom))
            {
                DialogResult dialogResult = MessageBox.Show("The names of 2 file are different. Do you want to continue?", "Conflict File", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    updateImagetoExcelInput();
                    SaveExcelFile(outputPath);
                }
                else if (dialogResult == DialogResult.No)
                {
                    throw new Exception();
                }
            }
            else
            {
                updateImagetoExcelInput();
                SaveExcelFile(outputPath);
            }    
            
        }
        #endregion
    }
}
