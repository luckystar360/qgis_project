namespace comacExport
{
    partial class Active_License
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Label();
            this.btnActive = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLicensePath = new System.Windows.Forms.Label();
            this.btnImportLicense = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.titleTransparent1 = new comacExport.TitleTransparent();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Firebrick;
            this.btnCancel.Location = new System.Drawing.Point(298, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Exit";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.btnCancel.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // btnActive
            // 
            this.btnActive.BackColor = System.Drawing.Color.White;
            this.btnActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActive.Location = new System.Drawing.Point(384, 161);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(66, 22);
            this.btnActive.TabIndex = 1;
            this.btnActive.Text = "Activate";
            this.btnActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            this.btnActive.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.btnActive.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Import License File:";
            // 
            // lbLicensePath
            // 
            this.lbLicensePath.BackColor = System.Drawing.Color.White;
            this.lbLicensePath.Location = new System.Drawing.Point(20, 84);
            this.lbLicensePath.Name = "lbLicensePath";
            this.lbLicensePath.Size = new System.Drawing.Size(387, 20);
            this.lbLicensePath.TabIndex = 3;
            this.lbLicensePath.Text = "...";
            // 
            // btnImportLicense
            // 
            this.btnImportLicense.BackColor = System.Drawing.Color.White;
            this.btnImportLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnImportLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportLicense.Location = new System.Drawing.Point(418, 84);
            this.btnImportLicense.Name = "btnImportLicense";
            this.btnImportLicense.Size = new System.Drawing.Size(31, 22);
            this.btnImportLicense.TabIndex = 1;
            this.btnImportLicense.Text = "...";
            this.btnImportLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnImportLicense.Click += new System.EventHandler(this.btnImportLicense_Click);
            this.btnImportLicense.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
            this.btnImportLicense.MouseLeave += new System.EventHandler(this.MouseLeaveEvent);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(1, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 165);
            this.panel1.TabIndex = 5;
            // 
            // titleTransparent1
            // 
            this.titleTransparent1.BackColor = System.Drawing.Color.Silver;
            this.titleTransparent1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleTransparent1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTransparent1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.titleTransparent1.Location = new System.Drawing.Point(1, 1);
            this.titleTransparent1.Name = "titleTransparent1";
            this.titleTransparent1.Size = new System.Drawing.Size(461, 28);
            this.titleTransparent1.TabIndex = 4;
            this.titleTransparent1.Text = "Active License";
            this.titleTransparent1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Active_License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(463, 195);
            this.Controls.Add(this.titleTransparent1);
            this.Controls.Add(this.lbLicensePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImportLicense);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Active_License";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Active_License";
            this.Click += new System.EventHandler(this.Active_License_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label btnCancel;
        private System.Windows.Forms.Label btnActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbLicensePath;
        private System.Windows.Forms.Label btnImportLicense;
        private TitleTransparent titleTransparent1;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}