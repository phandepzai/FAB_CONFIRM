using System;
namespace FAB_CONFIRM
{
    
    partial class MainForm
    {
        #region KHAI BÁO CÁC THÀNH PHẦN UI
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelAPN;
        private System.Windows.Forms.TextBox txtAPN;
        private System.Windows.Forms.Label labelX1;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.Label labelY1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.Label labelX2;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.Label labelY2;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.Label labelX3;
        private System.Windows.Forms.TextBox txtX3;
        private System.Windows.Forms.Label labelY3;
        private System.Windows.Forms.TextBox txtY3;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnTenLoi;
        private System.Windows.Forms.Label labelTenLoi;
        private System.Windows.Forms.Button btnLevel;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Button btnPattern;
        private System.Windows.Forms.Label labelPattern;
        private System.Windows.Forms.Button btnMapping;
        private System.Windows.Forms.Label labelMapping;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label labelSoCellDaLuu;
        private System.Windows.Forms.Label labelDaLuuVao;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button btnDK;
        private System.Windows.Forms.Button btnBR;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.PictureBox pictureBox;
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
            this.labelAPN = new System.Windows.Forms.Label();
            this.txtAPN = new System.Windows.Forms.TextBox();
            this.labelX1 = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.labelY1 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.labelX2 = new System.Windows.Forms.Label();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.labelY2 = new System.Windows.Forms.Label();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.labelX3 = new System.Windows.Forms.Label();
            this.txtX3 = new System.Windows.Forms.TextBox();
            this.labelY3 = new System.Windows.Forms.Label();
            this.txtY3 = new System.Windows.Forms.TextBox();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnTenLoi = new System.Windows.Forms.Button();
            this.labelTenLoi = new System.Windows.Forms.Label();
            this.btnLevel = new System.Windows.Forms.Button();
            this.labelLevel = new System.Windows.Forms.Label();
            this.btnPattern = new System.Windows.Forms.Button();
            this.labelPattern = new System.Windows.Forms.Label();
            this.btnMapping = new System.Windows.Forms.Button();
            this.labelMapping = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.labelSoCellDaLuu = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelDaLuuVao = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnDK = new System.Windows.Forms.Button();
            this.btnBR = new System.Windows.Forms.Button();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAPN
            // 
            this.labelAPN.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAPN.Location = new System.Drawing.Point(12, 19);
            this.labelAPN.Name = "labelAPN";
            this.labelAPN.Size = new System.Drawing.Size(40, 20);
            this.labelAPN.TabIndex = 43;
            this.labelAPN.Text = "APN:";
            this.labelAPN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAPN
            // 
            this.txtAPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAPN.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPN.Location = new System.Drawing.Point(53, 12);
            this.txtAPN.MaxLength = 300;
            this.txtAPN.Multiline = true;
            this.txtAPN.Name = "txtAPN";
            this.txtAPN.Size = new System.Drawing.Size(622, 36);
            this.txtAPN.TabIndex = 0;
            this.txtAPN.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(18, 75);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(35, 31);
            this.labelX1.TabIndex = 42;
            this.labelX1.Text = "X1:";
            this.labelX1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtX1
            // 
            this.txtX1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX1.Location = new System.Drawing.Point(53, 70);
            this.txtX1.MaxLength = 3;
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(85, 39);
            this.txtX1.TabIndex = 1;
            this.txtX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtX1.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelY1
            // 
            this.labelY1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelY1.Location = new System.Drawing.Point(159, 75);
            this.labelY1.Name = "labelY1";
            this.labelY1.Size = new System.Drawing.Size(37, 31);
            this.labelY1.TabIndex = 41;
            this.labelY1.Text = "Y1:";
            this.labelY1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtY1
            // 
            this.txtY1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY1.Location = new System.Drawing.Point(192, 70);
            this.txtY1.MaxLength = 3;
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(85, 39);
            this.txtY1.TabIndex = 2;
            this.txtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtY1.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(18, 135);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(35, 31);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "X2:";
            this.labelX2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtX2
            // 
            this.txtX2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX2.Location = new System.Drawing.Point(53, 130);
            this.txtX2.MaxLength = 3;
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(85, 39);
            this.txtX2.TabIndex = 3;
            this.txtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtX2.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelY2
            // 
            this.labelY2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelY2.Location = new System.Drawing.Point(159, 135);
            this.labelY2.Name = "labelY2";
            this.labelY2.Size = new System.Drawing.Size(37, 31);
            this.labelY2.TabIndex = 39;
            this.labelY2.Text = "Y2:";
            this.labelY2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtY2
            // 
            this.txtY2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY2.Location = new System.Drawing.Point(192, 130);
            this.txtY2.MaxLength = 3;
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(85, 39);
            this.txtY2.TabIndex = 4;
            this.txtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtY2.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(18, 195);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(35, 31);
            this.labelX3.TabIndex = 38;
            this.labelX3.Text = "X3:";
            this.labelX3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtX3
            // 
            this.txtX3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX3.Location = new System.Drawing.Point(53, 190);
            this.txtX3.MaxLength = 3;
            this.txtX3.Name = "txtX3";
            this.txtX3.Size = new System.Drawing.Size(85, 39);
            this.txtX3.TabIndex = 5;
            this.txtX3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtX3.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // labelY3
            // 
            this.labelY3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelY3.Location = new System.Drawing.Point(159, 195);
            this.labelY3.Name = "labelY3";
            this.labelY3.Size = new System.Drawing.Size(37, 31);
            this.labelY3.TabIndex = 37;
            this.labelY3.Text = "Y3:";
            this.labelY3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtY3
            // 
            this.txtY3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY3.Location = new System.Drawing.Point(192, 190);
            this.txtY3.MaxLength = 3;
            this.txtY3.Name = "txtY3";
            this.txtY3.Size = new System.Drawing.Size(85, 39);
            this.txtY3.TabIndex = 6;
            this.txtY3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtY3.Enter += new System.EventHandler(this.OnTextBoxEnter);
            // 
            // btn7
            // 
            this.btn7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(452, 65);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(64, 58);
            this.btn7.TabIndex = 11;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(539, 65);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(64, 58);
            this.btn8.TabIndex = 12;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn9
            // 
            this.btn9.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(624, 65);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(64, 58);
            this.btn9.TabIndex = 13;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(452, 136);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(64, 58);
            this.btn4.TabIndex = 14;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(539, 136);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(64, 58);
            this.btn5.TabIndex = 15;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(624, 136);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(64, 58);
            this.btn6.TabIndex = 16;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(452, 207);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(64, 58);
            this.btn1.TabIndex = 17;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(539, 207);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(64, 58);
            this.btn2.TabIndex = 18;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(624, 207);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(64, 58);
            this.btn3.TabIndex = 19;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btn0
            // 
            this.btn0.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(452, 278);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(64, 58);
            this.btn0.TabIndex = 20;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btnDot
            // 
            this.btnDot.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDot.Location = new System.Drawing.Point(539, 278);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(64, 58);
            this.btnDot.TabIndex = 21;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = true;
            this.btnDot.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Tomato;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(624, 278);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 58);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "◄Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnTenLoi
            // 
            this.btnTenLoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTenLoi.Location = new System.Drawing.Point(40, 261);
            this.btnTenLoi.Name = "btnTenLoi";
            this.btnTenLoi.Size = new System.Drawing.Size(100, 46);
            this.btnTenLoi.TabIndex = 23;
            this.btnTenLoi.Text = "TÊN LỖI";
            this.btnTenLoi.UseVisualStyleBackColor = true;
            this.btnTenLoi.Click += new System.EventHandler(this.btnTenLoi_Click);
            // 
            // labelTenLoi
            // 
            this.labelTenLoi.AllowDrop = true;
            this.labelTenLoi.BackColor = System.Drawing.SystemColors.Info;
            this.labelTenLoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenLoi.ForeColor = System.Drawing.Color.Blue;
            this.labelTenLoi.Location = new System.Drawing.Point(155, 264);
            this.labelTenLoi.Name = "labelTenLoi";
            this.labelTenLoi.Size = new System.Drawing.Size(197, 40);
            this.labelTenLoi.TabIndex = 27;
            this.labelTenLoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLevel
            // 
            this.btnLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLevel.Location = new System.Drawing.Point(40, 391);
            this.btnLevel.Name = "btnLevel";
            this.btnLevel.Size = new System.Drawing.Size(100, 45);
            this.btnLevel.TabIndex = 25;
            this.btnLevel.Text = "LEVEL";
            this.btnLevel.UseVisualStyleBackColor = true;
            this.btnLevel.Click += new System.EventHandler(this.btnLevel_Click);
            // 
            // labelLevel
            // 
            this.labelLevel.AllowDrop = true;
            this.labelLevel.BackColor = System.Drawing.SystemColors.Info;
            this.labelLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevel.ForeColor = System.Drawing.Color.Blue;
            this.labelLevel.Location = new System.Drawing.Point(155, 394);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(86, 40);
            this.labelLevel.TabIndex = 29;
            this.labelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPattern
            // 
            this.btnPattern.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPattern.Location = new System.Drawing.Point(40, 326);
            this.btnPattern.Name = "btnPattern";
            this.btnPattern.Size = new System.Drawing.Size(100, 46);
            this.btnPattern.TabIndex = 27;
            this.btnPattern.Text = "PATTERN";
            this.btnPattern.UseVisualStyleBackColor = true;
            this.btnPattern.Click += new System.EventHandler(this.btnPattern_Click);
            // 
            // labelPattern
            // 
            this.labelPattern.AllowDrop = true;
            this.labelPattern.BackColor = System.Drawing.SystemColors.Info;
            this.labelPattern.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPattern.ForeColor = System.Drawing.Color.Blue;
            this.labelPattern.Location = new System.Drawing.Point(155, 329);
            this.labelPattern.Name = "labelPattern";
            this.labelPattern.Size = new System.Drawing.Size(197, 40);
            this.labelPattern.TabIndex = 31;
            this.labelPattern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMapping
            // 
            this.btnMapping.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMapping.Location = new System.Drawing.Point(40, 456);
            this.btnMapping.Name = "btnMapping";
            this.btnMapping.Size = new System.Drawing.Size(100, 46);
            this.btnMapping.TabIndex = 29;
            this.btnMapping.Text = "MAPPING";
            this.btnMapping.UseVisualStyleBackColor = true;
            this.btnMapping.Click += new System.EventHandler(this.btnMapping_Click);
            // 
            // labelMapping
            // 
            this.labelMapping.AllowDrop = true;
            this.labelMapping.BackColor = System.Drawing.SystemColors.Info;
            this.labelMapping.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapping.ForeColor = System.Drawing.Color.Blue;
            this.labelMapping.Location = new System.Drawing.Point(155, 460);
            this.labelMapping.Name = "labelMapping";
            this.labelMapping.Size = new System.Drawing.Size(197, 40);
            this.labelMapping.TabIndex = 33;
            this.labelMapping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.SeaGreen;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXacNhan.Location = new System.Drawing.Point(471, 365);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(88, 45);
            this.btnXacNhan.TabIndex = 31;
            this.btnXacNhan.Text = "SAVE";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Goldenrod;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(578, 365);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 45);
            this.btnReset.TabIndex = 32;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // labelSoCellDaLuu
            // 
            this.labelSoCellDaLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSoCellDaLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSoCellDaLuu.ForeColor = System.Drawing.Color.Blue;
            this.labelSoCellDaLuu.Location = new System.Drawing.Point(518, 436);
            this.labelSoCellDaLuu.Name = "labelSoCellDaLuu";
            this.labelSoCellDaLuu.Size = new System.Drawing.Size(100, 20);
            this.labelSoCellDaLuu.TabIndex = 4;
            this.labelSoCellDaLuu.Text = "Số cell đã lưu:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCount.Location = new System.Drawing.Point(608, 436);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(15, 17);
            this.labelCount.TabIndex = 3;
            this.labelCount.Text = "0";
            // 
            // labelDaLuuVao
            // 
            this.labelDaLuuVao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDaLuuVao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDaLuuVao.ForeColor = System.Drawing.Color.Blue;
            this.labelDaLuuVao.Location = new System.Drawing.Point(31, 546);
            this.labelDaLuuVao.Name = "labelDaLuuVao";
            this.labelDaLuuVao.Size = new System.Drawing.Size(448, 20);
            this.labelDaLuuVao.TabIndex = 2;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(31, 521);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 15);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Trạng thái";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Red;
            this.labelTime.Location = new System.Drawing.Point(545, 480);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 17);
            this.labelTime.TabIndex = 1;
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // btnDK
            // 
            this.btnDK.BackColor = System.Drawing.Color.DimGray;
            this.btnDK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDK.ForeColor = System.Drawing.Color.White;
            this.btnDK.Location = new System.Drawing.Point(247, 392);
            this.btnDK.Name = "btnDK";
            this.btnDK.Size = new System.Drawing.Size(50, 45);
            this.btnDK.TabIndex = 30;
            this.btnDK.Text = "DK";
            this.btnDK.UseVisualStyleBackColor = false;
            this.btnDK.Click += new System.EventHandler(this.btnDK_Click);
            // 
            // btnBR
            // 
            this.btnBR.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBR.Location = new System.Drawing.Point(303, 392);
            this.btnBR.Name = "btnBR";
            this.btnBR.Size = new System.Drawing.Size(50, 45);
            this.btnBR.TabIndex = 31;
            this.btnBR.Text = "BR";
            this.btnBR.UseVisualStyleBackColor = true;
            this.btnBR.Click += new System.EventHandler(this.btnBR_Click);
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthor.ForeColor = System.Drawing.Color.Silver;
            this.labelAuthor.Location = new System.Drawing.Point(593, 552);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(103, 15);
            this.labelAuthor.TabIndex = 45;
            this.labelAuthor.Text = "©Nông Văn Phấn";
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.ForeColor = System.Drawing.Color.Red;
            this.labelDate.Location = new System.Drawing.Point(536, 505);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(0, 17);
            this.labelDate.TabIndex = 46;
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 571);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelDaLuuVao);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelSoCellDaLuu);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.labelMapping);
            this.Controls.Add(this.btnMapping);
            this.Controls.Add(this.labelPattern);
            this.Controls.Add(this.btnPattern);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.btnLevel);
            this.Controls.Add(this.btnDK);
            this.Controls.Add(this.btnBR);
            this.Controls.Add(this.labelTenLoi);
            this.Controls.Add(this.btnTenLoi);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDot);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.txtY3);
            this.Controls.Add(this.labelY3);
            this.Controls.Add(this.txtX3);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtY2);
            this.Controls.Add(this.labelY2);
            this.Controls.Add(this.txtX2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.labelY1);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtAPN);
            this.Controls.Add(this.labelAPN);
            this.Controls.Add(this.labelAuthor);
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