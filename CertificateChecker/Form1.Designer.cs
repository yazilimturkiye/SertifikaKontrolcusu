
namespace CertificateChecker
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ımageList1 = new System.Windows.Forms.ImageList(components);
            Label_Baslik = new System.Windows.Forms.Label();
            Label_AltBaslik = new System.Windows.Forms.Label();
            ımageList2 = new System.Windows.Forms.ImageList(components);
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            toolStripMenuItem_App = new System.Windows.Forms.ToolStripMenuItem();
            ExitApp_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Verification = new System.Windows.Forms.ToolStripMenuItem();
            FILE_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SSLTLS_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            PEM_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            CAChecker_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Tools = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_CSR = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Compare = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Converter = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            officalWebSite_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            github_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            about_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panel_main = new System.Windows.Forms.Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ımageList1
            // 
            ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ımageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ımageList1.ImageStream");
            ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            ımageList1.Images.SetKeyName(0, "certificate.png");
            ımageList1.Images.SetKeyName(1, "error.png");
            ımageList1.Images.SetKeyName(2, "new.png");
            ımageList1.Images.SetKeyName(3, "ok.png");
            ımageList1.Images.SetKeyName(4, "settings.png");
            ımageList1.Images.SetKeyName(5, "view.png");
            ımageList1.Images.SetKeyName(6, "warning.png");
            ımageList1.Images.SetKeyName(7, "yazilimturkiye.png");
            ımageList1.Images.SetKeyName(8, "earth.png");
            ımageList1.Images.SetKeyName(9, "info.png");
            ımageList1.Images.SetKeyName(10, "terminal.fw.png");
            // 
            // Label_Baslik
            // 
            Label_Baslik.AutoSize = true;
            Label_Baslik.BackColor = System.Drawing.SystemColors.Control;
            Label_Baslik.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            Label_Baslik.ForeColor = System.Drawing.Color.FromArgb(231, 174, 78);
            Label_Baslik.Location = new System.Drawing.Point(7, 28);
            Label_Baslik.Name = "Label_Baslik";
            Label_Baslik.Size = new System.Drawing.Size(270, 32);
            Label_Baslik.TabIndex = 33;
            Label_Baslik.Text = "Certificate Checker";
            // 
            // Label_AltBaslik
            // 
            Label_AltBaslik.AutoSize = true;
            Label_AltBaslik.BackColor = System.Drawing.SystemColors.Control;
            Label_AltBaslik.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            Label_AltBaslik.Location = new System.Drawing.Point(10, 59);
            Label_AltBaslik.Name = "Label_AltBaslik";
            Label_AltBaslik.Size = new System.Drawing.Size(221, 20);
            Label_AltBaslik.TabIndex = 29;
            Label_AltBaslik.Text = "Verification > File Verification";
            // 
            // ımageList2
            // 
            ımageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ımageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ımageList2.ImageStream");
            ımageList2.TransparentColor = System.Drawing.Color.Transparent;
            ımageList2.Images.SetKeyName(0, "download.png");
            ımageList2.Images.SetKeyName(1, "recheck.png");
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_App, toolStripMenuItem_Verification, toolStripMenuItem_Tools, toolStripMenuItem_Help });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1220, 28);
            menuStrip1.TabIndex = 45;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_App
            // 
            toolStripMenuItem_App.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ExitApp_toolStripMenuItem });
            toolStripMenuItem_App.Name = "toolStripMenuItem_App";
            toolStripMenuItem_App.Size = new System.Drawing.Size(52, 24);
            toolStripMenuItem_App.Text = "App";
            // 
            // ExitApp_toolStripMenuItem
            // 
            ExitApp_toolStripMenuItem.Name = "ExitApp_toolStripMenuItem";
            ExitApp_toolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            ExitApp_toolStripMenuItem.Text = "Exit";
            // 
            // toolStripMenuItem_Verification
            // 
            toolStripMenuItem_Verification.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { FILE_toolStripMenuItem, SSLTLS_toolStripMenuItem, PEM_toolStripMenuItem, CAChecker_toolStripMenuItem });
            toolStripMenuItem_Verification.Name = "toolStripMenuItem_Verification";
            toolStripMenuItem_Verification.Size = new System.Drawing.Size(105, 24);
            toolStripMenuItem_Verification.Text = "Verification";
            // 
            // FILE_toolStripMenuItem
            // 
            FILE_toolStripMenuItem.Name = "FILE_toolStripMenuItem";
            FILE_toolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            FILE_toolStripMenuItem.Text = "File Verification";
            FILE_toolStripMenuItem.Click += FILE_toolStripMenuItem_Click;
            // 
            // SSLTLS_toolStripMenuItem
            // 
            SSLTLS_toolStripMenuItem.Name = "SSLTLS_toolStripMenuItem";
            SSLTLS_toolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            SSLTLS_toolStripMenuItem.Text = "SSL/TLS Verification";
            SSLTLS_toolStripMenuItem.Click += SSLTLS_toolStripMenuItem_Click;
            // 
            // PEM_toolStripMenuItem
            // 
            PEM_toolStripMenuItem.Name = "PEM_toolStripMenuItem";
            PEM_toolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            PEM_toolStripMenuItem.Text = "PEM/Base64 Verification";
            PEM_toolStripMenuItem.Click += PEM_toolStripMenuItem_Click;
            // 
            // CAChecker_toolStripMenuItem
            // 
            CAChecker_toolStripMenuItem.Name = "CAChecker_toolStripMenuItem";
            CAChecker_toolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            CAChecker_toolStripMenuItem.Text = "Certificate Authority Verification";
            CAChecker_toolStripMenuItem.Click += CAChecker_toolStripMenuItem_Click;
            // 
            // toolStripMenuItem_Tools
            // 
            toolStripMenuItem_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_CSR, toolStripMenuItem_Compare, toolStripMenuItem_Converter });
            toolStripMenuItem_Tools.Name = "toolStripMenuItem_Tools";
            toolStripMenuItem_Tools.Size = new System.Drawing.Size(55, 24);
            toolStripMenuItem_Tools.Text = "Tools";
            // 
            // toolStripMenuItem_CSR
            // 
            toolStripMenuItem_CSR.Name = "toolStripMenuItem_CSR";
            toolStripMenuItem_CSR.Size = new System.Drawing.Size(236, 24);
            toolStripMenuItem_CSR.Text = "Create CSR Request";
            // 
            // toolStripMenuItem_Compare
            // 
            toolStripMenuItem_Compare.Name = "toolStripMenuItem_Compare";
            toolStripMenuItem_Compare.Size = new System.Drawing.Size(236, 24);
            toolStripMenuItem_Compare.Text = "Compare Certificate";
            // 
            // toolStripMenuItem_Converter
            // 
            toolStripMenuItem_Converter.Name = "toolStripMenuItem_Converter";
            toolStripMenuItem_Converter.Size = new System.Drawing.Size(236, 24);
            toolStripMenuItem_Converter.Text = "Certificate Converter";
            // 
            // toolStripMenuItem_Help
            // 
            toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { officalWebSite_ToolStripMenuItem, github_ToolStripMenuItem, toolStripSeparator1, about_ToolStripMenuItem });
            toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            toolStripMenuItem_Help.Size = new System.Drawing.Size(54, 24);
            toolStripMenuItem_Help.Text = "Help";
            // 
            // officalWebSite_ToolStripMenuItem
            // 
            officalWebSite_ToolStripMenuItem.Name = "officalWebSite_ToolStripMenuItem";
            officalWebSite_ToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            officalWebSite_ToolStripMenuItem.Text = "Official Website";
            officalWebSite_ToolStripMenuItem.Click += officalWebSite_ToolStripMenuItem_Click;
            // 
            // github_ToolStripMenuItem
            // 
            github_ToolStripMenuItem.Name = "github_ToolStripMenuItem";
            github_ToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            github_ToolStripMenuItem.Text = "Github";
            github_ToolStripMenuItem.Click += github_ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // about_ToolStripMenuItem
            // 
            about_ToolStripMenuItem.Name = "about_ToolStripMenuItem";
            about_ToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            about_ToolStripMenuItem.Text = "About";
            about_ToolStripMenuItem.Click += about_ToolStripMenuItem_Click;
            // 
            // panel_main
            // 
            panel_main.Location = new System.Drawing.Point(0, 82);
            panel_main.Name = "panel_main";
            panel_main.Size = new System.Drawing.Size(1220, 720);
            panel_main.TabIndex = 46;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1220, 802);
            Controls.Add(panel_main);
            Controls.Add(Label_AltBaslik);
            Controls.Add(Label_Baslik);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Arial", 9.75F);
            ForeColor = System.Drawing.SystemColors.Desktop;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "CertificateChecker";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Label Label_Baslik;
        private System.Windows.Forms.Label Label_AltBaslik;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_App;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Verification;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem officalWebSite_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem github_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem about_ToolStripMenuItem;
        private System.Windows.Forms.ImageList ımageList2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitApp_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CSRGenerator_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CONVERTER_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem COMPARE_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CAChecker_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CSR;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.ToolStripMenuItem FILE_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SSLTLS_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PEM_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Compare;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Converter;
        private System.Windows.Forms.ToolStripMenuItem OPENCert_toolStripMenuItem;
    }
}