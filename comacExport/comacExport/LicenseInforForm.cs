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
            if (Instance != null && !Instance.IsDisposed) { }
            else
                Instance = new LicenseInforForm();

            Instance.lbStateLicense.Text = active ? (expireTime > DateTime.Now ? "VALID LICENSE" : "EXPIRE LICENSE") : "INVALID LICENSE";

            Instance.lbStateLicense.ForeColor = active ? Color.Green : Color.Red;

            if (active)
            {
                Instance.lbUID.Text = UID;
                Instance.lbExpireTime.Text = expireTime.ToString();
            }    

            Instance.ShowDialog();
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
