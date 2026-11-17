namespace CertificateChecker
{
    partial class CA_Verification_UserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox_CertInfo2 = new System.Windows.Forms.GroupBox();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            button1 = new System.Windows.Forms.Button();
            button_DeleteCA = new System.Windows.Forms.Button();
            button_ImportCA = new System.Windows.Forms.Button();
            Label_SearchCA = new System.Windows.Forms.Label();
            TextBox_SearchCA = new System.Windows.Forms.TextBox();
            TreeView_CA = new System.Windows.Forms.TreeView();
            tabPage2 = new System.Windows.Forms.TabPage();
            listView_VerificationDetail = new System.Windows.Forms.ListView();
            columnComponent = new System.Windows.Forms.ColumnHeader("(none)");
            columnStatus = new System.Windows.Forms.ColumnHeader();
            columnDescription = new System.Windows.Forms.ColumnHeader();
            columnSource = new System.Windows.Forms.ColumnHeader();
            tabPage3 = new System.Windows.Forms.TabPage();
            listView_SecurityAnalysis = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader("(none)");
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            Button_Result_Recheck = new System.Windows.Forms.Button();
            PictureBox_Durum = new System.Windows.Forms.PictureBox();
            Label_Durum = new System.Windows.Forms.Label();
            Textbox_Durum = new System.Windows.Forms.TextBox();
            button_ocsp_recheck = new System.Windows.Forms.Button();
            button_aia_recheck = new System.Windows.Forms.Button();
            button_crl_recheck = new System.Windows.Forms.Button();
            button_ocsp_downloader = new System.Windows.Forms.Button();
            button_aia_downloader = new System.Windows.Forms.Button();
            button_crl_downloader = new System.Windows.Forms.Button();
            pictureBox_OCSP = new System.Windows.Forms.PictureBox();
            pictureBox_AIA = new System.Windows.Forms.PictureBox();
            pictureBox_CDP = new System.Windows.Forms.PictureBox();
            TextBox_AIA = new System.Windows.Forms.TextBox();
            TextBox_OCSP = new System.Windows.Forms.TextBox();
            label_OCSP = new System.Windows.Forms.Label();
            TextBox_CRL = new System.Windows.Forms.TextBox();
            label_CDP = new System.Windows.Forms.Label();
            label_AIA = new System.Windows.Forms.Label();
            groupBox_CertInfo1 = new System.Windows.Forms.GroupBox();
            TextBox_Thumbprint = new System.Windows.Forms.TextBox();
            label_Thumbprint = new System.Windows.Forms.Label();
            TextBox_Policy = new System.Windows.Forms.TextBox();
            label_SHA = new System.Windows.Forms.Label();
            TextBox_ski = new System.Windows.Forms.TextBox();
            Label_Ski = new System.Windows.Forms.Label();
            TextBox_aki = new System.Windows.Forms.TextBox();
            Label_Aki = new System.Windows.Forms.Label();
            TextBox_Publickey = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            Textbox_Constraints = new System.Windows.Forms.TextBox();
            Label_Constraints = new System.Windows.Forms.Label();
            Textbox_Verilen = new System.Windows.Forms.TextBox();
            Label_Verilen = new System.Windows.Forms.Label();
            Textbox_Usage = new System.Windows.Forms.TextBox();
            Label_Usage = new System.Windows.Forms.Label();
            Textbox_Bitis = new System.Windows.Forms.TextBox();
            Label_Bitis = new System.Windows.Forms.Label();
            Textbox_Serino = new System.Windows.Forms.TextBox();
            Textbox_Baslangic = new System.Windows.Forms.TextBox();
            Label_Baslangic = new System.Windows.Forms.Label();
            Label_Serino = new System.Windows.Forms.Label();
            Label_Algoritma = new System.Windows.Forms.Label();
            Textbox_Algoritma = new System.Windows.Forms.TextBox();
            Textbox_Veren = new System.Windows.Forms.TextBox();
            Label_Veren = new System.Windows.Forms.Label();
            groupBox_CertInfo2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_OCSP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_AIA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_CDP).BeginInit();
            groupBox_CertInfo1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_CertInfo2
            // 
            groupBox_CertInfo2.Controls.Add(tabControl1);
            groupBox_CertInfo2.Controls.Add(Button_Result_Recheck);
            groupBox_CertInfo2.Controls.Add(PictureBox_Durum);
            groupBox_CertInfo2.Controls.Add(Label_Durum);
            groupBox_CertInfo2.Controls.Add(Textbox_Durum);
            groupBox_CertInfo2.Font = new System.Drawing.Font("Arial", 9.75F);
            groupBox_CertInfo2.Location = new System.Drawing.Point(612, 3);
            groupBox_CertInfo2.Name = "groupBox_CertInfo2";
            groupBox_CertInfo2.Size = new System.Drawing.Size(605, 715);
            groupBox_CertInfo2.TabIndex = 54;
            groupBox_CertInfo2.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tabControl1.Location = new System.Drawing.Point(6, 15);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(593, 410);
            tabControl1.TabIndex = 50;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(button_DeleteCA);
            tabPage1.Controls.Add(button_ImportCA);
            tabPage1.Controls.Add(Label_SearchCA);
            tabPage1.Controls.Add(TextBox_SearchCA);
            tabPage1.Controls.Add(TreeView_CA);
            tabPage1.Location = new System.Drawing.Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(585, 377);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Certificate Authorities";
            tabPage1.ToolTipText = "Displays the certificate chain from the end-entity certificate to the trusted root CA.";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button1.ImageIndex = 4;
            button1.Location = new System.Drawing.Point(549, 8);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(30, 30);
            button1.TabIndex = 53;
            button1.UseVisualStyleBackColor = false;
            // 
            // button_DeleteCA
            // 
            button_DeleteCA.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_DeleteCA.ImageIndex = 4;
            button_DeleteCA.Location = new System.Drawing.Point(513, 8);
            button_DeleteCA.Name = "button_DeleteCA";
            button_DeleteCA.Size = new System.Drawing.Size(30, 30);
            button_DeleteCA.TabIndex = 52;
            button_DeleteCA.UseVisualStyleBackColor = false;
            // 
            // button_ImportCA
            // 
            button_ImportCA.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_ImportCA.ImageIndex = 4;
            button_ImportCA.Location = new System.Drawing.Point(477, 8);
            button_ImportCA.Name = "button_ImportCA";
            button_ImportCA.Size = new System.Drawing.Size(30, 30);
            button_ImportCA.TabIndex = 51;
            button_ImportCA.UseVisualStyleBackColor = false;
            button_ImportCA.Click += button_ImportCA_Click;
            // 
            // Label_SearchCA
            // 
            Label_SearchCA.AutoSize = true;
            Label_SearchCA.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_SearchCA.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_SearchCA.Location = new System.Drawing.Point(3, 10);
            Label_SearchCA.Name = "Label_SearchCA";
            Label_SearchCA.Size = new System.Drawing.Size(94, 21);
            Label_SearchCA.TabIndex = 30;
            Label_SearchCA.Text = "Search CA";
            // 
            // TextBox_SearchCA
            // 
            TextBox_SearchCA.BackColor = System.Drawing.Color.White;
            TextBox_SearchCA.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_SearchCA.Location = new System.Drawing.Point(109, 10);
            TextBox_SearchCA.Name = "TextBox_SearchCA";
            TextBox_SearchCA.Size = new System.Drawing.Size(362, 26);
            TextBox_SearchCA.TabIndex = 29;
            TextBox_SearchCA.TextChanged += TextBox_SearchCA_TextChanged;
            // 
            // TreeView_CA
            // 
            TreeView_CA.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TreeView_CA.Location = new System.Drawing.Point(3, 48);
            TreeView_CA.Name = "TreeView_CA";
            TreeView_CA.Size = new System.Drawing.Size(579, 325);
            TreeView_CA.TabIndex = 28;
            TreeView_CA.AfterSelect += TreeView_CA_AfterSelect;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(listView_VerificationDetail);
            tabPage2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tabPage2.Location = new System.Drawing.Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(585, 377);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Verification Detail";
            tabPage2.ToolTipText = "Displays detailed certificate verification results.";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView_VerificationDetail
            // 
            listView_VerificationDetail.Activation = System.Windows.Forms.ItemActivation.OneClick;
            listView_VerificationDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnComponent, columnStatus, columnDescription, columnSource });
            listView_VerificationDetail.FullRowSelect = true;
            listView_VerificationDetail.GridLines = true;
            listView_VerificationDetail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listView_VerificationDetail.Location = new System.Drawing.Point(3, 3);
            listView_VerificationDetail.Name = "listView_VerificationDetail";
            listView_VerificationDetail.Size = new System.Drawing.Size(579, 337);
            listView_VerificationDetail.TabIndex = 0;
            listView_VerificationDetail.UseCompatibleStateImageBehavior = false;
            listView_VerificationDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnComponent
            // 
            columnComponent.Text = "Component";
            columnComponent.Width = 110;
            // 
            // columnStatus
            // 
            columnStatus.Text = "Status";
            columnStatus.Width = 90;
            // 
            // columnDescription
            // 
            columnDescription.Text = "Description";
            columnDescription.Width = 260;
            // 
            // columnSource
            // 
            columnSource.Text = "Source";
            columnSource.Width = 400;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(listView_SecurityAnalysis);
            tabPage3.Location = new System.Drawing.Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(585, 377);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Security Analysis";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // listView_SecurityAnalysis
            // 
            listView_SecurityAnalysis.Activation = System.Windows.Forms.ItemActivation.OneClick;
            listView_SecurityAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listView_SecurityAnalysis.FullRowSelect = true;
            listView_SecurityAnalysis.GridLines = true;
            listView_SecurityAnalysis.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listView_SecurityAnalysis.Location = new System.Drawing.Point(3, 3);
            listView_SecurityAnalysis.Name = "listView_SecurityAnalysis";
            listView_SecurityAnalysis.Size = new System.Drawing.Size(579, 337);
            listView_SecurityAnalysis.TabIndex = 1;
            listView_SecurityAnalysis.UseCompatibleStateImageBehavior = false;
            listView_SecurityAnalysis.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Component";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Status";
            columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Description";
            columnHeader3.Width = 500;
            // 
            // Button_Result_Recheck
            // 
            Button_Result_Recheck.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            Button_Result_Recheck.Enabled = false;
            Button_Result_Recheck.ImageIndex = 4;
            Button_Result_Recheck.Location = new System.Drawing.Point(83, 639);
            Button_Result_Recheck.Name = "Button_Result_Recheck";
            Button_Result_Recheck.Size = new System.Drawing.Size(30, 30);
            Button_Result_Recheck.TabIndex = 48;
            Button_Result_Recheck.UseVisualStyleBackColor = false;
            // 
            // PictureBox_Durum
            // 
            PictureBox_Durum.BackColor = System.Drawing.Color.Transparent;
            PictureBox_Durum.Location = new System.Drawing.Point(88, 673);
            PictureBox_Durum.Name = "PictureBox_Durum";
            PictureBox_Durum.Size = new System.Drawing.Size(20, 20);
            PictureBox_Durum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            PictureBox_Durum.TabIndex = 0;
            PictureBox_Durum.TabStop = false;
            // 
            // Label_Durum
            // 
            Label_Durum.AutoSize = true;
            Label_Durum.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Durum.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Durum.Location = new System.Drawing.Point(8, 642);
            Label_Durum.Name = "Label_Durum";
            Label_Durum.Size = new System.Drawing.Size(56, 21);
            Label_Durum.TabIndex = 25;
            Label_Durum.Text = "Result";
            // 
            // Textbox_Durum
            // 
            Textbox_Durum.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Durum.Location = new System.Drawing.Point(119, 639);
            Textbox_Durum.Multiline = true;
            Textbox_Durum.Name = "Textbox_Durum";
            Textbox_Durum.ReadOnly = true;
            Textbox_Durum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            Textbox_Durum.Size = new System.Drawing.Size(440, 70);
            Textbox_Durum.TabIndex = 27;
            // 
            // button_ocsp_recheck
            // 
            button_ocsp_recheck.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_ocsp_recheck.Enabled = false;
            button_ocsp_recheck.ImageIndex = 4;
            button_ocsp_recheck.Location = new System.Drawing.Point(83, 649);
            button_ocsp_recheck.Name = "button_ocsp_recheck";
            button_ocsp_recheck.Size = new System.Drawing.Size(30, 30);
            button_ocsp_recheck.TabIndex = 45;
            button_ocsp_recheck.UseVisualStyleBackColor = false;
            // 
            // button_aia_recheck
            // 
            button_aia_recheck.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_aia_recheck.Enabled = false;
            button_aia_recheck.ImageIndex = 4;
            button_aia_recheck.Location = new System.Drawing.Point(83, 583);
            button_aia_recheck.Name = "button_aia_recheck";
            button_aia_recheck.Size = new System.Drawing.Size(30, 30);
            button_aia_recheck.TabIndex = 44;
            button_aia_recheck.UseVisualStyleBackColor = false;
            // 
            // button_crl_recheck
            // 
            button_crl_recheck.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_crl_recheck.Enabled = false;
            button_crl_recheck.ImageIndex = 4;
            button_crl_recheck.Location = new System.Drawing.Point(83, 517);
            button_crl_recheck.Name = "button_crl_recheck";
            button_crl_recheck.Size = new System.Drawing.Size(30, 30);
            button_crl_recheck.TabIndex = 43;
            button_crl_recheck.UseVisualStyleBackColor = false;
            // 
            // button_ocsp_downloader
            // 
            button_ocsp_downloader.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_ocsp_downloader.Enabled = false;
            button_ocsp_downloader.ImageIndex = 3;
            button_ocsp_downloader.Location = new System.Drawing.Point(53, 649);
            button_ocsp_downloader.Name = "button_ocsp_downloader";
            button_ocsp_downloader.Size = new System.Drawing.Size(30, 30);
            button_ocsp_downloader.TabIndex = 42;
            button_ocsp_downloader.UseVisualStyleBackColor = false;
            // 
            // button_aia_downloader
            // 
            button_aia_downloader.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_aia_downloader.Enabled = false;
            button_aia_downloader.ImageIndex = 3;
            button_aia_downloader.Location = new System.Drawing.Point(53, 582);
            button_aia_downloader.Name = "button_aia_downloader";
            button_aia_downloader.Size = new System.Drawing.Size(30, 30);
            button_aia_downloader.TabIndex = 41;
            button_aia_downloader.UseVisualStyleBackColor = false;
            // 
            // button_crl_downloader
            // 
            button_crl_downloader.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_crl_downloader.Enabled = false;
            button_crl_downloader.ImageIndex = 3;
            button_crl_downloader.Location = new System.Drawing.Point(53, 517);
            button_crl_downloader.Name = "button_crl_downloader";
            button_crl_downloader.Size = new System.Drawing.Size(30, 30);
            button_crl_downloader.TabIndex = 40;
            button_crl_downloader.UseVisualStyleBackColor = false;
            // 
            // pictureBox_OCSP
            // 
            pictureBox_OCSP.BackColor = System.Drawing.Color.Transparent;
            pictureBox_OCSP.Location = new System.Drawing.Point(93, 685);
            pictureBox_OCSP.Name = "pictureBox_OCSP";
            pictureBox_OCSP.Size = new System.Drawing.Size(20, 20);
            pictureBox_OCSP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox_OCSP.TabIndex = 39;
            pictureBox_OCSP.TabStop = false;
            // 
            // pictureBox_AIA
            // 
            pictureBox_AIA.BackColor = System.Drawing.Color.Transparent;
            pictureBox_AIA.Location = new System.Drawing.Point(93, 619);
            pictureBox_AIA.Name = "pictureBox_AIA";
            pictureBox_AIA.Size = new System.Drawing.Size(20, 20);
            pictureBox_AIA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox_AIA.TabIndex = 38;
            pictureBox_AIA.TabStop = false;
            // 
            // pictureBox_CDP
            // 
            pictureBox_CDP.BackColor = System.Drawing.Color.Transparent;
            pictureBox_CDP.Location = new System.Drawing.Point(93, 553);
            pictureBox_CDP.Name = "pictureBox_CDP";
            pictureBox_CDP.Size = new System.Drawing.Size(20, 20);
            pictureBox_CDP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox_CDP.TabIndex = 37;
            pictureBox_CDP.TabStop = false;
            // 
            // TextBox_AIA
            // 
            TextBox_AIA.BackColor = System.Drawing.SystemColors.Control;
            TextBox_AIA.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_AIA.Location = new System.Drawing.Point(119, 583);
            TextBox_AIA.Multiline = true;
            TextBox_AIA.Name = "TextBox_AIA";
            TextBox_AIA.ReadOnly = true;
            TextBox_AIA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TextBox_AIA.Size = new System.Drawing.Size(475, 60);
            TextBox_AIA.TabIndex = 30;
            // 
            // TextBox_OCSP
            // 
            TextBox_OCSP.BackColor = System.Drawing.SystemColors.Control;
            TextBox_OCSP.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_OCSP.Location = new System.Drawing.Point(119, 649);
            TextBox_OCSP.Multiline = true;
            TextBox_OCSP.Name = "TextBox_OCSP";
            TextBox_OCSP.ReadOnly = true;
            TextBox_OCSP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TextBox_OCSP.Size = new System.Drawing.Size(475, 60);
            TextBox_OCSP.TabIndex = 31;
            // 
            // label_OCSP
            // 
            label_OCSP.AutoSize = true;
            label_OCSP.Font = new System.Drawing.Font("Century Gothic", 12F);
            label_OCSP.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label_OCSP.Location = new System.Drawing.Point(2, 649);
            label_OCSP.Name = "label_OCSP";
            label_OCSP.Size = new System.Drawing.Size(51, 21);
            label_OCSP.TabIndex = 32;
            label_OCSP.Text = "Ocsp";
            // 
            // TextBox_CRL
            // 
            TextBox_CRL.BackColor = System.Drawing.SystemColors.Control;
            TextBox_CRL.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_CRL.Location = new System.Drawing.Point(119, 517);
            TextBox_CRL.Multiline = true;
            TextBox_CRL.Name = "TextBox_CRL";
            TextBox_CRL.ReadOnly = true;
            TextBox_CRL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TextBox_CRL.Size = new System.Drawing.Size(475, 60);
            TextBox_CRL.TabIndex = 29;
            // 
            // label_CDP
            // 
            label_CDP.AutoSize = true;
            label_CDP.Font = new System.Drawing.Font("Century Gothic", 12F);
            label_CDP.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label_CDP.Location = new System.Drawing.Point(5, 517);
            label_CDP.Name = "label_CDP";
            label_CDP.Size = new System.Drawing.Size(31, 21);
            label_CDP.TabIndex = 9;
            label_CDP.Text = "Crl";
            // 
            // label_AIA
            // 
            label_AIA.AutoSize = true;
            label_AIA.Font = new System.Drawing.Font("Century Gothic", 12F);
            label_AIA.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label_AIA.Location = new System.Drawing.Point(5, 583);
            label_AIA.Name = "label_AIA";
            label_AIA.Size = new System.Drawing.Size(37, 21);
            label_AIA.TabIndex = 8;
            label_AIA.Text = "Aia";
            // 
            // groupBox_CertInfo1
            // 
            groupBox_CertInfo1.Controls.Add(TextBox_Thumbprint);
            groupBox_CertInfo1.Controls.Add(label_Thumbprint);
            groupBox_CertInfo1.Controls.Add(button_ocsp_recheck);
            groupBox_CertInfo1.Controls.Add(TextBox_Policy);
            groupBox_CertInfo1.Controls.Add(button_aia_recheck);
            groupBox_CertInfo1.Controls.Add(label_SHA);
            groupBox_CertInfo1.Controls.Add(button_crl_recheck);
            groupBox_CertInfo1.Controls.Add(TextBox_ski);
            groupBox_CertInfo1.Controls.Add(button_ocsp_downloader);
            groupBox_CertInfo1.Controls.Add(Label_Ski);
            groupBox_CertInfo1.Controls.Add(button_aia_downloader);
            groupBox_CertInfo1.Controls.Add(TextBox_aki);
            groupBox_CertInfo1.Controls.Add(button_crl_downloader);
            groupBox_CertInfo1.Controls.Add(Label_Aki);
            groupBox_CertInfo1.Controls.Add(pictureBox_OCSP);
            groupBox_CertInfo1.Controls.Add(TextBox_Publickey);
            groupBox_CertInfo1.Controls.Add(pictureBox_AIA);
            groupBox_CertInfo1.Controls.Add(label5);
            groupBox_CertInfo1.Controls.Add(pictureBox_CDP);
            groupBox_CertInfo1.Controls.Add(Textbox_Constraints);
            groupBox_CertInfo1.Controls.Add(TextBox_AIA);
            groupBox_CertInfo1.Controls.Add(Label_Constraints);
            groupBox_CertInfo1.Controls.Add(TextBox_OCSP);
            groupBox_CertInfo1.Controls.Add(Textbox_Verilen);
            groupBox_CertInfo1.Controls.Add(Label_Verilen);
            groupBox_CertInfo1.Controls.Add(label_OCSP);
            groupBox_CertInfo1.Controls.Add(Textbox_Usage);
            groupBox_CertInfo1.Controls.Add(Label_Usage);
            groupBox_CertInfo1.Controls.Add(Textbox_Bitis);
            groupBox_CertInfo1.Controls.Add(TextBox_CRL);
            groupBox_CertInfo1.Controls.Add(label_CDP);
            groupBox_CertInfo1.Controls.Add(Label_Bitis);
            groupBox_CertInfo1.Controls.Add(label_AIA);
            groupBox_CertInfo1.Controls.Add(Textbox_Serino);
            groupBox_CertInfo1.Controls.Add(Textbox_Baslangic);
            groupBox_CertInfo1.Controls.Add(Label_Baslangic);
            groupBox_CertInfo1.Controls.Add(Label_Serino);
            groupBox_CertInfo1.Controls.Add(Label_Algoritma);
            groupBox_CertInfo1.Controls.Add(Textbox_Algoritma);
            groupBox_CertInfo1.Controls.Add(Textbox_Veren);
            groupBox_CertInfo1.Controls.Add(Label_Veren);
            groupBox_CertInfo1.Font = new System.Drawing.Font("Arial", 9.75F);
            groupBox_CertInfo1.Location = new System.Drawing.Point(4, 3);
            groupBox_CertInfo1.Name = "groupBox_CertInfo1";
            groupBox_CertInfo1.Size = new System.Drawing.Size(605, 715);
            groupBox_CertInfo1.TabIndex = 53;
            groupBox_CertInfo1.TabStop = false;
            // 
            // TextBox_Thumbprint
            // 
            TextBox_Thumbprint.BackColor = System.Drawing.SystemColors.Control;
            TextBox_Thumbprint.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_Thumbprint.Location = new System.Drawing.Point(120, 391);
            TextBox_Thumbprint.Name = "TextBox_Thumbprint";
            TextBox_Thumbprint.ReadOnly = true;
            TextBox_Thumbprint.Size = new System.Drawing.Size(476, 26);
            TextBox_Thumbprint.TabIndex = 37;
            // 
            // label_Thumbprint
            // 
            label_Thumbprint.AutoSize = true;
            label_Thumbprint.Font = new System.Drawing.Font("Century Gothic", 12F);
            label_Thumbprint.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label_Thumbprint.Location = new System.Drawing.Point(6, 391);
            label_Thumbprint.Name = "label_Thumbprint";
            label_Thumbprint.Size = new System.Drawing.Size(100, 21);
            label_Thumbprint.TabIndex = 38;
            label_Thumbprint.Text = "Thumbprint";
            // 
            // TextBox_Policy
            // 
            TextBox_Policy.BackColor = System.Drawing.SystemColors.Control;
            TextBox_Policy.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_Policy.Location = new System.Drawing.Point(120, 423);
            TextBox_Policy.Multiline = true;
            TextBox_Policy.Name = "TextBox_Policy";
            TextBox_Policy.ReadOnly = true;
            TextBox_Policy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TextBox_Policy.Size = new System.Drawing.Size(476, 88);
            TextBox_Policy.TabIndex = 35;
            // 
            // label_SHA
            // 
            label_SHA.AutoSize = true;
            label_SHA.Font = new System.Drawing.Font("Century Gothic", 12F);
            label_SHA.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label_SHA.Location = new System.Drawing.Point(8, 423);
            label_SHA.Name = "label_SHA";
            label_SHA.Size = new System.Drawing.Size(53, 21);
            label_SHA.TabIndex = 36;
            label_SHA.Text = "Policy";
            // 
            // TextBox_ski
            // 
            TextBox_ski.BackColor = System.Drawing.SystemColors.Control;
            TextBox_ski.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_ski.Location = new System.Drawing.Point(120, 199);
            TextBox_ski.Name = "TextBox_ski";
            TextBox_ski.ReadOnly = true;
            TextBox_ski.Size = new System.Drawing.Size(476, 26);
            TextBox_ski.TabIndex = 18;
            // 
            // Label_Ski
            // 
            Label_Ski.AutoSize = true;
            Label_Ski.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Ski.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Ski.Location = new System.Drawing.Point(8, 199);
            Label_Ski.Name = "Label_Ski";
            Label_Ski.Size = new System.Drawing.Size(29, 21);
            Label_Ski.TabIndex = 34;
            Label_Ski.Text = "Ski";
            // 
            // TextBox_aki
            // 
            TextBox_aki.BackColor = System.Drawing.SystemColors.Control;
            TextBox_aki.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_aki.Location = new System.Drawing.Point(120, 167);
            TextBox_aki.Name = "TextBox_aki";
            TextBox_aki.ReadOnly = true;
            TextBox_aki.Size = new System.Drawing.Size(476, 26);
            TextBox_aki.TabIndex = 17;
            // 
            // Label_Aki
            // 
            Label_Aki.AutoSize = true;
            Label_Aki.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Aki.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Aki.Location = new System.Drawing.Point(6, 167);
            Label_Aki.Name = "Label_Aki";
            Label_Aki.Size = new System.Drawing.Size(34, 21);
            Label_Aki.TabIndex = 32;
            Label_Aki.Text = "Aki";
            // 
            // TextBox_Publickey
            // 
            TextBox_Publickey.BackColor = System.Drawing.SystemColors.Control;
            TextBox_Publickey.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_Publickey.Location = new System.Drawing.Point(396, 359);
            TextBox_Publickey.Name = "TextBox_Publickey";
            TextBox_Publickey.ReadOnly = true;
            TextBox_Publickey.Size = new System.Drawing.Size(200, 26);
            TextBox_Publickey.TabIndex = 26;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Century Gothic", 12F);
            label5.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label5.Location = new System.Drawing.Point(337, 359);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(37, 21);
            label5.TabIndex = 30;
            label5.Text = "Key";
            // 
            // Textbox_Constraints
            // 
            Textbox_Constraints.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Constraints.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Constraints.Location = new System.Drawing.Point(120, 295);
            Textbox_Constraints.Name = "Textbox_Constraints";
            Textbox_Constraints.ReadOnly = true;
            Textbox_Constraints.Size = new System.Drawing.Size(476, 26);
            Textbox_Constraints.TabIndex = 22;
            // 
            // Label_Constraints
            // 
            Label_Constraints.AutoSize = true;
            Label_Constraints.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Constraints.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Constraints.Location = new System.Drawing.Point(6, 295);
            Label_Constraints.Name = "Label_Constraints";
            Label_Constraints.Size = new System.Drawing.Size(98, 21);
            Label_Constraints.TabIndex = 29;
            Label_Constraints.Text = "Constraints";
            // 
            // Textbox_Verilen
            // 
            Textbox_Verilen.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Verilen.Location = new System.Drawing.Point(120, 15);
            Textbox_Verilen.Multiline = true;
            Textbox_Verilen.Name = "Textbox_Verilen";
            Textbox_Verilen.ReadOnly = true;
            Textbox_Verilen.Size = new System.Drawing.Size(476, 70);
            Textbox_Verilen.TabIndex = 16;
            // 
            // Label_Verilen
            // 
            Label_Verilen.AutoSize = true;
            Label_Verilen.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Verilen.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Verilen.Location = new System.Drawing.Point(6, 15);
            Label_Verilen.Name = "Label_Verilen";
            Label_Verilen.Size = new System.Drawing.Size(69, 21);
            Label_Verilen.TabIndex = 8;
            Label_Verilen.Text = "Subject";
            // 
            // Textbox_Usage
            // 
            Textbox_Usage.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Usage.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Usage.Location = new System.Drawing.Point(120, 263);
            Textbox_Usage.Name = "Textbox_Usage";
            Textbox_Usage.ReadOnly = true;
            Textbox_Usage.Size = new System.Drawing.Size(476, 26);
            Textbox_Usage.TabIndex = 21;
            // 
            // Label_Usage
            // 
            Label_Usage.AutoSize = true;
            Label_Usage.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Usage.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Usage.Location = new System.Drawing.Point(8, 263);
            Label_Usage.Name = "Label_Usage";
            Label_Usage.Size = new System.Drawing.Size(90, 21);
            Label_Usage.TabIndex = 27;
            Label_Usage.Text = "Key Usage";
            // 
            // Textbox_Bitis
            // 
            Textbox_Bitis.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Bitis.Location = new System.Drawing.Point(396, 327);
            Textbox_Bitis.Name = "Textbox_Bitis";
            Textbox_Bitis.ReadOnly = true;
            Textbox_Bitis.Size = new System.Drawing.Size(200, 26);
            Textbox_Bitis.TabIndex = 24;
            // 
            // Label_Bitis
            // 
            Label_Bitis.AutoSize = true;
            Label_Bitis.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Bitis.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Bitis.Location = new System.Drawing.Point(337, 327);
            Label_Bitis.Name = "Label_Bitis";
            Label_Bitis.Size = new System.Drawing.Size(40, 21);
            Label_Bitis.TabIndex = 23;
            Label_Bitis.Text = "End";
            // 
            // Textbox_Serino
            // 
            Textbox_Serino.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Serino.Location = new System.Drawing.Point(120, 231);
            Textbox_Serino.Name = "Textbox_Serino";
            Textbox_Serino.ReadOnly = true;
            Textbox_Serino.Size = new System.Drawing.Size(476, 26);
            Textbox_Serino.TabIndex = 19;
            // 
            // Textbox_Baslangic
            // 
            Textbox_Baslangic.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Baslangic.Location = new System.Drawing.Point(119, 327);
            Textbox_Baslangic.Name = "Textbox_Baslangic";
            Textbox_Baslangic.ReadOnly = true;
            Textbox_Baslangic.Size = new System.Drawing.Size(200, 26);
            Textbox_Baslangic.TabIndex = 23;
            // 
            // Label_Baslangic
            // 
            Label_Baslangic.AutoSize = true;
            Label_Baslangic.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Baslangic.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Baslangic.Location = new System.Drawing.Point(8, 327);
            Label_Baslangic.Name = "Label_Baslangic";
            Label_Baslangic.Size = new System.Drawing.Size(48, 21);
            Label_Baslangic.TabIndex = 10;
            Label_Baslangic.Text = "Start";
            // 
            // Label_Serino
            // 
            Label_Serino.AutoSize = true;
            Label_Serino.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Serino.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Serino.Location = new System.Drawing.Point(8, 231);
            Label_Serino.Name = "Label_Serino";
            Label_Serino.Size = new System.Drawing.Size(50, 21);
            Label_Serino.TabIndex = 19;
            Label_Serino.Text = "Serial";
            // 
            // Label_Algoritma
            // 
            Label_Algoritma.AutoSize = true;
            Label_Algoritma.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Algoritma.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Algoritma.Location = new System.Drawing.Point(6, 359);
            Label_Algoritma.Name = "Label_Algoritma";
            Label_Algoritma.Size = new System.Drawing.Size(87, 21);
            Label_Algoritma.TabIndex = 22;
            Label_Algoritma.Text = "Algorithm";
            // 
            // Textbox_Algoritma
            // 
            Textbox_Algoritma.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Algoritma.Location = new System.Drawing.Point(119, 359);
            Textbox_Algoritma.Name = "Textbox_Algoritma";
            Textbox_Algoritma.ReadOnly = true;
            Textbox_Algoritma.Size = new System.Drawing.Size(200, 26);
            Textbox_Algoritma.TabIndex = 25;
            // 
            // Textbox_Veren
            // 
            Textbox_Veren.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Veren.Location = new System.Drawing.Point(120, 91);
            Textbox_Veren.Multiline = true;
            Textbox_Veren.Name = "Textbox_Veren";
            Textbox_Veren.ReadOnly = true;
            Textbox_Veren.Size = new System.Drawing.Size(476, 70);
            Textbox_Veren.TabIndex = 15;
            // 
            // Label_Veren
            // 
            Label_Veren.AutoSize = true;
            Label_Veren.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Veren.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Veren.Location = new System.Drawing.Point(4, 91);
            Label_Veren.Name = "Label_Veren";
            Label_Veren.Size = new System.Drawing.Size(52, 21);
            Label_Veren.TabIndex = 9;
            Label_Veren.Text = "Issuer";
            // 
            // CA_Verification_UserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_CertInfo2);
            Controls.Add(groupBox_CertInfo1);
            Name = "CA_Verification_UserControl";
            Size = new System.Drawing.Size(1220, 720);
            groupBox_CertInfo2.ResumeLayout(false);
            groupBox_CertInfo2.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_OCSP).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_AIA).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_CDP).EndInit();
            groupBox_CertInfo1.ResumeLayout(false);
            groupBox_CertInfo1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_CertInfo2;
        private System.Windows.Forms.Button Button_Result_Recheck;
        private System.Windows.Forms.TreeView TreeView_CA;
        private System.Windows.Forms.Button button_ocsp_recheck;
        private System.Windows.Forms.Button button_aia_recheck;
        private System.Windows.Forms.Button button_crl_recheck;
        private System.Windows.Forms.Button button_ocsp_downloader;
        private System.Windows.Forms.Button button_aia_downloader;
        private System.Windows.Forms.Button button_crl_downloader;
        private System.Windows.Forms.PictureBox pictureBox_OCSP;
        private System.Windows.Forms.PictureBox pictureBox_AIA;
        private System.Windows.Forms.PictureBox pictureBox_CDP;
        private System.Windows.Forms.TextBox TextBox_AIA;
        private System.Windows.Forms.TextBox TextBox_OCSP;
        private System.Windows.Forms.PictureBox PictureBox_Durum;
        private System.Windows.Forms.Label label_OCSP;
        private System.Windows.Forms.Label Label_Durum;
        private System.Windows.Forms.TextBox Textbox_Durum;
        private System.Windows.Forms.TextBox TextBox_CRL;
        private System.Windows.Forms.Label label_CDP;
        private System.Windows.Forms.Label label_AIA;
        private System.Windows.Forms.GroupBox groupBox_CertInfo1;
        private System.Windows.Forms.TextBox TextBox_Thumbprint;
        private System.Windows.Forms.Label label_Thumbprint;
        private System.Windows.Forms.TextBox TextBox_Policy;
        private System.Windows.Forms.Label label_SHA;
        private System.Windows.Forms.TextBox TextBox_ski;
        private System.Windows.Forms.Label Label_Ski;
        private System.Windows.Forms.TextBox TextBox_aki;
        private System.Windows.Forms.Label Label_Aki;
        private System.Windows.Forms.TextBox TextBox_Publickey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Textbox_Constraints;
        private System.Windows.Forms.Label Label_Constraints;
        private System.Windows.Forms.TextBox Textbox_Verilen;
        private System.Windows.Forms.Label Label_Verilen;
        private System.Windows.Forms.TextBox Textbox_Usage;
        private System.Windows.Forms.Label Label_Usage;
        private System.Windows.Forms.TextBox Textbox_Bitis;
        private System.Windows.Forms.Label Label_Bitis;
        private System.Windows.Forms.TextBox Textbox_Serino;
        private System.Windows.Forms.TextBox Textbox_Baslangic;
        private System.Windows.Forms.Label Label_Baslangic;
        private System.Windows.Forms.Label Label_Serino;
        private System.Windows.Forms.Label Label_Algoritma;
        private System.Windows.Forms.TextBox Textbox_Algoritma;
        private System.Windows.Forms.TextBox Textbox_Veren;
        private System.Windows.Forms.Label Label_Veren;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView_VerificationDetail;
        private System.Windows.Forms.ColumnHeader columnComponent;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.ColumnHeader columnSource;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView_SecurityAnalysis;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label Label_SearchCA;
        private System.Windows.Forms.TextBox TextBox_SearchCA;
        private System.Windows.Forms.Button button_DeleteCA;
        private System.Windows.Forms.Button button_ImportCA;
        private System.Windows.Forms.Button button1;
    }
}
