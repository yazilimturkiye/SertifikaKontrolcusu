
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
            Buton_Yardim = new System.Windows.Forms.Button();
            Label_AltBaslik = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Buton_Goruntule = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Durum).BeginInit();
            groupBox3.SuspendLayout();
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
            Buton_Dosya_Sec.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Buton_Dosya_Sec.ForeColor = System.Drawing.SystemColors.ControlText;
            Buton_Dosya_Sec.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            Buton_Dosya_Sec.ImageIndex = 1;
            Buton_Dosya_Sec.ImageList = ımageList1;
            Buton_Dosya_Sec.Location = new System.Drawing.Point(93, 90);
            Buton_Dosya_Sec.Name = "Buton_Dosya_Sec";
            Buton_Dosya_Sec.Size = new System.Drawing.Size(477, 40);
            Buton_Dosya_Sec.TabIndex = 1;
            Buton_Dosya_Sec.Text = "Sertifika Seç";
            Buton_Dosya_Sec.UseVisualStyleBackColor = false;
            Buton_Dosya_Sec.Click += Buton_Dosya_Sec_Click;
            // 
            // ımageList1
            // 
            ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ımageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ımageList1.ImageStream");
            ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            ımageList1.Images.SetKeyName(0, "certificate.png");
            ımageList1.Images.SetKeyName(1, "new.png");
            ımageList1.Images.SetKeyName(2, "view.png");
            ımageList1.Images.SetKeyName(3, "settings.png");
            // 
            // Label_SurumBilgisi
            // 
            Label_SurumBilgisi.AutoSize = true;
            Label_SurumBilgisi.BackColor = System.Drawing.SystemColors.Window;
            Label_SurumBilgisi.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_SurumBilgisi.ForeColor = System.Drawing.Color.Black;
            Label_SurumBilgisi.Location = new System.Drawing.Point(252, 18);
            Label_SurumBilgisi.Name = "Label_SurumBilgisi";
            Label_SurumBilgisi.Size = new System.Drawing.Size(34, 17);
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
            groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            groupBox1.Location = new System.Drawing.Point(12, 287);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(580, 285);
            groupBox1.TabIndex = 29;
            groupBox1.TabStop = false;
            // 
            // PictureBox_Durum
            // 
            PictureBox_Durum.BackColor = System.Drawing.Color.Transparent;
            PictureBox_Durum.Location = new System.Drawing.Point(545, 248);
            PictureBox_Durum.Name = "PictureBox_Durum";
            PictureBox_Durum.Size = new System.Drawing.Size(20, 20);
            PictureBox_Durum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            PictureBox_Durum.TabIndex = 0;
            PictureBox_Durum.TabStop = false;
            // 
            // Label_Durum
            // 
            Label_Durum.AutoSize = true;
            Label_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Durum.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Durum.Location = new System.Drawing.Point(306, 246);
            Label_Durum.Name = "Label_Durum";
            Label_Durum.Size = new System.Drawing.Size(56, 20);
            Label_Durum.TabIndex = 25;
            Label_Durum.Text = "Durum";
            // 
            // Textbox_Durum
            // 
            Textbox_Durum.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Durum.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Durum.Location = new System.Drawing.Point(369, 243);
            Textbox_Durum.Multiline = true;
            Textbox_Durum.Name = "Textbox_Durum";
            Textbox_Durum.ReadOnly = true;
            Textbox_Durum.Size = new System.Drawing.Size(200, 30);
            Textbox_Durum.TabIndex = 9;
            // 
            // Textbox_Bitis
            // 
            Textbox_Bitis.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Bitis.Location = new System.Drawing.Point(369, 207);
            Textbox_Bitis.Multiline = true;
            Textbox_Bitis.Name = "Textbox_Bitis";
            Textbox_Bitis.ReadOnly = true;
            Textbox_Bitis.Size = new System.Drawing.Size(200, 30);
            Textbox_Bitis.TabIndex = 7;
            // 
            // Label_Bitis
            // 
            Label_Bitis.AutoSize = true;
            Label_Bitis.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Bitis.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Bitis.Location = new System.Drawing.Point(306, 210);
            Label_Bitis.Name = "Label_Bitis";
            Label_Bitis.Size = new System.Drawing.Size(35, 20);
            Label_Bitis.TabIndex = 23;
            Label_Bitis.Text = "Bitiş";
            // 
            // Textbox_Serino
            // 
            Textbox_Serino.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Serino.Location = new System.Drawing.Point(93, 171);
            Textbox_Serino.Multiline = true;
            Textbox_Serino.Name = "Textbox_Serino";
            Textbox_Serino.ReadOnly = true;
            Textbox_Serino.Size = new System.Drawing.Size(476, 30);
            Textbox_Serino.TabIndex = 5;
            // 
            // Textbox_Baslangic
            // 
            Textbox_Baslangic.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Baslangic.Location = new System.Drawing.Point(93, 207);
            Textbox_Baslangic.Multiline = true;
            Textbox_Baslangic.Name = "Textbox_Baslangic";
            Textbox_Baslangic.ReadOnly = true;
            Textbox_Baslangic.Size = new System.Drawing.Size(200, 30);
            Textbox_Baslangic.TabIndex = 6;
            // 
            // Label_Baslangic
            // 
            Label_Baslangic.AutoSize = true;
            Label_Baslangic.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Baslangic.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Baslangic.Location = new System.Drawing.Point(6, 207);
            Label_Baslangic.Name = "Label_Baslangic";
            Label_Baslangic.Size = new System.Drawing.Size(79, 20);
            Label_Baslangic.TabIndex = 10;
            Label_Baslangic.Text = "Başlangıç";
            // 
            // Label_Serino
            // 
            Label_Serino.AutoSize = true;
            Label_Serino.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Serino.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Serino.Location = new System.Drawing.Point(7, 171);
            Label_Serino.Name = "Label_Serino";
            Label_Serino.Size = new System.Drawing.Size(63, 20);
            Label_Serino.TabIndex = 19;
            Label_Serino.Text = "Seri No ";
            // 
            // Label_Algoritma
            // 
            Label_Algoritma.AutoSize = true;
            Label_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Algoritma.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Algoritma.Location = new System.Drawing.Point(7, 246);
            Label_Algoritma.Name = "Label_Algoritma";
            Label_Algoritma.Size = new System.Drawing.Size(79, 20);
            Label_Algoritma.TabIndex = 22;
            Label_Algoritma.Text = "Algoritma";
            // 
            // Textbox_Algoritma
            // 
            Textbox_Algoritma.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Algoritma.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Algoritma.Location = new System.Drawing.Point(92, 243);
            Textbox_Algoritma.Multiline = true;
            Textbox_Algoritma.Name = "Textbox_Algoritma";
            Textbox_Algoritma.ReadOnly = true;
            Textbox_Algoritma.Size = new System.Drawing.Size(200, 30);
            Textbox_Algoritma.TabIndex = 8;
            // 
            // Textbox_Verilen
            // 
            Textbox_Verilen.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Verilen.Location = new System.Drawing.Point(93, 95);
            Textbox_Verilen.Multiline = true;
            Textbox_Verilen.Name = "Textbox_Verilen";
            Textbox_Verilen.ReadOnly = true;
            Textbox_Verilen.Size = new System.Drawing.Size(476, 70);
            Textbox_Verilen.TabIndex = 4;
            // 
            // Textbox_Veren
            // 
            Textbox_Veren.BackColor = System.Drawing.SystemColors.Control;
            Textbox_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_Veren.Location = new System.Drawing.Point(93, 19);
            Textbox_Veren.Multiline = true;
            Textbox_Veren.Name = "Textbox_Veren";
            Textbox_Veren.ReadOnly = true;
            Textbox_Veren.Size = new System.Drawing.Size(476, 70);
            Textbox_Veren.TabIndex = 3;
            // 
            // Label_Veren
            // 
            Label_Veren.AutoSize = true;
            Label_Veren.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Veren.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Veren.Location = new System.Drawing.Point(7, 18);
            Label_Veren.Name = "Label_Veren";
            Label_Veren.Size = new System.Drawing.Size(54, 20);
            Label_Veren.TabIndex = 9;
            Label_Veren.Text = "Veren";
            // 
            // Label_Verilen
            // 
            Label_Verilen.AutoSize = true;
            Label_Verilen.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Verilen.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_Verilen.Location = new System.Drawing.Point(7, 95);
            Label_Verilen.Name = "Label_Verilen";
            Label_Verilen.Size = new System.Drawing.Size(60, 20);
            Label_Verilen.TabIndex = 8;
            Label_Verilen.Text = "Verilen";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(0, 126);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(604, 6);
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
            Textbox_DosyaAdi.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_DosyaAdi.ForeColor = System.Drawing.SystemColors.ControlText;
            Textbox_DosyaAdi.Location = new System.Drawing.Point(93, 18);
            Textbox_DosyaAdi.Multiline = true;
            Textbox_DosyaAdi.Name = "Textbox_DosyaAdi";
            Textbox_DosyaAdi.ReadOnly = true;
            Textbox_DosyaAdi.Size = new System.Drawing.Size(477, 30);
            Textbox_DosyaAdi.TabIndex = 23;
            // 
            // Textbox_DosyaYolu
            // 
            Textbox_DosyaYolu.BackColor = System.Drawing.SystemColors.Control;
            Textbox_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Textbox_DosyaYolu.ForeColor = System.Drawing.SystemColors.ControlText;
            Textbox_DosyaYolu.Location = new System.Drawing.Point(93, 54);
            Textbox_DosyaYolu.Multiline = true;
            Textbox_DosyaYolu.Name = "Textbox_DosyaYolu";
            Textbox_DosyaYolu.ReadOnly = true;
            Textbox_DosyaYolu.Size = new System.Drawing.Size(477, 30);
            Textbox_DosyaYolu.TabIndex = 25;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Label_DosyaYolu);
            groupBox3.Controls.Add(Label_DosyaAdi);
            groupBox3.Controls.Add(Textbox_DosyaYolu);
            groupBox3.Controls.Add(Buton_Dosya_Sec);
            groupBox3.Controls.Add(Textbox_DosyaAdi);
            groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            groupBox3.Location = new System.Drawing.Point(11, 138);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(581, 143);
            groupBox3.TabIndex = 28;
            groupBox3.TabStop = false;
            // 
            // Label_DosyaYolu
            // 
            Label_DosyaYolu.AutoSize = true;
            Label_DosyaYolu.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_DosyaYolu.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaYolu.Location = new System.Drawing.Point(6, 57);
            Label_DosyaYolu.Name = "Label_DosyaYolu";
            Label_DosyaYolu.Size = new System.Drawing.Size(87, 20);
            Label_DosyaYolu.TabIndex = 28;
            Label_DosyaYolu.Text = "Dosya Yolu";
            // 
            // Label_DosyaAdi
            // 
            Label_DosyaAdi.AutoSize = true;
            Label_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            Label_DosyaAdi.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_DosyaAdi.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Label_DosyaAdi.Location = new System.Drawing.Point(6, 21);
            Label_DosyaAdi.Name = "Label_DosyaAdi";
            Label_DosyaAdi.Size = new System.Drawing.Size(82, 20);
            Label_DosyaAdi.TabIndex = 26;
            Label_DosyaAdi.Text = "Dosya Adı";
            // 
            // Label_Baslik
            // 
            Label_Baslik.AutoSize = true;
            Label_Baslik.BackColor = System.Drawing.SystemColors.Window;
            Label_Baslik.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Label_Baslik.ForeColor = System.Drawing.Color.CornflowerBlue;
            Label_Baslik.Location = new System.Drawing.Point(11, 8);
            Label_Baslik.Name = "Label_Baslik";
            Label_Baslik.Size = new System.Drawing.Size(244, 28);
            Label_Baslik.TabIndex = 33;
            Label_Baslik.Text = "Sertifika Kontrolcüsü";
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = System.Drawing.SystemColors.Control;
            linkLabel1.DisabledLinkColor = System.Drawing.Color.Gray;
            linkLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            linkLabel1.ForeColor = System.Drawing.Color.Gray;
            linkLabel1.LinkColor = System.Drawing.Color.Gray;
            linkLabel1.Location = new System.Drawing.Point(464, 575);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(128, 17);
            linkLabel1.TabIndex = 34;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "yazilimturkiye.com";
            linkLabel1.VisitedLinkColor = System.Drawing.Color.Gray;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Buton_Yardim
            // 
            Buton_Yardim.BackColor = System.Drawing.SystemColors.Control;
            Buton_Yardim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Buton_Yardim.FlatAppearance.BorderSize = 0;
            Buton_Yardim.ImageIndex = 3;
            Buton_Yardim.ImageList = ımageList1;
            Buton_Yardim.Location = new System.Drawing.Point(68, 64);
            Buton_Yardim.Name = "Buton_Yardim";
            Buton_Yardim.Size = new System.Drawing.Size(50, 50);
            Buton_Yardim.TabIndex = 36;
            Buton_Yardim.UseVisualStyleBackColor = false;
            Buton_Yardim.Click += Buton_Yardim_Click;
            // 
            // Label_AltBaslik
            // 
            Label_AltBaslik.AutoSize = true;
            Label_AltBaslik.BackColor = System.Drawing.SystemColors.Window;
            Label_AltBaslik.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_AltBaslik.Location = new System.Drawing.Point(12, 36);
            Label_AltBaslik.Name = "Label_AltBaslik";
            Label_AltBaslik.Size = new System.Drawing.Size(205, 20);
            Label_AltBaslik.TabIndex = 29;
            Label_AltBaslik.Text = "Dijital Sertifika Kontrol Aracı";
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.SystemColors.Window;
            label1.Location = new System.Drawing.Point(0, -1);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(604, 133);
            label1.TabIndex = 37;
            // 
            // Buton_Goruntule
            // 
            Buton_Goruntule.BackColor = System.Drawing.SystemColors.Control;
            Buton_Goruntule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Buton_Goruntule.FlatAppearance.BorderSize = 0;
            Buton_Goruntule.ImageIndex = 2;
            Buton_Goruntule.ImageList = ımageList1;
            Buton_Goruntule.Location = new System.Drawing.Point(12, 64);
            Buton_Goruntule.Name = "Buton_Goruntule";
            Buton_Goruntule.Size = new System.Drawing.Size(50, 50);
            Buton_Goruntule.TabIndex = 38;
            Buton_Goruntule.UseVisualStyleBackColor = false;
            Buton_Goruntule.Click += button_Goruntule_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(604, 601);
            Controls.Add(Buton_Goruntule);
            Controls.Add(Label_AltBaslik);
            Controls.Add(Buton_Yardim);
            Controls.Add(linkLabel1);
            Controls.Add(Label_Baslik);
            Controls.Add(progressBar1);
            Controls.Add(Label_SurumBilgisi);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
        private System.Windows.Forms.Button Buton_Yardim;
        private System.Windows.Forms.Label Label_AltBaslik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Buton_Goruntule;
    }
}