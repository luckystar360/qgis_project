using LicenseActive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comacExport
{
    public partial class Active_License : Form
    {
        public string pathLicense { get; set; }
        public delegate void LicenseStateCallBack(bool stt);
        public event LicenseStateCallBack LicenseState;
        private string appName;

        public Active_License(String appName)
        {
            InitializeComponent();
            this.appName = appName;
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
                    if (pos.Y < 28)
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
        private void Active_License_Click(object sender, EventArgs e)
        {
   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(pathLicense))
                {
                    string textLicense = File.ReadAllText(pathLicense);
                    bool validLicense = ActiveLicense.ValidateLicense(textLicense, appName);
                    LicenseState(validLicense);
                }
                else
                    LicenseState(false);
            }
            catch
            {
                LicenseState(false);
                return;
            }
        }

        private void btnImportLicense_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog licenseFileDialog = new OpenFileDialog())
            {
                licenseFileDialog.Filter = "License|*.lic";
                licenseFileDialog.Title = "Import License";
                licenseFileDialog.Multiselect = false;
                //openFileDialog1.InitialDirectory = lastLogDir ?? Properties.Settings.Default.LogDir;

                if (licenseFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pathLicense = licenseFileDialog.FileName;
                        this.lbLicensePath.Text = pathLicense;

                    }
                    catch
                    {
                        MessageBox.Show("Can not import License File", "Error");
                    }

                    return;
                }

            }
        }


        //mouse event
        private void MouseEnterEvent(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.Gray;
        }

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.White;
        }
    }
}
