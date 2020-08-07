using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGen
{
    public partial class KeyGenerator : Form
    {
        public KeyGenerator()
        {
            InitializeComponent();
        }

        public string appName = "trading_fx_standard";
        private static KeyGenerator Instance;

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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(appName))
                txtUID.Text = HardwareInfo.GenerateUID(appName);
            else
                MessageBox.Show("Can not create Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnGenerate_MouseEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.Gray;
        }

        private void btnGenerate_MouseLeave(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            lb.BackColor = Color.White;
        }

        public static void showForm()
        {
            try
            {
                if (Instance != null && !Instance.IsDisposed) { }
                else
                    Instance = new KeyGenerator();

                Instance.ShowDialog();
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Instance.Close();
            Instance.Dispose();
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
