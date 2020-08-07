using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comacExport
{
    public partial class LicenseInforForm : Form
    {
        public  LicenseInforForm()
        {
            InitializeComponent();
        }

        static LicenseInforForm Instance;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void showForm(bool active, String UID, DateTime expireTime)
        {
            try
            {
                if (Instance != null && !Instance.IsDisposed) { }
                else
                    Instance = new LicenseInforForm();

                Instance.lbStateLicense.Text = active ? "VALID LICENSE" : (expireTime > DateTime.Now ? "INVALID LICENSE" : "EXPIRE LICENSE");

                Instance.lbStateLicense.ForeColor = active ? Color.Green : Color.Red;

                if (expireTime != DateTime.MinValue)
                {
                    Instance.lbUID.Text = UID;
                    Instance.lbExpireTime.Text = expireTime.ToString();
                }

                Instance.ShowDialog();
            }
            catch { }
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.Gray;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.White;
        }
    }
}
