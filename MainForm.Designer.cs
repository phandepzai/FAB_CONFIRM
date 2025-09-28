using System;
namespace FAB_CONFIRM
{
    
    partial class MainForm
    {
        #region KHAI BÁO CÁC THÀNH PHẦN UI
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label LabelAPN;
        private System.Windows.Forms.TextBox TxtAPN;
        private System.Windows.Forms.Label LabelX1;
        private System.Windows.Forms.TextBox TxtX1;
        private System.Windows.Forms.Label LabelY1;
        private System.Windows.Forms.TextBox TxtY1;
        private System.Windows.Forms.Label LabelX2;
        private System.Windows.Forms.TextBox TxtX2;
        private System.Windows.Forms.Label LabelY2;
        private System.Windows.Forms.TextBox TxtY2;
        private System.Windows.Forms.Label LabelX3;
        private System.Windows.Forms.TextBox TxtX3;
        private System.Windows.Forms.Label LabelY3;
        private System.Windows.Forms.TextBox TxtY3;
        private System.Windows.Forms.Button Btn7;
        private System.Windows.Forms.Button Btn8;
        private System.Windows.Forms.Button Btn9;
        private System.Windows.Forms.Button Btn4;
        private System.Windows.Forms.Button Btn5;
        private System.Windows.Forms.Button Btn6;
        private System.Windows.Forms.Button Btn1;
        private System.Windows.Forms.Button Btn2;
        private System.Windows.Forms.Button Btn3;
        private System.Windows.Forms.Button Btn0;
        private System.Windows.Forms.Button BtnDot;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnTenLoi;
        private System.Windows.Forms.Label LabelTenLoi;
        private System.Windows.Forms.Button BtnLevel;
        private System.Windows.Forms.Label LabelLevel;
        private System.Windows.Forms.Button BtnPattern;
        private System.Windows.Forms.Label LabelPattern;
        private System.Windows.Forms.Button BtnMapping;
        private System.Windows.Forms.Label LabelMapping;
        private System.Windows.Forms.Button BtnXacNhan;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label LabelSoCellDaLuu;
        private System.Windows.Forms.Label LabelDaLuuVao;
        private System.Windows.Forms.Label LabelCount;
        private System.Windows.Forms.Button BtnDK;
        private System.Windows.Forms.Button BtnBR;
        private System.Windows.Forms.Label LabelAuthor;
        private System.Windows.Forms.Label LabelTime;
        private System.Windows.Forms.Label LabelDate;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.RichTextBox RichTextStatus;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
#endregion

        #region CÁC THÀNH PHẦN UI CỦA ỨNG DỤNG

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LabelAPN = new System.Windows.Forms.Label();
            this.TxtAPN = new System.Windows.Forms.TextBox();
            this.LabelX1 = new System.Windows.Forms.Label();
            this.TxtX1 = new System.Windows.Forms.TextBox();
            this.LabelY1 = new System.Windows.Forms.Label();
            this.TxtY1 = new System.Windows.Forms.TextBox();
            this.LabelX2 = new System.Windows.Forms.Label();
            this.TxtX2 = new System.Windows.Forms.TextBox();
            this.LabelY2 = new System.Windows.Forms.Label();
            this.TxtY2 = new System.Windows.Forms.TextBox();
            this.LabelX3 = new System.Windows.Forms.Label();
            this.TxtX3 = new System.Windows.Forms.TextBox();
            this.LabelY3 = new System.Windows.Forms.Label();
            this.TxtY3 = new System.Windows.Forms.TextBox();
            this.Btn7 = new System.Windows.Forms.Button();
            this.Btn8 = new System.Windows.Forms.Button();
            this.Btn9 = new System.Windows.Forms.Button();
            this.Btn4 = new System.Windows.Forms.Button();
            this.Btn5 = new System.Windows.Forms.Button();
            this.Btn6 = new System.Windows.Forms.Button();
            this.Btn1 = new System.Windows.Forms.Button();
            this.Btn2 = new System.Windows.Forms.Button();
            this.Btn3 = new System.Windows.Forms.Button();
            this.Btn0 = new System.Windows.Forms.Button();
            this.BtnDot = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnTenLoi = new System.Windows.Forms.Button();
            this.LabelTenLoi = new System.Windows.Forms.Label();
            this.BtnLevel = new System.Windows.Forms.Button();
            this.LabelLevel = new System.Windows.Forms.Label();
            this.BtnPattern = new System.Windows.Forms.Button();
            this.LabelPattern = new System.Windows.Forms.Label();
            this.BtnMapping = new System.Windows.Forms.Button();
            this.LabelMapping = new System.Windows.Forms.Label();
            this.BtnXacNhan = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.LabelSoCellDaLuu = new System.Windows.Forms.Label();
            this.LabelCount = new System.Windows.Forms.Label();
            this.LabelDaLuuVao = new System.Windows.Forms.Label();
            this.LabelTime = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.BtnDK = new System.Windows.Forms.Button();
            this.BtnBR = new System.Windows.Forms.Button();
            this.LabelAuthor = new System.Windows.Forms.Label();
            this.LabelDate = new System.Windows.Forms.Label();
            this.RichTextStatus = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelAPN
            // 
            this.LabelAPN.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAPN.Location = new System.Drawing.Point(12, 19);
            this.LabelAPN.Name = "LabelAPN";
            this.LabelAPN.Size = new System.Drawing.Size(40, 20);
            this.LabelAPN.TabIndex = 43;
            this.LabelAPN.Text = "APN:";
            this.LabelAPN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtAPN
            // 
            this.TxtAPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtAPN.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAPN.Location = new System.Drawing.Point(53, 12);
            this.TxtAPN.MaxLength = 300;
            this.TxtAPN.Multiline = true;
            this.TxtAPN.Name = "TxtAPN";
            this.TxtAPN.Size = new System.Drawing.Size(622, 36);
            this.TxtAPN.TabIndex = 0;
            this.TxtAPN.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelX1
            // 
            this.LabelX1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelX1.Location = new System.Drawing.Point(18, 75);
            this.LabelX1.Name = "LabelX1";
            this.LabelX1.Size = new System.Drawing.Size(35, 31);
            this.LabelX1.TabIndex = 42;
            this.LabelX1.Text = "X1:";
            this.LabelX1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtX1
            // 
            this.TxtX1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtX1.Location = new System.Drawing.Point(53, 70);
            this.TxtX1.MaxLength = 3;
            this.TxtX1.Name = "TxtX1";
            this.TxtX1.Size = new System.Drawing.Size(85, 39);
            this.TxtX1.TabIndex = 1;
            this.TxtX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtX1.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelY1
            // 
            this.LabelY1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelY1.Location = new System.Drawing.Point(159, 75);
            this.LabelY1.Name = "LabelY1";
            this.LabelY1.Size = new System.Drawing.Size(37, 31);
            this.LabelY1.TabIndex = 41;
            this.LabelY1.Text = "Y1:";
            this.LabelY1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtY1
            // 
            this.TxtY1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtY1.Location = new System.Drawing.Point(192, 70);
            this.TxtY1.MaxLength = 3;
            this.TxtY1.Name = "TxtY1";
            this.TxtY1.Size = new System.Drawing.Size(85, 39);
            this.TxtY1.TabIndex = 2;
            this.TxtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtY1.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelX2
            // 
            this.LabelX2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelX2.Location = new System.Drawing.Point(18, 135);
            this.LabelX2.Name = "LabelX2";
            this.LabelX2.Size = new System.Drawing.Size(35, 31);
            this.LabelX2.TabIndex = 40;
            this.LabelX2.Text = "X2:";
            this.LabelX2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtX2
            // 
            this.TxtX2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtX2.Location = new System.Drawing.Point(53, 130);
            this.TxtX2.MaxLength = 3;
            this.TxtX2.Name = "TxtX2";
            this.TxtX2.Size = new System.Drawing.Size(85, 39);
            this.TxtX2.TabIndex = 3;
            this.TxtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtX2.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelY2
            // 
            this.LabelY2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelY2.Location = new System.Drawing.Point(159, 135);
            this.LabelY2.Name = "LabelY2";
            this.LabelY2.Size = new System.Drawing.Size(37, 31);
            this.LabelY2.TabIndex = 39;
            this.LabelY2.Text = "Y2:";
            this.LabelY2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtY2
            // 
            this.TxtY2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtY2.Location = new System.Drawing.Point(192, 130);
            this.TxtY2.MaxLength = 3;
            this.TxtY2.Name = "TxtY2";
            this.TxtY2.Size = new System.Drawing.Size(85, 39);
            this.TxtY2.TabIndex = 4;
            this.TxtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtY2.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelX3
            // 
            this.LabelX3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelX3.Location = new System.Drawing.Point(18, 195);
            this.LabelX3.Name = "LabelX3";
            this.LabelX3.Size = new System.Drawing.Size(35, 31);
            this.LabelX3.TabIndex = 38;
            this.LabelX3.Text = "X3:";
            this.LabelX3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtX3
            // 
            this.TxtX3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtX3.Location = new System.Drawing.Point(53, 190);
            this.TxtX3.MaxLength = 3;
            this.TxtX3.Name = "TxtX3";
            this.TxtX3.Size = new System.Drawing.Size(85, 39);
            this.TxtX3.TabIndex = 5;
            this.TxtX3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtX3.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // LabelY3
            // 
            this.LabelY3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelY3.Location = new System.Drawing.Point(159, 195);
            this.LabelY3.Name = "LabelY3";
            this.LabelY3.Size = new System.Drawing.Size(37, 31);
            this.LabelY3.TabIndex = 37;
            this.LabelY3.Text = "Y3:";
            this.LabelY3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtY3
            // 
            this.TxtY3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtY3.Location = new System.Drawing.Point(192, 190);
            this.TxtY3.MaxLength = 3;
            this.TxtY3.Name = "TxtY3";
            this.TxtY3.Size = new System.Drawing.Size(85, 39);
            this.TxtY3.TabIndex = 6;
            this.TxtY3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtY3.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // Btn7
            // 
            this.Btn7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn7.Location = new System.Drawing.Point(452, 65);
            this.Btn7.Name = "Btn7";
            this.Btn7.Size = new System.Drawing.Size(64, 58);
            this.Btn7.TabIndex = 11;
            this.Btn7.Text = "7";
            this.Btn7.UseVisualStyleBackColor = true;
            this.Btn7.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn8
            // 
            this.Btn8.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn8.Location = new System.Drawing.Point(539, 65);
            this.Btn8.Name = "Btn8";
            this.Btn8.Size = new System.Drawing.Size(64, 58);
            this.Btn8.TabIndex = 12;
            this.Btn8.Text = "8";
            this.Btn8.UseVisualStyleBackColor = true;
            this.Btn8.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn9
            // 
            this.Btn9.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn9.Location = new System.Drawing.Point(624, 65);
            this.Btn9.Name = "Btn9";
            this.Btn9.Size = new System.Drawing.Size(64, 58);
            this.Btn9.TabIndex = 13;
            this.Btn9.Text = "9";
            this.Btn9.UseVisualStyleBackColor = true;
            this.Btn9.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn4
            // 
            this.Btn4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn4.Location = new System.Drawing.Point(452, 136);
            this.Btn4.Name = "Btn4";
            this.Btn4.Size = new System.Drawing.Size(64, 58);
            this.Btn4.TabIndex = 14;
            this.Btn4.Text = "4";
            this.Btn4.UseVisualStyleBackColor = true;
            this.Btn4.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn5
            // 
            this.Btn5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn5.Location = new System.Drawing.Point(539, 136);
            this.Btn5.Name = "Btn5";
            this.Btn5.Size = new System.Drawing.Size(64, 58);
            this.Btn5.TabIndex = 15;
            this.Btn5.Text = "5";
            this.Btn5.UseVisualStyleBackColor = true;
            this.Btn5.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn6
            // 
            this.Btn6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn6.Location = new System.Drawing.Point(624, 136);
            this.Btn6.Name = "Btn6";
            this.Btn6.Size = new System.Drawing.Size(64, 58);
            this.Btn6.TabIndex = 16;
            this.Btn6.Text = "6";
            this.Btn6.UseVisualStyleBackColor = true;
            this.Btn6.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn1
            // 
            this.Btn1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn1.Location = new System.Drawing.Point(452, 207);
            this.Btn1.Name = "Btn1";
            this.Btn1.Size = new System.Drawing.Size(64, 58);
            this.Btn1.TabIndex = 17;
            this.Btn1.Text = "1";
            this.Btn1.UseVisualStyleBackColor = true;
            this.Btn1.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn2
            // 
            this.Btn2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn2.Location = new System.Drawing.Point(539, 207);
            this.Btn2.Name = "Btn2";
            this.Btn2.Size = new System.Drawing.Size(64, 58);
            this.Btn2.TabIndex = 18;
            this.Btn2.Text = "2";
            this.Btn2.UseVisualStyleBackColor = true;
            this.Btn2.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn3
            // 
            this.Btn3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn3.Location = new System.Drawing.Point(624, 207);
            this.Btn3.Name = "Btn3";
            this.Btn3.Size = new System.Drawing.Size(64, 58);
            this.Btn3.TabIndex = 19;
            this.Btn3.Text = "3";
            this.Btn3.UseVisualStyleBackColor = true;
            this.Btn3.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // Btn0
            // 
            this.Btn0.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn0.Location = new System.Drawing.Point(452, 278);
            this.Btn0.Name = "Btn0";
            this.Btn0.Size = new System.Drawing.Size(64, 58);
            this.Btn0.TabIndex = 20;
            this.Btn0.Text = "0";
            this.Btn0.UseVisualStyleBackColor = true;
            this.Btn0.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // BtnDot
            // 
            this.BtnDot.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDot.Location = new System.Drawing.Point(539, 278);
            this.BtnDot.Name = "BtnDot";
            this.BtnDot.Size = new System.Drawing.Size(64, 58);
            this.BtnDot.TabIndex = 21;
            this.BtnDot.Text = ".";
            this.BtnDot.UseVisualStyleBackColor = true;
            this.BtnDot.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Tomato;
            this.BtnDelete.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(624, 278);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(64, 58);
            this.BtnDelete.TabIndex = 22;
            this.BtnDelete.Text = "◄Xóa";
            this.BtnDelete.UseVisualStyleBackColor = false;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnTenLoi
            // 
            this.BtnTenLoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTenLoi.Location = new System.Drawing.Point(40, 261);
            this.BtnTenLoi.Name = "BtnTenLoi";
            this.BtnTenLoi.Size = new System.Drawing.Size(100, 46);
            this.BtnTenLoi.TabIndex = 23;
            this.BtnTenLoi.Text = "TÊN LỖI";
            this.BtnTenLoi.UseVisualStyleBackColor = true;
            this.BtnTenLoi.Click += new System.EventHandler(this.BtnTenLoi_Click);
            // 
            // LabelTenLoi
            // 
            this.LabelTenLoi.AllowDrop = true;
            this.LabelTenLoi.BackColor = System.Drawing.SystemColors.Info;
            this.LabelTenLoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTenLoi.ForeColor = System.Drawing.Color.Blue;
            this.LabelTenLoi.Location = new System.Drawing.Point(155, 264);
            this.LabelTenLoi.Name = "LabelTenLoi";
            this.LabelTenLoi.Size = new System.Drawing.Size(197, 40);
            this.LabelTenLoi.TabIndex = 27;
            this.LabelTenLoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnLevel
            // 
            this.BtnLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLevel.Location = new System.Drawing.Point(40, 391);
            this.BtnLevel.Name = "BtnLevel";
            this.BtnLevel.Size = new System.Drawing.Size(100, 45);
            this.BtnLevel.TabIndex = 25;
            this.BtnLevel.Text = "LEVEL";
            this.BtnLevel.UseVisualStyleBackColor = true;
            this.BtnLevel.Click += new System.EventHandler(this.BtnLevel_Click);
            // 
            // LabelLevel
            // 
            this.LabelLevel.AllowDrop = true;
            this.LabelLevel.BackColor = System.Drawing.SystemColors.Info;
            this.LabelLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLevel.ForeColor = System.Drawing.Color.Blue;
            this.LabelLevel.Location = new System.Drawing.Point(155, 394);
            this.LabelLevel.Name = "LabelLevel";
            this.LabelLevel.Size = new System.Drawing.Size(86, 40);
            this.LabelLevel.TabIndex = 29;
            this.LabelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnPattern
            // 
            this.BtnPattern.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPattern.Location = new System.Drawing.Point(40, 326);
            this.BtnPattern.Name = "BtnPattern";
            this.BtnPattern.Size = new System.Drawing.Size(100, 46);
            this.BtnPattern.TabIndex = 27;
            this.BtnPattern.Text = "PATTERN";
            this.BtnPattern.UseVisualStyleBackColor = true;
            this.BtnPattern.Click += new System.EventHandler(this.BtnPattern_Click);
            // 
            // LabelPattern
            // 
            this.LabelPattern.AllowDrop = true;
            this.LabelPattern.BackColor = System.Drawing.SystemColors.Info;
            this.LabelPattern.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPattern.ForeColor = System.Drawing.Color.Blue;
            this.LabelPattern.Location = new System.Drawing.Point(155, 329);
            this.LabelPattern.Name = "LabelPattern";
            this.LabelPattern.Size = new System.Drawing.Size(197, 40);
            this.LabelPattern.TabIndex = 31;
            this.LabelPattern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnMapping
            // 
            this.BtnMapping.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMapping.Location = new System.Drawing.Point(40, 456);
            this.BtnMapping.Name = "BtnMapping";
            this.BtnMapping.Size = new System.Drawing.Size(100, 46);
            this.BtnMapping.TabIndex = 29;
            this.BtnMapping.Text = "MAPPING";
            this.BtnMapping.UseVisualStyleBackColor = true;
            this.BtnMapping.Click += new System.EventHandler(this.BtnMapping_Click);
            // 
            // LabelMapping
            // 
            this.LabelMapping.AllowDrop = true;
            this.LabelMapping.BackColor = System.Drawing.SystemColors.Info;
            this.LabelMapping.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMapping.ForeColor = System.Drawing.Color.Blue;
            this.LabelMapping.Location = new System.Drawing.Point(155, 460);
            this.LabelMapping.Name = "LabelMapping";
            this.LabelMapping.Size = new System.Drawing.Size(197, 40);
            this.LabelMapping.TabIndex = 33;
            this.LabelMapping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnXacNhan
            // 
            this.BtnXacNhan.BackColor = System.Drawing.Color.SeaGreen;
            this.BtnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnXacNhan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnXacNhan.Location = new System.Drawing.Point(471, 365);
            this.BtnXacNhan.Name = "BtnXacNhan";
            this.BtnXacNhan.Size = new System.Drawing.Size(88, 45);
            this.BtnXacNhan.TabIndex = 31;
            this.BtnXacNhan.Text = "SAVE";
            this.BtnXacNhan.UseVisualStyleBackColor = false;
            this.BtnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.BackColor = System.Drawing.Color.Goldenrod;
            this.BtnReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReset.Location = new System.Drawing.Point(578, 365);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(88, 45);
            this.BtnReset.TabIndex = 32;
            this.BtnReset.Text = "RESET";
            this.BtnReset.UseVisualStyleBackColor = false;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // LabelSoCellDaLuu
            // 
            this.LabelSoCellDaLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelSoCellDaLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSoCellDaLuu.ForeColor = System.Drawing.Color.Blue;
            this.LabelSoCellDaLuu.Location = new System.Drawing.Point(518, 436);
            this.LabelSoCellDaLuu.Name = "LabelSoCellDaLuu";
            this.LabelSoCellDaLuu.Size = new System.Drawing.Size(100, 20);
            this.LabelSoCellDaLuu.TabIndex = 4;
            this.LabelSoCellDaLuu.Text = "Số cell đã lưu:";
            // 
            // LabelCount
            // 
            this.LabelCount.AutoSize = true;
            this.LabelCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCount.Location = new System.Drawing.Point(608, 436);
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.Size = new System.Drawing.Size(15, 17);
            this.LabelCount.TabIndex = 3;
            this.LabelCount.Text = "0";
            // 
            // LabelDaLuuVao
            // 
            this.LabelDaLuuVao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelDaLuuVao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDaLuuVao.ForeColor = System.Drawing.Color.Blue;
            this.LabelDaLuuVao.Location = new System.Drawing.Point(16, 546);
            this.LabelDaLuuVao.Name = "LabelDaLuuVao";
            this.LabelDaLuuVao.Size = new System.Drawing.Size(448, 20);
            this.LabelDaLuuVao.TabIndex = 2;
            // 
            // LabelTime
            // 
            this.LabelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTime.AutoSize = true;
            this.LabelTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTime.ForeColor = System.Drawing.Color.Red;
            this.LabelTime.Location = new System.Drawing.Point(547, 465);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(0, 17);
            this.LabelTime.TabIndex = 1;
            this.LabelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::FAB_CONFIRM.Properties.Resources.OYX_MAP;
            this.pictureBox.Location = new System.Drawing.Point(294, 69);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(143, 170);
            this.pictureBox.TabIndex = 44;
            this.pictureBox.TabStop = false;
            // 
            // BtnDK
            // 
            this.BtnDK.BackColor = System.Drawing.Color.DimGray;
            this.BtnDK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDK.ForeColor = System.Drawing.Color.White;
            this.BtnDK.Location = new System.Drawing.Point(247, 392);
            this.BtnDK.Name = "BtnDK";
            this.BtnDK.Size = new System.Drawing.Size(50, 45);
            this.BtnDK.TabIndex = 30;
            this.BtnDK.Text = "DK";
            this.BtnDK.UseVisualStyleBackColor = false;
            this.BtnDK.Click += new System.EventHandler(this.BtnDK_Click);
            // 
            // BtnBR
            // 
            this.BtnBR.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBR.Location = new System.Drawing.Point(303, 392);
            this.BtnBR.Name = "BtnBR";
            this.BtnBR.Size = new System.Drawing.Size(50, 45);
            this.BtnBR.TabIndex = 31;
            this.BtnBR.Text = "BR";
            this.BtnBR.UseVisualStyleBackColor = true;
            this.BtnBR.Click += new System.EventHandler(this.BtnBR_Click);
            // 
            // LabelAuthor
            // 
            this.LabelAuthor.AutoSize = true;
            this.LabelAuthor.BackColor = System.Drawing.Color.Transparent;
            this.LabelAuthor.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAuthor.ForeColor = System.Drawing.Color.Silver;
            this.LabelAuthor.Location = new System.Drawing.Point(598, 583);
            this.LabelAuthor.Name = "LabelAuthor";
            this.LabelAuthor.Size = new System.Drawing.Size(97, 13);
            this.LabelAuthor.TabIndex = 45;
            this.LabelAuthor.Text = "©Nông Văn Phấn";
            // 
            // LabelDate
            // 
            this.LabelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelDate.AutoSize = true;
            this.LabelDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDate.ForeColor = System.Drawing.Color.Red;
            this.LabelDate.Location = new System.Drawing.Point(538, 490);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Size = new System.Drawing.Size(0, 17);
            this.LabelDate.TabIndex = 46;
            this.LabelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RichTextStatus
            // 
            this.RichTextStatus.AccessibleDescription = "";
            this.RichTextStatus.BackColor = System.Drawing.SystemColors.Control;
            this.RichTextStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RichTextStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextStatus.ForeColor = System.Drawing.Color.CadetBlue;
            this.RichTextStatus.Location = new System.Drawing.Point(12, 518);
            this.RichTextStatus.Name = "RichTextStatus";
            this.RichTextStatus.Size = new System.Drawing.Size(586, 91);
            this.RichTextStatus.TabIndex = 47;
            this.RichTextStatus.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(699, 601);
            this.Controls.Add(this.LabelAuthor);
            this.Controls.Add(this.RichTextStatus);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.LabelTime);
            this.Controls.Add(this.LabelDaLuuVao);
            this.Controls.Add(this.LabelCount);
            this.Controls.Add(this.LabelSoCellDaLuu);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.BtnXacNhan);
            this.Controls.Add(this.LabelMapping);
            this.Controls.Add(this.BtnMapping);
            this.Controls.Add(this.LabelPattern);
            this.Controls.Add(this.BtnPattern);
            this.Controls.Add(this.LabelLevel);
            this.Controls.Add(this.BtnLevel);
            this.Controls.Add(this.BtnDK);
            this.Controls.Add(this.BtnBR);
            this.Controls.Add(this.LabelTenLoi);
            this.Controls.Add(this.BtnTenLoi);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnDot);
            this.Controls.Add(this.Btn0);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.Btn6);
            this.Controls.Add(this.Btn5);
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.Btn9);
            this.Controls.Add(this.Btn8);
            this.Controls.Add(this.Btn7);
            this.Controls.Add(this.TxtY3);
            this.Controls.Add(this.LabelY3);
            this.Controls.Add(this.TxtX3);
            this.Controls.Add(this.LabelX3);
            this.Controls.Add(this.TxtY2);
            this.Controls.Add(this.LabelY2);
            this.Controls.Add(this.TxtX2);
            this.Controls.Add(this.LabelX2);
            this.Controls.Add(this.TxtY1);
            this.Controls.Add(this.LabelY1);
            this.Controls.Add(this.TxtX1);
            this.Controls.Add(this.LabelX1);
            this.Controls.Add(this.TxtAPN);
            this.Controls.Add(this.LabelAPN);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAB CONFIRM";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}