
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
            Label_SurumBilgisi = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
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
            Label_DosyaYolu = new System.Windows.Forms.Label();
            Label_DosyaAdi = new System.Windows.Forms.Label();
            Label_Baslik = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            Buton_Ayarlar = new System.Windows.Forms.Button();
            Label_AltBaslik = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Buton_Goruntule = new System.Windows.Forms.Button();
            Panel_SertifikaKontrol = new System.Windows.Forms.Panel();
            panel_Ayarlar = new System.Windows.Forms.Panel();
            groupBox4 = new System.Windows.Forms.GroupBox();
            radioButton_stunner = new System.Windows.Forms.RadioButton();
            radioButton_darkmode = new System.Windows.Forms.RadioButton();
            radioButton_lightmode = new System.Windows.Forms.RadioButton();
            label5 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).BeginInit();
            groupBox3.SuspendLayout();
            Panel_SertifikaKontrol.SuspendLayout();
            panel_Ayarlar.SuspendLayout();
            groupBox4.SuspendLayout();
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
            Buton_Dosya_Sec.BackColor = System.Drawing.SystemColors.ControlLight;
            Buton_Dosya_Sec.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Dosya_Sec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Buton_Dosya_Sec.Font = new System.Drawing.Font("Century Gothic", 12F);
            Buton_Dosya_Sec.ForeColor = System.Drawing.SystemColors.ControlText;
            Buton_Dosya_Sec.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            Buton_Dosya_Sec.ImageIndex = 2;
            Buton_Dosya_Sec.ImageList = ımageList1;
            Buton_Dosya_Sec.Location = new System.Drawing.Point(118, 106);
            Buton_Dosya_Sec.Name = "Buton_Dosya_Sec";
            Buton_Dosya_Sec.Size = new System.Drawing.Size(477, 51);
            Buton_Dosya_Sec.TabIndex = 0;
            Buton_Dosya_Sec.Text = "Select Certificate";
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
            // 
            // Label_SurumBilgisi
            // 
            Label_SurumBilgisi.AutoSize = true;
            Label_SurumBilgisi.BackColor = System.Drawing.SystemColors.Window;
            Label_SurumBilgisi.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            Label_SurumBilgisi.ForeColor = System.Drawing.Color.Black;
            Label_SurumBilgisi.Location = new System.Drawing.Point(252, 18);
            Label_SurumBilgisi.Name = "Label_SurumBilgisi";
            Label_SurumBilgisi.Size = new System.Drawing.Size(44, 21);
            Label_SurumBilgisi.TabIndex = 5;
            Label_SurumBilgisi.Text = "v2.0";
            // 
            // groupBox1
            // 
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
            groupBox1.Location = new System.Drawing.Point(9, 178);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(608, 329);
            groupBox1.TabIndex = 29;
            groupBox1.TabStop = false;
            // 
            // PictureBox_Durum
            // 
            PictureBox_Durum.BackColor = System.Drawing.Color.Transparent;
            PictureBox_Durum.Location = new System.Drawing.Point(568, 282);
            PictureBox_Durum.Name = "PictureBox_Durum";
            PictureBox_Durum.Size = new System.Drawing.Size(25, 25);
            PictureBox_Durum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            PictureBox_Durum.TabIndex = 0;
            PictureBox_Durum.TabStop = false;
            // 
            // Label_Durum
            // 
            Label_Durum.AutoSize = true;
            Label_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Durum.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Durum.Location = new System.Drawing.Point(324, 288);
            Label_Durum.Name = "Label_Durum";
            Label_Durum.Size = new System.Drawing.Size(65, 22);
            Label_Durum.TabIndex = 25;
            Label_Durum.Text = "Status";
            // 
            // Textbox_Durum
            // 
            Textbox_Durum.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Durum.Location = new System.Drawing.Point(395, 280);
            Textbox_Durum.Name = "Textbox_Durum";
            Textbox_Durum.ReadOnly = true;
            Textbox_Durum.Size = new System.Drawing.Size(200, 30);
            Textbox_Durum.TabIndex = 11;
            // 
            // Textbox_Bitis
            // 
            Textbox_Bitis.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Bitis.Location = new System.Drawing.Point(395, 236);
            Textbox_Bitis.Name = "Textbox_Bitis";
            Textbox_Bitis.ReadOnly = true;
            Textbox_Bitis.Size = new System.Drawing.Size(200, 30);
            Textbox_Bitis.TabIndex = 9;
            // 
            // Label_Bitis
            // 
            Label_Bitis.AutoSize = true;
            Label_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Bitis.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Bitis.Location = new System.Drawing.Point(324, 244);
            Label_Bitis.Name = "Label_Bitis";
            Label_Bitis.Size = new System.Drawing.Size(45, 22);
            Label_Bitis.TabIndex = 23;
            Label_Bitis.Text = "End";
            // 
            // Textbox_Serino
            // 
            Textbox_Serino.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Serino.Location = new System.Drawing.Point(119, 193);
            Textbox_Serino.Name = "Textbox_Serino";
            Textbox_Serino.ReadOnly = true;
            Textbox_Serino.Size = new System.Drawing.Size(476, 30);
            Textbox_Serino.TabIndex = 7;
            // 
            // Textbox_Baslangic
            // 
            Textbox_Baslangic.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Baslangic.Location = new System.Drawing.Point(118, 236);
            Textbox_Baslangic.Name = "Textbox_Baslangic";
            Textbox_Baslangic.ReadOnly = true;
            Textbox_Baslangic.Size = new System.Drawing.Size(200, 30);
            Textbox_Baslangic.TabIndex = 8;
            // 
            // Label_Baslangic
            // 
            Label_Baslangic.AutoSize = true;
            Label_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Baslangic.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Baslangic.Location = new System.Drawing.Point(9, 244);
            Label_Baslangic.Name = "Label_Baslangic";
            Label_Baslangic.Size = new System.Drawing.Size(52, 22);
            Label_Baslangic.TabIndex = 10;
            Label_Baslangic.Text = "Start";
            // 
            // Label_Serino
            // 
            Label_Serino.AutoSize = true;
            Label_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Serino.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Serino.Location = new System.Drawing.Point(9, 201);
            Label_Serino.Name = "Label_Serino";
            Label_Serino.Size = new System.Drawing.Size(56, 22);
            Label_Serino.TabIndex = 19;
            Label_Serino.Text = "Serial";
            // 
            // Label_Algoritma
            // 
            Label_Algoritma.AutoSize = true;
            Label_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Algoritma.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Algoritma.Location = new System.Drawing.Point(6, 288);
            Label_Algoritma.Name = "Label_Algoritma";
            Label_Algoritma.Size = new System.Drawing.Size(98, 22);
            Label_Algoritma.TabIndex = 22;
            Label_Algoritma.Text = "Algorithm";
            // 
            // Textbox_Algoritma
            // 
            Textbox_Algoritma.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Algoritma.Location = new System.Drawing.Point(118, 280);
            Textbox_Algoritma.Name = "Textbox_Algoritma";
            Textbox_Algoritma.ReadOnly = true;
            Textbox_Algoritma.Size = new System.Drawing.Size(200, 30);
            Textbox_Algoritma.TabIndex = 10;
            // 
            // Textbox_Verilen
            // 
            Textbox_Verilen.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Verilen.Location = new System.Drawing.Point(118, 106);
            Textbox_Verilen.Multiline = true;
            Textbox_Verilen.Name = "Textbox_Verilen";
            Textbox_Verilen.ReadOnly = true;
            Textbox_Verilen.Size = new System.Drawing.Size(476, 70);
            Textbox_Verilen.TabIndex = 6;
            // 
            // Textbox_Veren
            // 
            Textbox_Veren.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_Veren.Location = new System.Drawing.Point(119, 23);
            Textbox_Veren.Multiline = true;
            Textbox_Veren.Name = "Textbox_Veren";
            Textbox_Veren.ReadOnly = true;
            Textbox_Veren.Size = new System.Drawing.Size(476, 70);
            Textbox_Veren.TabIndex = 5;
            // 
            // Label_Veren
            // 
            Label_Veren.AutoSize = true;
            Label_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Veren.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Veren.Location = new System.Drawing.Point(6, 26);
            Label_Veren.Name = "Label_Veren";
            Label_Veren.Size = new System.Drawing.Size(95, 22);
            Label_Veren.TabIndex = 9;
            Label_Veren.Text = "Issued by";
            // 
            // Label_Verilen
            // 
            Label_Verilen.AutoSize = true;
            Label_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_Verilen.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Verilen.Location = new System.Drawing.Point(9, 109);
            Label_Verilen.Name = "Label_Verilen";
            Label_Verilen.Size = new System.Drawing.Size(90, 22);
            Label_Verilen.TabIndex = 8;
            Label_Verilen.Text = "Issued to";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(0, 130);
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
            Textbox_DosyaAdi.Location = new System.Drawing.Point(118, 22);
            Textbox_DosyaAdi.Name = "Textbox_DosyaAdi";
            Textbox_DosyaAdi.ReadOnly = true;
            Textbox_DosyaAdi.Size = new System.Drawing.Size(477, 30);
            Textbox_DosyaAdi.TabIndex = 3;
            // 
            // Textbox_DosyaYolu
            // 
            Textbox_DosyaYolu.BackColor = System.Drawing.SystemColors.Control;
            Textbox_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Textbox_DosyaYolu.ForeColor = System.Drawing.SystemColors.WindowText;
            Textbox_DosyaYolu.Location = new System.Drawing.Point(118, 62);
            Textbox_DosyaYolu.Name = "Textbox_DosyaYolu";
            Textbox_DosyaYolu.ReadOnly = true;
            Textbox_DosyaYolu.Size = new System.Drawing.Size(477, 30);
            Textbox_DosyaYolu.TabIndex = 4;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Label_DosyaYolu);
            groupBox3.Controls.Add(Label_DosyaAdi);
            groupBox3.Controls.Add(Textbox_DosyaYolu);
            groupBox3.Controls.Add(Buton_Dosya_Sec);
            groupBox3.Controls.Add(Textbox_DosyaAdi);
            groupBox3.Font = new System.Drawing.Font("Arial", 9.75F);
            groupBox3.Location = new System.Drawing.Point(9, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(608, 170);
            groupBox3.TabIndex = 28;
            groupBox3.TabStop = false;
            // 
            // Label_DosyaYolu
            // 
            Label_DosyaYolu.AutoSize = true;
            Label_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_DosyaYolu.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaYolu.Location = new System.Drawing.Point(6, 62);
            Label_DosyaYolu.Name = "Label_DosyaYolu";
            Label_DosyaYolu.Size = new System.Drawing.Size(53, 22);
            Label_DosyaYolu.TabIndex = 28;
            Label_DosyaYolu.Text = "Path";
            // 
            // Label_DosyaAdi
            // 
            Label_DosyaAdi.AutoSize = true;
            Label_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            Label_DosyaAdi.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_DosyaAdi.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaAdi.Location = new System.Drawing.Point(5, 22);
            Label_DosyaAdi.Name = "Label_DosyaAdi";
            Label_DosyaAdi.Size = new System.Drawing.Size(37, 22);
            Label_DosyaAdi.TabIndex = 26;
            Label_DosyaAdi.Text = "File";
            // 
            // Label_Baslik
            // 
            Label_Baslik.AutoSize = true;
            Label_Baslik.BackColor = System.Drawing.SystemColors.ControlLight;
            Label_Baslik.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            Label_Baslik.ForeColor = System.Drawing.Color.Goldenrod;
            Label_Baslik.Location = new System.Drawing.Point(12, 4);
            Label_Baslik.Name = "Label_Baslik";
            Label_Baslik.Size = new System.Drawing.Size(300, 37);
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
            linkLabel1.Location = new System.Drawing.Point(432, 512);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(186, 23);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "yazilimturkiye.com";
            linkLabel1.VisitedLinkColor = System.Drawing.Color.Gray;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Buton_Ayarlar
            // 
            Buton_Ayarlar.BackColor = System.Drawing.SystemColors.Control;
            Buton_Ayarlar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Buton_Ayarlar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Ayarlar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Buton_Ayarlar.ImageIndex = 4;
            Buton_Ayarlar.ImageList = ımageList1;
            Buton_Ayarlar.Location = new System.Drawing.Point(18, 71);
            Buton_Ayarlar.Name = "Buton_Ayarlar";
            Buton_Ayarlar.Size = new System.Drawing.Size(50, 50);
            Buton_Ayarlar.TabIndex = 1;
            Buton_Ayarlar.UseVisualStyleBackColor = false;
            Buton_Ayarlar.Click += Buton_Ayarlar_Click;
            // 
            // Label_AltBaslik
            // 
            Label_AltBaslik.AutoSize = true;
            Label_AltBaslik.BackColor = System.Drawing.SystemColors.ControlLight;
            Label_AltBaslik.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            Label_AltBaslik.Location = new System.Drawing.Point(18, 42);
            Label_AltBaslik.Name = "Label_AltBaslik";
            Label_AltBaslik.Size = new System.Drawing.Size(362, 22);
            Label_AltBaslik.TabIndex = 29;
            Label_AltBaslik.Text = "Digital Certificate Checker Application";
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.SystemColors.ControlLight;
            label1.Location = new System.Drawing.Point(0, -1);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(628, 131);
            label1.TabIndex = 37;
            // 
            // Buton_Goruntule
            // 
            Buton_Goruntule.BackColor = System.Drawing.SystemColors.Control;
            Buton_Goruntule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Buton_Goruntule.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            Buton_Goruntule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Buton_Goruntule.ImageIndex = 5;
            Buton_Goruntule.ImageList = ımageList1;
            Buton_Goruntule.Location = new System.Drawing.Point(77, 71);
            Buton_Goruntule.Name = "Buton_Goruntule";
            Buton_Goruntule.Size = new System.Drawing.Size(50, 50);
            Buton_Goruntule.TabIndex = 2;
            Buton_Goruntule.UseVisualStyleBackColor = false;
            Buton_Goruntule.Click += button_Goruntule_Click;
            // 
            // Panel_SertifikaKontrol
            // 
            Panel_SertifikaKontrol.Controls.Add(groupBox3);
            Panel_SertifikaKontrol.Controls.Add(groupBox1);
            Panel_SertifikaKontrol.Location = new System.Drawing.Point(0, 139);
            Panel_SertifikaKontrol.Name = "Panel_SertifikaKontrol";
            Panel_SertifikaKontrol.Size = new System.Drawing.Size(628, 542);
            Panel_SertifikaKontrol.TabIndex = 39;
            // 
            // panel_Ayarlar
            // 
            panel_Ayarlar.Controls.Add(groupBox4);
            panel_Ayarlar.Controls.Add(groupBox2);
            panel_Ayarlar.Controls.Add(linkLabel1);
            panel_Ayarlar.Location = new System.Drawing.Point(0, 139);
            panel_Ayarlar.Name = "panel_Ayarlar";
            panel_Ayarlar.Size = new System.Drawing.Size(628, 542);
            panel_Ayarlar.TabIndex = 40;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(radioButton_stunner);
            groupBox4.Controls.Add(radioButton_darkmode);
            groupBox4.Controls.Add(radioButton_lightmode);
            groupBox4.Controls.Add(label5);
            groupBox4.Location = new System.Drawing.Point(10, 239);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(608, 268);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            // 
            // radioButton_stunner
            // 
            radioButton_stunner.AutoSize = true;
            radioButton_stunner.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            radioButton_stunner.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            radioButton_stunner.Location = new System.Drawing.Point(314, 25);
            radioButton_stunner.Name = "radioButton_stunner";
            radioButton_stunner.Size = new System.Drawing.Size(101, 26);
            radioButton_stunner.TabIndex = 36;
            radioButton_stunner.TabStop = true;
            radioButton_stunner.Text = "Stunner";
            radioButton_stunner.UseVisualStyleBackColor = true;
            radioButton_stunner.CheckedChanged += radioButton_stunner_CheckedChanged;
            // 
            // radioButton_darkmode
            // 
            radioButton_darkmode.AutoSize = true;
            radioButton_darkmode.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            radioButton_darkmode.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            radioButton_darkmode.Location = new System.Drawing.Point(215, 25);
            radioButton_darkmode.Name = "radioButton_darkmode";
            radioButton_darkmode.Size = new System.Drawing.Size(93, 26);
            radioButton_darkmode.TabIndex = 35;
            radioButton_darkmode.TabStop = true;
            radioButton_darkmode.Text = "Hunter";
            radioButton_darkmode.UseVisualStyleBackColor = true;
            radioButton_darkmode.CheckedChanged += radioButton_darkmode_CheckedChanged;
            // 
            // radioButton_lightmode
            // 
            radioButton_lightmode.AutoSize = true;
            radioButton_lightmode.Checked = true;
            radioButton_lightmode.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            radioButton_lightmode.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            radioButton_lightmode.Location = new System.Drawing.Point(113, 25);
            radioButton_lightmode.Name = "radioButton_lightmode";
            radioButton_lightmode.Size = new System.Drawing.Size(96, 26);
            radioButton_lightmode.TabIndex = 34;
            radioButton_lightmode.TabStop = true;
            radioButton_lightmode.Text = "Argent";
            radioButton_lightmode.UseVisualStyleBackColor = true;
            radioButton_lightmode.CheckedChanged += radioButton_lightmode_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.SystemColors.Control;
            label5.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            label5.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label5.Location = new System.Drawing.Point(11, 27);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(71, 22);
            label5.TabIndex = 33;
            label5.Text = "Theme";
            // 
            // groupBox2
            // 
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
            label4.Font = new System.Drawing.Font("Century Gothic", 11.25F);
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
            label3.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            label3.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label3.Location = new System.Drawing.Point(346, 22);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(50, 22);
            label3.TabIndex = 31;
            label3.Text = "v3.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.SystemColors.Control;
            label2.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label2.Location = new System.Drawing.Point(113, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(236, 22);
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
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(628, 681);
            Controls.Add(Panel_SertifikaKontrol);
            Controls.Add(Buton_Goruntule);
            Controls.Add(Label_AltBaslik);
            Controls.Add(Buton_Ayarlar);
            Controls.Add(Label_Baslik);
            Controls.Add(progressBar1);
            Controls.Add(Label_SurumBilgisi);
            Controls.Add(label1);
            Controls.Add(panel_Ayarlar);
            Font = new System.Drawing.Font("Arial", 9.75F);
            ForeColor = System.Drawing.SystemColors.Desktop;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Certificate Checker";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            Panel_SertifikaKontrol.ResumeLayout(false);
            panel_Ayarlar.ResumeLayout(false);
            panel_Ayarlar.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Buton_Dosya_Sec;
        private System.Windows.Forms.Label Label_SurumBilgisi;
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
        private System.Windows.Forms.RadioButton radioButton_darkmode;
        private System.Windows.Forms.RadioButton radioButton_lightmode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton_stunner;
    }
}