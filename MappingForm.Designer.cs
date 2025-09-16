namespace FAB_CONFIRM
{
    partial class MappingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxScreen;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingForm));
            this.pictureBoxScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxScreen
            // 
            this.pictureBoxScreen.BackColor = System.Drawing.Color.White;
            this.pictureBoxScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxScreen.BackgroundImage")));
            this.pictureBoxScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxScreen.Location = new System.Drawing.Point(7, 1);
            this.pictureBoxScreen.Name = "pictureBoxScreen";
            this.pictureBoxScreen.Size = new System.Drawing.Size(272, 478);
            this.pictureBoxScreen.TabIndex = 1;
            this.pictureBoxScreen.TabStop = false;
            this.pictureBoxScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxScreen_Paint);
            this.pictureBoxScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxScreen_MouseClick);
            this.pictureBoxScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxScreen_MouseDown);
            // 
            // MappingSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(285, 482);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxScreen);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MappingSelectionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MAPPING LỖI";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).EndInit();
            this.ResumeLayout(false);

        }
    }
}