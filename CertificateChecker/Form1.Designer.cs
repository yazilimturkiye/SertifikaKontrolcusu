
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
            Buton_Dosya_Sec = new System.Windows.Forms.Button();
            ımageList1 = new System.Windows.Forms.ImageList(components);
            groupBox1 = new System.Windows.Forms.GroupBox();
            TextBox_Publickey = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            Textbox_Constraints = new System.Windows.Forms.TextBox();
            Label_Constraints = new System.Windows.Forms.Label();
            Textbox_Usage = new System.Windows.Forms.TextBox();
            Label_Usage = new System.Windows.Forms.Label();
            PictureBox_Durum = new System.Windows.Forms.PictureBox();
            Label_Durum = new System.Windows.Forms.Label();
            Textbox_Durum = new System.Windows.Forms.TextBox();
            Textbox_Bitis = new System.Windows.Forms.TextBox();
            Label_Bitis = new System.Windows.Forms.Label();
            Textbox_Serino = new System.Windows.Forms.TextBox();
            Textbox_Baslangic = new System.Windows.Forms.TextBox();
            Label_Baslangic = new System.Windows.Forms.Label();
            Label_Serino = new System.Windows.Forms.Label();
            Label_Algoritma = new System.Windows.Forms.Label();
            Textbox_Algoritma = new System.Windows.Forms.TextBox();
            Textbox_Verilen = new System.Windows.Forms.TextBox();
            Textbox_Veren = new System.Windows.Forms.TextBox();
            Label_Veren = new System.Windows.Forms.Label();
            Label_Verilen = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            Textbox_DosyaAdi = new System.Windows.Forms.TextBox();
            Textbox_DosyaYolu = new System.Windows.Forms.TextBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            button_CPS = new System.Windows.Forms.Button();
            Label_DosyaYolu = new System.Windows.Forms.Label();
            Buton_Goruntule = new System.Windows.Forms.Button();
            Label_DosyaAdi = new System.Windows.Forms.Label();
            Label_Baslik = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            Buton_Ayarlar = new System.Windows.Forms.Button();
            Label_AltBaslik = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Panel_SertifikaKontrol = new System.Windows.Forms.Panel();
            panel_Ayarlar = new System.Windows.Forms.Panel();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            button_Sertifikakontrol = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).BeginInit();
            groupBox3.SuspendLayout();
            Panel_SertifikaKontrol.SuspendLayout();
            panel_Ayarlar.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Buton_Dosya_Sec
            // 
            Buton_Dosya_Sec.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            Buton_Dosya_Sec.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Dosya_Sec.Font = new System.Drawing.Font("Century Gothic", 12F);
            Buton_Dosya_Sec.ForeColor = System.Drawing.SystemColors.ControlText;
            Buton_Dosya_Sec.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            Buton_Dosya_Sec.ImageIndex = 2;
            Buton_Dosya_Sec.ImageList = ımageList1;
            Buton_Dosya_Sec.Location = new System.Drawing.Point(232, 80);
            Buton_Dosya_Sec.Name = "Buton_Dosya_Sec";
            Buton_Dosya_Sec.Size = new System.Drawing.Size(364, 45);
            Buton_Dosya_Sec.TabIndex = 0;
            Buton_Dosya_Sec.Text = "Select Certificate File";
            Buton_Dosya_Sec.UseVisualStyleBackColor = false;
            Buton_Dosya_Sec.Click += Buton_Dosya_Sec_Click;
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
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TextBox_Publickey);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(Textbox_Constraints);
            groupBox1.Controls.Add(Label_Constraints);
            groupBox1.Controls.Add(Textbox_Usage);
            groupBox1.Controls.Add(Label_Usage);
            groupBox1.Controls.Add(PictureBox_Durum);
            groupBox1.Controls.Add(Label_Durum);
            groupBox1.Controls.Add(Textbox_Durum);
            groupBox1.Controls.Add(Textbox_Bitis);
            groupBox1.Controls.Add(Label_Bitis);
            groupBox1.Controls.Add(Textbox_Serino);
            groupBox1.Controls.Add(Textbox_Baslangic);
            groupBox1.Controls.Add(Label_Baslangic);
            groupBox1.Controls.Add(Label_Serino);
            groupBox1.Controls.Add(Label_Algoritma);
            groupBox1.Controls.Add(Textbox_Algoritma);
            groupBox1.Controls.Add(Textbox_Verilen);
            groupBox1.Controls.Add(Textbox_Veren);
            groupBox1.Controls.Add(Label_Veren);
            groupBox1.Controls.Add(Label_Verilen);
            groupBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            groupBox1.Location = new System.Drawing.Point(9, 141);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(608, 400);
            groupBox1.TabIndex = 29;
            groupBox1.TabStop = false;
            // 
            // TextBox_Publickey
            // 
            TextBox_Publickey.BackColor = System.Drawing.SystemColors.Control;
            TextBox_Publickey.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            TextBox_Publickey.Location = new System.Drawing.Point(396, 301);
            TextBox_Publickey.Name = "TextBox_Publickey";
            TextBox_Publickey.ReadOnly = true;
            TextBox_Publickey.Size = new System.Drawing.Size(200, 26);
            TextBox_Publickey.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Century Gothic", 12F);
            label5.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label5.Location = new System.Drawing.Point(337, 300);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(37, 21);
            label5.TabIndex = 30;
            label5.Text = "Key";
            // 
            // Textbox_Constraints
            // 
            Textbox_Constraints.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Constraints.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Constraints.Location = new System.Drawing.Point(118, 235);
            Textbox_Constraints.Name = "Textbox_Constraints";
            Textbox_Constraints.ReadOnly = true;
            Textbox_Constraints.Size = new System.Drawing.Size(476, 26);
            Textbox_Constraints.TabIndex = 10;
            // 
            // Label_Constraints
            // 
            Label_Constraints.AutoSize = true;
            Label_Constraints.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Constraints.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Constraints.Location = new System.Drawing.Point(9, 235);
            Label_Constraints.Name = "Label_Constraints";
            Label_Constraints.Size = new System.Drawing.Size(98, 21);
            Label_Constraints.TabIndex = 29;
            Label_Constraints.Text = "Constraints";
            // 
            // Textbox_Usage
            // 
            Textbox_Usage.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Usage.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Usage.Location = new System.Drawing.Point(119, 202);
            Textbox_Usage.Name = "Textbox_Usage";
            Textbox_Usage.ReadOnly = true;
            Textbox_Usage.Size = new System.Drawing.Size(476, 26);
            Textbox_Usage.TabIndex = 9;
            // 
            // Label_Usage
            // 
            Label_Usage.AutoSize = true;
            Label_Usage.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Usage.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Usage.Location = new System.Drawing.Point(9, 202);
            Label_Usage.Name = "Label_Usage";
            Label_Usage.Size = new System.Drawing.Size(90, 21);
            Label_Usage.TabIndex = 27;
            Label_Usage.Text = "Key Usage";
            // 
            // PictureBox_Durum
            // 
            PictureBox_Durum.BackColor = System.Drawing.Color.Transparent;
            PictureBox_Durum.Location = new System.Drawing.Point(67, 333);
            PictureBox_Durum.Name = "PictureBox_Durum";
            PictureBox_Durum.Size = new System.Drawing.Size(23, 23);
            PictureBox_Durum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            PictureBox_Durum.TabIndex = 0;
            PictureBox_Durum.TabStop = false;
            // 
            // Label_Durum
            // 
            Label_Durum.AutoSize = true;
            Label_Durum.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Durum.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Durum.Location = new System.Drawing.Point(9, 333);
            Label_Durum.Name = "Label_Durum";
            Label_Durum.Size = new System.Drawing.Size(56, 21);
            Label_Durum.TabIndex = 25;
            Label_Durum.Text = "Result";
            // 
            // Textbox_Durum
            // 
            Textbox_Durum.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Durum.Location = new System.Drawing.Point(118, 333);
            Textbox_Durum.Multiline = true;
            Textbox_Durum.Name = "Textbox_Durum";
            Textbox_Durum.ReadOnly = true;
            Textbox_Durum.Size = new System.Drawing.Size(476, 57);
            Textbox_Durum.TabIndex = 15;
            // 
            // Textbox_Bitis
            // 
            Textbox_Bitis.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Bitis.Location = new System.Drawing.Point(396, 268);
            Textbox_Bitis.Name = "Textbox_Bitis";
            Textbox_Bitis.ReadOnly = true;
            Textbox_Bitis.Size = new System.Drawing.Size(200, 26);
            Textbox_Bitis.TabIndex = 12;
            // 
            // Label_Bitis
            // 
            Label_Bitis.AutoSize = true;
            Label_Bitis.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Bitis.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Bitis.Location = new System.Drawing.Point(337, 268);
            Label_Bitis.Name = "Label_Bitis";
            Label_Bitis.Size = new System.Drawing.Size(40, 21);
            Label_Bitis.TabIndex = 23;
            Label_Bitis.Text = "End";
            // 
            // Textbox_Serino
            // 
            Textbox_Serino.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Serino.Location = new System.Drawing.Point(119, 169);
            Textbox_Serino.Name = "Textbox_Serino";
            Textbox_Serino.ReadOnly = true;
            Textbox_Serino.Size = new System.Drawing.Size(476, 26);
            Textbox_Serino.TabIndex = 8;
            // 
            // Textbox_Baslangic
            // 
            Textbox_Baslangic.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Baslangic.Location = new System.Drawing.Point(119, 268);
            Textbox_Baslangic.Name = "Textbox_Baslangic";
            Textbox_Baslangic.ReadOnly = true;
            Textbox_Baslangic.Size = new System.Drawing.Size(200, 26);
            Textbox_Baslangic.TabIndex = 11;
            // 
            // Label_Baslangic
            // 
            Label_Baslangic.AutoSize = true;
            Label_Baslangic.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Baslangic.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Baslangic.Location = new System.Drawing.Point(9, 268);
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
            Label_Serino.Location = new System.Drawing.Point(9, 169);
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
            Label_Algoritma.Location = new System.Drawing.Point(9, 300);
            Label_Algoritma.Name = "Label_Algoritma";
            Label_Algoritma.Size = new System.Drawing.Size(87, 21);
            Label_Algoritma.TabIndex = 22;
            Label_Algoritma.Text = "Algorithm";
            // 
            // Textbox_Algoritma
            // 
            Textbox_Algoritma.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Algoritma.Location = new System.Drawing.Point(119, 300);
            Textbox_Algoritma.Name = "Textbox_Algoritma";
            Textbox_Algoritma.ReadOnly = true;
            Textbox_Algoritma.Size = new System.Drawing.Size(200, 26);
            Textbox_Algoritma.TabIndex = 13;
            // 
            // Textbox_Verilen
            // 
            Textbox_Verilen.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Verilen.Location = new System.Drawing.Point(119, 93);
            Textbox_Verilen.Multiline = true;
            Textbox_Verilen.Name = "Textbox_Verilen";
            Textbox_Verilen.ReadOnly = true;
            Textbox_Verilen.Size = new System.Drawing.Size(476, 70);
            Textbox_Verilen.TabIndex = 7;
            // 
            // Textbox_Veren
            // 
            Textbox_Veren.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Veren.Location = new System.Drawing.Point(119, 17);
            Textbox_Veren.Multiline = true;
            Textbox_Veren.Name = "Textbox_Veren";
            Textbox_Veren.ReadOnly = true;
            Textbox_Veren.Size = new System.Drawing.Size(476, 70);
            Textbox_Veren.TabIndex = 6;
            // 
            // Label_Veren
            // 
            Label_Veren.AutoSize = true;
            Label_Veren.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Veren.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Veren.Location = new System.Drawing.Point(9, 17);
            Label_Veren.Name = "Label_Veren";
            Label_Veren.Size = new System.Drawing.Size(81, 21);
            Label_Veren.TabIndex = 9;
            Label_Veren.Text = "Issued by";
            // 
            // Label_Verilen
            // 
            Label_Verilen.AutoSize = true;
            Label_Verilen.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_Verilen.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Verilen.Location = new System.Drawing.Point(9, 93);
            Label_Verilen.Name = "Label_Verilen";
            Label_Verilen.Size = new System.Drawing.Size(79, 21);
            Label_Verilen.TabIndex = 8;
            Label_Verilen.Text = "Issued to";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(0, 115);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(628, 6);
            progressBar1.TabIndex = 8;
            // 
            // timer1
            // 
            timer1.Interval = 15;
            timer1.Tick += timer1_Tick;
            // 
            // Textbox_DosyaAdi
            // 
            Textbox_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            Textbox_DosyaAdi.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_DosyaAdi.ForeColor = System.Drawing.SystemColors.WindowText;
            Textbox_DosyaAdi.Location = new System.Drawing.Point(118, 14);
            Textbox_DosyaAdi.Name = "Textbox_DosyaAdi";
            Textbox_DosyaAdi.ReadOnly = true;
            Textbox_DosyaAdi.Size = new System.Drawing.Size(477, 26);
            Textbox_DosyaAdi.TabIndex = 4;
            // 
            // Textbox_DosyaYolu
            // 
            Textbox_DosyaYolu.BackColor = System.Drawing.SystemColors.Control;
            Textbox_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_DosyaYolu.ForeColor = System.Drawing.SystemColors.WindowText;
            Textbox_DosyaYolu.Location = new System.Drawing.Point(118, 47);
            Textbox_DosyaYolu.Name = "Textbox_DosyaYolu";
            Textbox_DosyaYolu.ReadOnly = true;
            Textbox_DosyaYolu.Size = new System.Drawing.Size(477, 26);
            Textbox_DosyaYolu.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button_CPS);
            groupBox3.Controls.Add(Label_DosyaYolu);
            groupBox3.Controls.Add(Buton_Goruntule);
            groupBox3.Controls.Add(Label_DosyaAdi);
            groupBox3.Controls.Add(Textbox_DosyaYolu);
            groupBox3.Controls.Add(Buton_Dosya_Sec);
            groupBox3.Controls.Add(Textbox_DosyaAdi);
            groupBox3.Font = new System.Drawing.Font("Arial", 9.75F);
            groupBox3.Location = new System.Drawing.Point(9, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(608, 139);
            groupBox3.TabIndex = 28;
            groupBox3.TabStop = false;
            // 
            // button_CPS
            // 
            button_CPS.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_CPS.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            button_CPS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            button_CPS.ForeColor = System.Drawing.SystemColors.ControlText;
            button_CPS.ImageIndex = 5;
            button_CPS.ImageList = ımageList1;
            button_CPS.Location = new System.Drawing.Point(120, 80);
            button_CPS.Name = "button_CPS";
            button_CPS.Size = new System.Drawing.Size(50, 45);
            button_CPS.TabIndex = 3;
            button_CPS.UseVisualStyleBackColor = false;
            button_CPS.Click += button_CPS_Click;
            // 
            // Label_DosyaYolu
            // 
            Label_DosyaYolu.AutoSize = true;
            Label_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_DosyaYolu.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaYolu.Location = new System.Drawing.Point(6, 47);
            Label_DosyaYolu.Name = "Label_DosyaYolu";
            Label_DosyaYolu.Size = new System.Drawing.Size(47, 21);
            Label_DosyaYolu.TabIndex = 28;
            Label_DosyaYolu.Text = "Path";
            // 
            // Buton_Goruntule
            // 
            Buton_Goruntule.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            Buton_Goruntule.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Goruntule.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            Buton_Goruntule.ForeColor = System.Drawing.SystemColors.ControlText;
            Buton_Goruntule.ImageIndex = 0;
            Buton_Goruntule.ImageList = ımageList1;
            Buton_Goruntule.Location = new System.Drawing.Point(176, 80);
            Buton_Goruntule.Name = "Buton_Goruntule";
            Buton_Goruntule.Size = new System.Drawing.Size(50, 45);
            Buton_Goruntule.TabIndex = 1;
            Buton_Goruntule.UseVisualStyleBackColor = false;
            Buton_Goruntule.Click += button_Goruntule_Click;
            // 
            // Label_DosyaAdi
            // 
            Label_DosyaAdi.AutoSize = true;
            Label_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            Label_DosyaAdi.Font = new System.Drawing.Font("Century Gothic", 12F);
            Label_DosyaAdi.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaAdi.Location = new System.Drawing.Point(5, 14);
            Label_DosyaAdi.Name = "Label_DosyaAdi";
            Label_DosyaAdi.Size = new System.Drawing.Size(34, 21);
            Label_DosyaAdi.TabIndex = 26;
            Label_DosyaAdi.Text = "File";
            // 
            // Label_Baslik
            // 
            Label_Baslik.AutoSize = true;
            Label_Baslik.BackColor = System.Drawing.SystemColors.ControlLight;
            Label_Baslik.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            Label_Baslik.ForeColor = System.Drawing.Color.FromArgb(231, 174, 78);
            Label_Baslik.Location = new System.Drawing.Point(9, 3);
            Label_Baslik.Name = "Label_Baslik";
            Label_Baslik.Size = new System.Drawing.Size(240, 28);
            Label_Baslik.TabIndex = 33;
            Label_Baslik.Text = "Certificate Checker";
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = System.Drawing.SystemColors.Control;
            linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            linkLabel1.DisabledLinkColor = System.Drawing.Color.Gray;
            linkLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            linkLabel1.ForeColor = System.Drawing.Color.Gray;
            linkLabel1.LinkColor = System.Drawing.Color.Gray;
            linkLabel1.Location = new System.Drawing.Point(113, 191);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(150, 21);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "yazilimturkiye.com";
            linkLabel1.VisitedLinkColor = System.Drawing.Color.Gray;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Buton_Ayarlar
            // 
            Buton_Ayarlar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            Buton_Ayarlar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Buton_Ayarlar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Ayarlar.ImageIndex = 4;
            Buton_Ayarlar.ImageList = ımageList1;
            Buton_Ayarlar.Location = new System.Drawing.Point(68, 57);
            Buton_Ayarlar.Name = "Buton_Ayarlar";
            Buton_Ayarlar.Size = new System.Drawing.Size(50, 50);
            Buton_Ayarlar.TabIndex = 16;
            Buton_Ayarlar.UseVisualStyleBackColor = false;
            Buton_Ayarlar.Click += Buton_Ayarlar_Click;
            // 
            // Label_AltBaslik
            // 
            Label_AltBaslik.AutoSize = true;
            Label_AltBaslik.BackColor = System.Drawing.SystemColors.ControlLight;
            Label_AltBaslik.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            Label_AltBaslik.Location = new System.Drawing.Point(12, 29);
            Label_AltBaslik.Name = "Label_AltBaslik";
            Label_AltBaslik.Size = new System.Drawing.Size(314, 21);
            Label_AltBaslik.TabIndex = 29;
            Label_AltBaslik.Text = "Digital Certificate Checker Application";
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.SystemColors.ControlLight;
            label1.Location = new System.Drawing.Point(0, -1);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(628, 122);
            label1.TabIndex = 37;
            // 
            // Panel_SertifikaKontrol
            // 
            Panel_SertifikaKontrol.Controls.Add(groupBox3);
            Panel_SertifikaKontrol.Controls.Add(groupBox1);
            Panel_SertifikaKontrol.Location = new System.Drawing.Point(0, 122);
            Panel_SertifikaKontrol.Name = "Panel_SertifikaKontrol";
            Panel_SertifikaKontrol.Size = new System.Drawing.Size(628, 550);
            Panel_SertifikaKontrol.TabIndex = 39;
            // 
            // panel_Ayarlar
            // 
            panel_Ayarlar.Controls.Add(groupBox4);
            panel_Ayarlar.Controls.Add(groupBox2);
            panel_Ayarlar.Location = new System.Drawing.Point(0, 121);
            panel_Ayarlar.Name = "panel_Ayarlar";
            panel_Ayarlar.Size = new System.Drawing.Size(628, 550);
            panel_Ayarlar.TabIndex = 40;
            // 
            // groupBox4
            // 
            groupBox4.Location = new System.Drawing.Point(10, 239);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(608, 302);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(linkLabel1);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(pictureBox1);
            groupBox2.Location = new System.Drawing.Point(10, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(608, 232);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.SystemColors.Control;
            label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            label4.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label4.Location = new System.Drawing.Point(113, 57);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(485, 164);
            label4.TabIndex = 32;
            label4.Text = resources.GetString("label4.Text");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.SystemColors.Control;
            label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            label3.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label3.Location = new System.Drawing.Point(314, 25);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 17);
            label3.TabIndex = 31;
            label3.Text = "v4.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.SystemColors.Control;
            label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label2.Location = new System.Drawing.Point(113, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(204, 21);
            label2.TabIndex = 30;
            label2.Text = "Certificate Checker FREE";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Image = Properties.Resources.certificate;
            pictureBox1.Location = new System.Drawing.Point(11, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(100, 100);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button_Sertifikakontrol
            // 
            button_Sertifikakontrol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            button_Sertifikakontrol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button_Sertifikakontrol.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            button_Sertifikakontrol.ImageIndex = 2;
            button_Sertifikakontrol.ImageList = ımageList1;
            button_Sertifikakontrol.Location = new System.Drawing.Point(12, 57);
            button_Sertifikakontrol.Name = "button_Sertifikakontrol";
            button_Sertifikakontrol.Size = new System.Drawing.Size(50, 50);
            button_Sertifikakontrol.TabIndex = 18;
            button_Sertifikakontrol.UseVisualStyleBackColor = false;
            button_Sertifikakontrol.Click += button_Sertifikakontrol_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(628, 672);
            Controls.Add(button_Sertifikakontrol);
            Controls.Add(Label_AltBaslik);
            Controls.Add(Buton_Ayarlar);
            Controls.Add(Label_Baslik);
            Controls.Add(progressBar1);
            Controls.Add(label1);
            Controls.Add(Panel_SertifikaKontrol);
            Controls.Add(panel_Ayarlar);
            Font = new System.Drawing.Font("Arial", 9.75F);
            ForeColor = System.Drawing.SystemColors.Desktop;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "CertificateChecker";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            Panel_SertifikaKontrol.ResumeLayout(false);
            panel_Ayarlar.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Buton_Dosya_Sec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Label_Baslangic;
        private System.Windows.Forms.Label Label_Veren;
        private System.Windows.Forms.Label Label_Verilen;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox Textbox_Veren;
        private System.Windows.Forms.TextBox Textbox_Baslangic;
        private System.Windows.Forms.TextBox Textbox_Verilen;
        private System.Windows.Forms.TextBox Textbox_Bitis;
        private System.Windows.Forms.TextBox Textbox_Algoritma;
        private System.Windows.Forms.TextBox Textbox_Serino;
        private System.Windows.Forms.Label Label_Serino;
        private System.Windows.Forms.Label Label_Algoritma;
        private System.Windows.Forms.TextBox Textbox_DosyaAdi;
        private System.Windows.Forms.TextBox Textbox_DosyaYolu;
        private System.Windows.Forms.Label Label_Bitis;
        private System.Windows.Forms.TextBox Textbox_Durum;
        private System.Windows.Forms.Label Label_Durum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label Label_DosyaYolu;
        private System.Windows.Forms.Label Label_DosyaAdi;
        private System.Windows.Forms.PictureBox PictureBox_Durum;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Label Label_Baslik;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button Buton_Ayarlar;
        private System.Windows.Forms.Label Label_AltBaslik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Buton_Goruntule;
        private System.Windows.Forms.Panel Panel_SertifikaKontrol;
        private System.Windows.Forms.Panel panel_Ayarlar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_Sertifikakontrol;
        private System.Windows.Forms.Button button_CPS;
        private System.Windows.Forms.TextBox Textbox_Constraints;
        private System.Windows.Forms.Label Label_Constraints;
        private System.Windows.Forms.TextBox Textbox_Usage;
        private System.Windows.Forms.Label Label_Usage;
        private System.Windows.Forms.TextBox TextBox_Publickey;
        private System.Windows.Forms.Label label5;
    }
}