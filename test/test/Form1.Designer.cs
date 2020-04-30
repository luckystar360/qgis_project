namespace test
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtExport = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInsee_casd2 = new System.Windows.Forms.TextBox();
            this.txtInsee_casd1 = new System.Windows.Forms.TextBox();
            this.txtNumberEplans = new System.Windows.Forms.TextBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCommon = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtOperatingCorrespondent = new System.Windows.Forms.TextBox();
            this.cbbEnergyDistribute = new System.Windows.Forms.ComboBox();
            this.cbbOperator = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtDirSource = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Label();
            this.titleTransparent1 = new test.TitleTransparent();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Import COMAC file:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Export date:";
            // 
            // dtExport
            // 
            this.dtExport.Location = new System.Drawing.Point(125, 93);
            this.dtExport.Name = "dtExport";
            this.dtExport.Size = new System.Drawing.Size(200, 20);
            this.dtExport.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtInsee_casd2);
            this.groupBox1.Controls.Add(this.txtInsee_casd1);
            this.groupBox1.Controls.Add(this.txtNumberEplans);
            this.groupBox1.Controls.Add(this.txtReference);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtCommon);
            this.groupBox1.Controls.Add(this.txtStreet);
            this.groupBox1.Controls.Add(this.txtPhoneNumber);
            this.groupBox1.Controls.Add(this.txtOperatingCorrespondent);
            this.groupBox1.Controls.Add(this.cbbEnergyDistribute);
            this.groupBox1.Controls.Add(this.cbbOperator);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(26, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(890, 243);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PCM data";
            // 
            // txtInsee_casd2
            // 
            this.txtInsee_casd2.Location = new System.Drawing.Point(589, 192);
            this.txtInsee_casd2.Name = "txtInsee_casd2";
            this.txtInsee_casd2.Size = new System.Drawing.Size(271, 20);
            this.txtInsee_casd2.TabIndex = 2;
            this.txtInsee_casd2.TextChanged += new System.EventHandler(this.txtInsee_casd2_TextChanged);
            // 
            // txtInsee_casd1
            // 
            this.txtInsee_casd1.Location = new System.Drawing.Point(156, 193);
            this.txtInsee_casd1.Name = "txtInsee_casd1";
            this.txtInsee_casd1.Size = new System.Drawing.Size(271, 20);
            this.txtInsee_casd1.TabIndex = 2;
            this.txtInsee_casd1.TextChanged += new System.EventHandler(this.txtInsee_casd1_TextChanged);
            // 
            // txtNumberEplans
            // 
            this.txtNumberEplans.Location = new System.Drawing.Point(589, 158);
            this.txtNumberEplans.Name = "txtNumberEplans";
            this.txtNumberEplans.Size = new System.Drawing.Size(271, 20);
            this.txtNumberEplans.TabIndex = 2;
            this.txtNumberEplans.TextChanged += new System.EventHandler(this.txtNumberEplans_TextChanged);
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(156, 159);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(271, 20);
            this.txtReference.TabIndex = 2;
            this.txtReference.TextChanged += new System.EventHandler(this.txtReference_TextChanged);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(589, 124);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(271, 20);
            this.txtComment.TabIndex = 2;
            this.txtComment.TextChanged += new System.EventHandler(this.txtComment_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(156, 125);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(271, 20);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtCommon
            // 
            this.txtCommon.Location = new System.Drawing.Point(589, 90);
            this.txtCommon.Name = "txtCommon";
            this.txtCommon.Size = new System.Drawing.Size(271, 20);
            this.txtCommon.TabIndex = 2;
            this.txtCommon.TextChanged += new System.EventHandler(this.txtCommon_TextChanged);
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(156, 91);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(271, 20);
            this.txtStreet.TabIndex = 2;
            this.txtStreet.TextChanged += new System.EventHandler(this.txtStreet_TextChanged);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(589, 55);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(271, 20);
            this.txtPhoneNumber.TabIndex = 2;
            this.txtPhoneNumber.TextChanged += new System.EventHandler(this.txtPhoneNumber_TextChanged);
            // 
            // txtOperatingCorrespondent
            // 
            this.txtOperatingCorrespondent.Location = new System.Drawing.Point(156, 56);
            this.txtOperatingCorrespondent.Name = "txtOperatingCorrespondent";
            this.txtOperatingCorrespondent.Size = new System.Drawing.Size(271, 20);
            this.txtOperatingCorrespondent.TabIndex = 2;
            this.txtOperatingCorrespondent.TextChanged += new System.EventHandler(this.txtOperatingCorrespondent_TextChanged);
            // 
            // cbbEnergyDistribute
            // 
            this.cbbEnergyDistribute.BackColor = System.Drawing.Color.White;
            this.cbbEnergyDistribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEnergyDistribute.FormattingEnabled = true;
            this.cbbEnergyDistribute.Location = new System.Drawing.Point(589, 19);
            this.cbbEnergyDistribute.Name = "cbbEnergyDistribute";
            this.cbbEnergyDistribute.Size = new System.Drawing.Size(271, 21);
            this.cbbEnergyDistribute.TabIndex = 1;
            // 
            // cbbOperator
            // 
            this.cbbOperator.BackColor = System.Drawing.Color.White;
            this.cbbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOperator.Enabled = false;
            this.cbbOperator.FormattingEnabled = true;
            this.cbbOperator.Items.AddRange(new object[] {
            "ORANGE",
            "FIBRE 31"});
            this.cbbOperator.Location = new System.Drawing.Point(156, 20);
            this.cbbOperator.Name = "cbbOperator";
            this.cbbOperator.Size = new System.Drawing.Size(271, 21);
            this.cbbOperator.TabIndex = 1;
            this.cbbOperator.SelectedValueChanged += new System.EventHandler(this.cbbOperator_SelectedValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(472, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Energy distributor:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(472, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Phone number:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(472, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Common:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(472, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Comment:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(472, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Study number E-plans:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(472, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Insee, cadastre 2:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Insee, cadastre 1:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Reference:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Street:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Operating Correspondent:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Operator:";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.Enabled = false;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(832, 411);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtDirSource
            // 
            this.txtDirSource.BackColor = System.Drawing.Color.White;
            this.txtDirSource.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtDirSource.Location = new System.Drawing.Point(125, 64);
            this.txtDirSource.Name = "txtDirSource";
            this.txtDirSource.Size = new System.Drawing.Size(745, 15);
            this.txtDirSource.TabIndex = 6;
            this.txtDirSource.Text = "...";
            this.txtDirSource.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.txtDirSource.UseCompatibleTextRendering = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 430);
            this.panel1.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(905, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 28);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "X";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.Click += new System.EventHandler(this.btnExit_MouseClick);
            this.btnExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseDown);
            this.btnExit.MouseEnter += new System.EventHandler(this.btnExit_MouseEnter);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            // 
            // titleTransparent1
            // 
            this.titleTransparent1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.titleTransparent1.AutoSize = true;
            this.titleTransparent1.Font = new System.Drawing.Font("Palatino Linotype", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTransparent1.Location = new System.Drawing.Point(393, 3);
            this.titleTransparent1.Name = "titleTransparent1";
            this.titleTransparent1.Size = new System.Drawing.Size(170, 23);
            this.titleTransparent1.TabIndex = 9;
            this.titleTransparent1.Text = "COMAC Report Tool";
            this.titleTransparent1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.White;
            this.btnOpenFile.FlatAppearance.BorderSize = 0;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(875, 48);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(38, 38);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(941, 460);
            this.ControlBox = false;
            this.Controls.Add(this.titleTransparent1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtDirSource);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInsee_casd2;
        private System.Windows.Forms.TextBox txtInsee_casd1;
        private System.Windows.Forms.TextBox txtNumberEplans;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCommon;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtOperatingCorrespondent;
        private System.Windows.Forms.ComboBox cbbEnergyDistribute;
        private System.Windows.Forms.ComboBox cbbOperator;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label txtDirSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label btnExit;
        private TitleTransparent titleTransparent1;
    }
}

