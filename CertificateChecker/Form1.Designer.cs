
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Buton_Dosya_Sec = new System.Windows.Forms.Button();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Label_SurumBilgisi = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PictureBox_Durum = new System.Windows.Forms.PictureBox();
            this.Label_Durum = new System.Windows.Forms.Label();
            this.Textbox_Durum = new System.Windows.Forms.TextBox();
            this.Textbox_Bitis = new System.Windows.Forms.TextBox();
            this.Label_Bitis = new System.Windows.Forms.Label();
            this.Textbox_Serino = new System.Windows.Forms.TextBox();
            this.Textbox_Baslangic = new System.Windows.Forms.TextBox();
            this.Label_Baslangic = new System.Windows.Forms.Label();
            this.Label_Serino = new System.Windows.Forms.Label();
            this.Label_Algoritma = new System.Windows.Forms.Label();
            this.Textbox_Algoritma = new System.Windows.Forms.TextBox();
            this.Textbox_Verilen = new System.Windows.Forms.TextBox();
            this.Textbox_Veren = new System.Windows.Forms.TextBox();
            this.Label_Veren = new System.Windows.Forms.Label();
            this.Label_Verilen = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Textbox_DosyaAdi = new System.Windows.Forms.TextBox();
            this.Textbox_DosyaYolu = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Label_DosyaYolu = new System.Windows.Forms.Label();
            this.Label_DosyaAdi = new System.Windows.Forms.Label();
            this.Buton_Kapat = new System.Windows.Forms.Button();
            this.Buton_Kucult = new System.Windows.Forms.Button();
            this.Picturebox_Logo = new System.Windows.Forms.PictureBox();
            this.Label_Baslik = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label_Arkaplan = new System.Windows.Forms.Label();
            this.Buton_Yardim = new System.Windows.Forms.Button();
            this.Label_AltBaslik = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Durum)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Buton_Dosya_Sec
            // 
            this.Buton_Dosya_Sec.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Buton_Dosya_Sec.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Buton_Dosya_Sec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buton_Dosya_Sec.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Buton_Dosya_Sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Buton_Dosya_Sec.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Buton_Dosya_Sec.ImageIndex = 0;
            this.Buton_Dosya_Sec.ImageList = this.ımageList1;
            this.Buton_Dosya_Sec.Location = new System.Drawing.Point(86, 90);
            this.Buton_Dosya_Sec.Name = "Buton_Dosya_Sec";
            this.Buton_Dosya_Sec.Size = new System.Drawing.Size(477, 40);
            this.Buton_Dosya_Sec.TabIndex = 1;
            this.Buton_Dosya_Sec.Text = "Sertifika Seç";
            this.Buton_Dosya_Sec.UseVisualStyleBackColor = false;
            this.Buton_Dosya_Sec.Click += new System.EventHandler(this.Buton_Dosya_Sec_Click);
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ımageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList1.ImageStream")));
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList1.Images.SetKeyName(0, "certificate.png");
            this.ımageList1.Images.SetKeyName(1, "terminal.png");
            // 
            // Label_SurumBilgisi
            // 
            this.Label_SurumBilgisi.AutoSize = true;
            this.Label_SurumBilgisi.BackColor = System.Drawing.SystemColors.Control;
            this.Label_SurumBilgisi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label_SurumBilgisi.ForeColor = System.Drawing.Color.Gray;
            this.Label_SurumBilgisi.Location = new System.Drawing.Point(145, 546);
            this.Label_SurumBilgisi.Name = "Label_SurumBilgisi";
            this.Label_SurumBilgisi.Size = new System.Drawing.Size(30, 16);
            this.Label_SurumBilgisi.TabIndex = 5;
            this.Label_SurumBilgisi.Text = "v1.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PictureBox_Durum);
            this.groupBox1.Controls.Add(this.Label_Durum);
            this.groupBox1.Controls.Add(this.Textbox_Durum);
            this.groupBox1.Controls.Add(this.Textbox_Bitis);
            this.groupBox1.Controls.Add(this.Label_Bitis);
            this.groupBox1.Controls.Add(this.Textbox_Serino);
            this.groupBox1.Controls.Add(this.Textbox_Baslangic);
            this.groupBox1.Controls.Add(this.Label_Baslangic);
            this.groupBox1.Controls.Add(this.Label_Serino);
            this.groupBox1.Controls.Add(this.Label_Algoritma);
            this.groupBox1.Controls.Add(this.Textbox_Algoritma);
            this.groupBox1.Controls.Add(this.Textbox_Verilen);
            this.groupBox1.Controls.Add(this.Textbox_Veren);
            this.groupBox1.Controls.Add(this.Label_Veren);
            this.groupBox1.Controls.Add(this.Label_Verilen);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(11, 252);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 285);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // PictureBox_Durum
            // 
            this.PictureBox_Durum.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Durum.Location = new System.Drawing.Point(539, 248);
            this.PictureBox_Durum.Name = "PictureBox_Durum";
            this.PictureBox_Durum.Size = new System.Drawing.Size(20, 20);
            this.PictureBox_Durum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_Durum.TabIndex = 0;
            this.PictureBox_Durum.TabStop = false;
            // 
            // Label_Durum
            // 
            this.Label_Durum.AutoSize = true;
            this.Label_Durum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Durum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Durum.Location = new System.Drawing.Point(300, 247);
            this.Label_Durum.Name = "Label_Durum";
            this.Label_Durum.Size = new System.Drawing.Size(49, 16);
            this.Label_Durum.TabIndex = 25;
            this.Label_Durum.Text = "Durum";
            // 
            // Textbox_Durum
            // 
            this.Textbox_Durum.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Durum.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Durum.Location = new System.Drawing.Point(363, 243);
            this.Textbox_Durum.Multiline = true;
            this.Textbox_Durum.Name = "Textbox_Durum";
            this.Textbox_Durum.ReadOnly = true;
            this.Textbox_Durum.Size = new System.Drawing.Size(200, 30);
            this.Textbox_Durum.TabIndex = 9;
            // 
            // Textbox_Bitis
            // 
            this.Textbox_Bitis.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Bitis.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Bitis.Location = new System.Drawing.Point(363, 207);
            this.Textbox_Bitis.Multiline = true;
            this.Textbox_Bitis.Name = "Textbox_Bitis";
            this.Textbox_Bitis.ReadOnly = true;
            this.Textbox_Bitis.Size = new System.Drawing.Size(200, 30);
            this.Textbox_Bitis.TabIndex = 7;
            // 
            // Label_Bitis
            // 
            this.Label_Bitis.AutoSize = true;
            this.Label_Bitis.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Bitis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Bitis.Location = new System.Drawing.Point(300, 211);
            this.Label_Bitis.Name = "Label_Bitis";
            this.Label_Bitis.Size = new System.Drawing.Size(34, 16);
            this.Label_Bitis.TabIndex = 23;
            this.Label_Bitis.Text = "Bitiş";
            // 
            // Textbox_Serino
            // 
            this.Textbox_Serino.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Serino.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Serino.Location = new System.Drawing.Point(87, 171);
            this.Textbox_Serino.Multiline = true;
            this.Textbox_Serino.Name = "Textbox_Serino";
            this.Textbox_Serino.ReadOnly = true;
            this.Textbox_Serino.Size = new System.Drawing.Size(476, 30);
            this.Textbox_Serino.TabIndex = 5;
            // 
            // Textbox_Baslangic
            // 
            this.Textbox_Baslangic.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Baslangic.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Baslangic.Location = new System.Drawing.Point(87, 207);
            this.Textbox_Baslangic.Multiline = true;
            this.Textbox_Baslangic.Name = "Textbox_Baslangic";
            this.Textbox_Baslangic.ReadOnly = true;
            this.Textbox_Baslangic.Size = new System.Drawing.Size(200, 30);
            this.Textbox_Baslangic.TabIndex = 6;
            // 
            // Label_Baslangic
            // 
            this.Label_Baslangic.AutoSize = true;
            this.Label_Baslangic.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Baslangic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Baslangic.Location = new System.Drawing.Point(6, 212);
            this.Label_Baslangic.Name = "Label_Baslangic";
            this.Label_Baslangic.Size = new System.Drawing.Size(69, 16);
            this.Label_Baslangic.TabIndex = 10;
            this.Label_Baslangic.Text = "Başlangıç";
            // 
            // Label_Serino
            // 
            this.Label_Serino.AutoSize = true;
            this.Label_Serino.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Serino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Serino.Location = new System.Drawing.Point(6, 175);
            this.Label_Serino.Name = "Label_Serino";
            this.Label_Serino.Size = new System.Drawing.Size(58, 16);
            this.Label_Serino.TabIndex = 19;
            this.Label_Serino.Text = "Seri No ";
            // 
            // Label_Algoritma
            // 
            this.Label_Algoritma.AutoSize = true;
            this.Label_Algoritma.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Algoritma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Algoritma.Location = new System.Drawing.Point(6, 247);
            this.Label_Algoritma.Name = "Label_Algoritma";
            this.Label_Algoritma.Size = new System.Drawing.Size(69, 16);
            this.Label_Algoritma.TabIndex = 22;
            this.Label_Algoritma.Text = "Algoritma";
            // 
            // Textbox_Algoritma
            // 
            this.Textbox_Algoritma.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Algoritma.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Algoritma.Location = new System.Drawing.Point(87, 243);
            this.Textbox_Algoritma.Multiline = true;
            this.Textbox_Algoritma.Name = "Textbox_Algoritma";
            this.Textbox_Algoritma.ReadOnly = true;
            this.Textbox_Algoritma.Size = new System.Drawing.Size(200, 30);
            this.Textbox_Algoritma.TabIndex = 8;
            // 
            // Textbox_Verilen
            // 
            this.Textbox_Verilen.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Verilen.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Verilen.Location = new System.Drawing.Point(87, 95);
            this.Textbox_Verilen.Multiline = true;
            this.Textbox_Verilen.Name = "Textbox_Verilen";
            this.Textbox_Verilen.ReadOnly = true;
            this.Textbox_Verilen.Size = new System.Drawing.Size(476, 70);
            this.Textbox_Verilen.TabIndex = 4;
            // 
            // Textbox_Veren
            // 
            this.Textbox_Veren.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_Veren.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_Veren.Location = new System.Drawing.Point(87, 19);
            this.Textbox_Veren.Multiline = true;
            this.Textbox_Veren.Name = "Textbox_Veren";
            this.Textbox_Veren.ReadOnly = true;
            this.Textbox_Veren.Size = new System.Drawing.Size(476, 70);
            this.Textbox_Veren.TabIndex = 3;
            // 
            // Label_Veren
            // 
            this.Label_Veren.AutoSize = true;
            this.Label_Veren.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Veren.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Veren.Location = new System.Drawing.Point(6, 23);
            this.Label_Veren.Name = "Label_Veren";
            this.Label_Veren.Size = new System.Drawing.Size(44, 16);
            this.Label_Veren.TabIndex = 9;
            this.Label_Veren.Text = "Veren";
            // 
            // Label_Verilen
            // 
            this.Label_Verilen.AutoSize = true;
            this.Label_Verilen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_Verilen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_Verilen.Location = new System.Drawing.Point(6, 99);
            this.Label_Verilen.Name = "Label_Verilen";
            this.Label_Verilen.Size = new System.Drawing.Size(52, 16);
            this.Label_Verilen.TabIndex = 8;
            this.Label_Verilen.Text = "Verilen";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 93);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(598, 5);
            this.progressBar1.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Textbox_DosyaAdi
            // 
            this.Textbox_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_DosyaAdi.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_DosyaAdi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Textbox_DosyaAdi.Location = new System.Drawing.Point(86, 18);
            this.Textbox_DosyaAdi.Multiline = true;
            this.Textbox_DosyaAdi.Name = "Textbox_DosyaAdi";
            this.Textbox_DosyaAdi.ReadOnly = true;
            this.Textbox_DosyaAdi.Size = new System.Drawing.Size(477, 30);
            this.Textbox_DosyaAdi.TabIndex = 23;
            // 
            // Textbox_DosyaYolu
            // 
            this.Textbox_DosyaYolu.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_DosyaYolu.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Textbox_DosyaYolu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Textbox_DosyaYolu.Location = new System.Drawing.Point(86, 54);
            this.Textbox_DosyaYolu.Multiline = true;
            this.Textbox_DosyaYolu.Name = "Textbox_DosyaYolu";
            this.Textbox_DosyaYolu.ReadOnly = true;
            this.Textbox_DosyaYolu.Size = new System.Drawing.Size(477, 30);
            this.Textbox_DosyaYolu.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Label_DosyaYolu);
            this.groupBox3.Controls.Add(this.Label_DosyaAdi);
            this.groupBox3.Controls.Add(this.Textbox_DosyaYolu);
            this.groupBox3.Controls.Add(this.Buton_Dosya_Sec);
            this.groupBox3.Controls.Add(this.Textbox_DosyaAdi);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(12, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(575, 143);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // Label_DosyaYolu
            // 
            this.Label_DosyaYolu.AutoSize = true;
            this.Label_DosyaYolu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_DosyaYolu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_DosyaYolu.Location = new System.Drawing.Point(5, 58);
            this.Label_DosyaYolu.Name = "Label_DosyaYolu";
            this.Label_DosyaYolu.Size = new System.Drawing.Size(76, 16);
            this.Label_DosyaYolu.TabIndex = 28;
            this.Label_DosyaYolu.Text = "Dosya Yolu";
            // 
            // Label_DosyaAdi
            // 
            this.Label_DosyaAdi.AutoSize = true;
            this.Label_DosyaAdi.BackColor = System.Drawing.SystemColors.Control;
            this.Label_DosyaAdi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_DosyaAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label_DosyaAdi.Location = new System.Drawing.Point(5, 22);
            this.Label_DosyaAdi.Name = "Label_DosyaAdi";
            this.Label_DosyaAdi.Size = new System.Drawing.Size(70, 16);
            this.Label_DosyaAdi.TabIndex = 26;
            this.Label_DosyaAdi.Text = "Dosya Adı";
            // 
            // Buton_Kapat
            // 
            this.Buton_Kapat.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Buton_Kapat.BackgroundImage = global::CertificateChecker.Properties.Resources.kapat;
            this.Buton_Kapat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Buton_Kapat.FlatAppearance.BorderSize = 0;
            this.Buton_Kapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buton_Kapat.Location = new System.Drawing.Point(550, 12);
            this.Buton_Kapat.Name = "Buton_Kapat";
            this.Buton_Kapat.Size = new System.Drawing.Size(25, 25);
            this.Buton_Kapat.TabIndex = 30;
            this.Buton_Kapat.UseVisualStyleBackColor = false;
            this.Buton_Kapat.Click += new System.EventHandler(this.Buton_Kapat_Click);
            // 
            // Buton_Kucult
            // 
            this.Buton_Kucult.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Buton_Kucult.BackgroundImage = global::CertificateChecker.Properties.Resources.kucult;
            this.Buton_Kucult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Buton_Kucult.FlatAppearance.BorderSize = 0;
            this.Buton_Kucult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buton_Kucult.Location = new System.Drawing.Point(519, 12);
            this.Buton_Kucult.Name = "Buton_Kucult";
            this.Buton_Kucult.Size = new System.Drawing.Size(25, 25);
            this.Buton_Kucult.TabIndex = 31;
            this.Buton_Kucult.UseVisualStyleBackColor = false;
            this.Buton_Kucult.Click += new System.EventHandler(this.Buton_Kucult_Click);
            // 
            // Picturebox_Logo
            // 
            this.Picturebox_Logo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Picturebox_Logo.Image = global::CertificateChecker.Properties.Resources.certificate;
            this.Picturebox_Logo.Location = new System.Drawing.Point(12, 12);
            this.Picturebox_Logo.Name = "Picturebox_Logo";
            this.Picturebox_Logo.Size = new System.Drawing.Size(75, 70);
            this.Picturebox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picturebox_Logo.TabIndex = 32;
            this.Picturebox_Logo.TabStop = false;
            // 
            // Label_Baslik
            // 
            this.Label_Baslik.AutoSize = true;
            this.Label_Baslik.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Label_Baslik.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label_Baslik.ForeColor = System.Drawing.Color.Black;
            this.Label_Baslik.Location = new System.Drawing.Point(97, 26);
            this.Label_Baslik.Name = "Label_Baslik";
            this.Label_Baslik.Size = new System.Drawing.Size(203, 24);
            this.Label_Baslik.TabIndex = 33;
            this.Label_Baslik.Text = "Sertifika Kontrolcüsü";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabel1.ForeColor = System.Drawing.Color.Gray;
            this.linkLabel1.LinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.Location = new System.Drawing.Point(11, 545);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(128, 17);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "yazilimturkiye.com";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Label_Arkaplan
            // 
            this.Label_Arkaplan.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Label_Arkaplan.Location = new System.Drawing.Point(0, 0);
            this.Label_Arkaplan.Name = "Label_Arkaplan";
            this.Label_Arkaplan.Size = new System.Drawing.Size(598, 93);
            this.Label_Arkaplan.TabIndex = 35;
            this.Label_Arkaplan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Arkaplan_MouseDown);
            this.Label_Arkaplan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Arkaplan_MouseMove);
            this.Label_Arkaplan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Label_Arkaplan_MouseUp);
            // 
            // Buton_Yardim
            // 
            this.Buton_Yardim.BackColor = System.Drawing.SystemColors.Control;
            this.Buton_Yardim.BackgroundImage = global::CertificateChecker.Properties.Resources.yardim;
            this.Buton_Yardim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Buton_Yardim.FlatAppearance.BorderSize = 0;
            this.Buton_Yardim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buton_Yardim.Location = new System.Drawing.Point(562, 542);
            this.Buton_Yardim.Name = "Buton_Yardim";
            this.Buton_Yardim.Size = new System.Drawing.Size(25, 25);
            this.Buton_Yardim.TabIndex = 36;
            this.Buton_Yardim.UseVisualStyleBackColor = false;
            this.Buton_Yardim.Click += new System.EventHandler(this.Buton_Yardim_Click);
            // 
            // Label_AltBaslik
            // 
            this.Label_AltBaslik.AutoSize = true;
            this.Label_AltBaslik.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Label_AltBaslik.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label_AltBaslik.Location = new System.Drawing.Point(99, 51);
            this.Label_AltBaslik.Name = "Label_AltBaslik";
            this.Label_AltBaslik.Size = new System.Drawing.Size(185, 17);
            this.Label_AltBaslik.TabIndex = 29;
            this.Label_AltBaslik.Text = "Dijital Sertifika Kontrol Aracı";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 575);
            this.ControlBox = false;
            this.Controls.Add(this.Label_AltBaslik);
            this.Controls.Add(this.Buton_Yardim);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Label_Baslik);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Picturebox_Logo);
            this.Controls.Add(this.Label_SurumBilgisi);
            this.Controls.Add(this.Buton_Kucult);
            this.Controls.Add(this.Buton_Kapat);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Label_Arkaplan);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sertifika Kontrolcüsü";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Durum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button Buton_Kapat;
        private System.Windows.Forms.Button Buton_Kucult;
        private System.Windows.Forms.PictureBox Picturebox_Logo;
        private System.Windows.Forms.Label Label_Baslik;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label Label_Arkaplan;
        private System.Windows.Forms.Button Buton_Yardim;
        private System.Windows.Forms.Label Label_AltBaslik;
    }
}