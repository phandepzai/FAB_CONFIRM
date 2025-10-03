using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#region HIỂN THỊ FORM MAPPING
namespace FAB_CONFIRM
{
    public partial class MappingForm : Form
    {
        // Property để lưu mapping được chọn (định dạng: Tên ô^Tọa độ X,Tọa độ Y)
        public string SelectedMapping { get; private set; }
        private readonly string[,] gridLabels = new string[4, 4]
        {
            { "A1", "A2", "B1", "B2" },
            { "A3", "A4", "B3", "B4" },
            { "C1", "C2", "D1", "D2" },
            { "C3", "C4", "D3", "D4" }
        };
        private readonly Image iphoneImage;
        private Point lastClickPosition = Point.Empty; // Lưu tọa độ click cuối cùng

        public MappingForm()
        {
            InitializeComponent();
            pictureBoxScreen.MouseClick += PictureBoxScreen_MouseClick;

            // Tải hình nền iPhone từ tài nguyên nhúng
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FAB_CONFIRM.iphone_screen.png"))
                {
                    if (stream != null)
                    {
                        iphoneImage = Image.FromStream(stream);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài nguyên hình ảnh 'iphone_screen.png'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        iphoneImage = null; // Nếu không tải được, không dùng hình nền
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hình nền iPhone: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                iphoneImage = null; // Nếu có lỗi, không dùng hình nền
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                iphoneImage?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PictureBoxScreen_MouseClick(object sender, MouseEventArgs e)
        {
            // Kích thước màn hình iPhone: 72mm x 154mm
            const float screenWidthMm = 72.86f;  // Chiều rộng (Y: trái sang phải)
            const float screenHeightMm = 158.20f; // Chiều cao (X: dưới lên trên)

            // Kích thước PictureBox: 227x480 pixels
            int cellWidth = pictureBoxScreen.Width / 4;  // 227 / 4 ≈ 56 pixels
            int cellHeight = pictureBoxScreen.Height / 4; // 480 / 4 = 120 pixels

            // Xác định chỉ số cột và hàng dựa trên tọa độ click
            int col = e.X / cellWidth;  // Cột: 0, 1, 2, 3 (liên quan đến Y)
            int row = e.Y / cellHeight; // Hàng: 0, 1, 2, 3 (liên quan đến X)

            // Đảm bảo chỉ số nằm trong giới hạn
            if (col >= 0 && col < 4 && row >= 0 && row < 4)
            {
                // Lưu tọa độ click (pixel) để vẽ dấu chấm
                lastClickPosition = e.Location;

                // Tính tọa độ (X, Y) trong milimet với gốc ở góc trái bên dưới
                float pixelsPerMmX = pictureBoxScreen.Height / screenHeightMm; // 480 / 154 ≈ 3.158 pixels/mm (cho X: dưới lên)
                float pixelsPerMmY = pictureBoxScreen.Width / screenWidthMm;   // 227 / 72 ≈ 3.154 pixels/mm (cho Y: trái sang)
                float xMm = (pictureBoxScreen.Height - e.Y) / pixelsPerMmX;   // X: 0-154mm (dưới lên trên)
                float yMm = e.X / pixelsPerMmY;                              // Y: 0-72mm (trái sang phải)

                // Làm tròn tọa độ đến số nguyên
                int xRounded = (int)Math.Round(xMm);
                int yRounded = (int)Math.Round(yMm);

                // Lấy tên ô
                string cellLabel = gridLabels[row, col];

                // Tạo chuỗi định dạng: Tên ô^Tọa độ X,Tọa độ Y
                SelectedMapping = $"{cellLabel}^{xRounded},{yRounded}";

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void PictureBoxScreen_MouseDown(object sender, MouseEventArgs e)
        {
            // Đóng form khi click chuột phải
            if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void PictureBoxScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ hình nền iPhone nếu có
            if (iphoneImage != null)
            {
                g.DrawImage(iphoneImage, 0, 0, pictureBoxScreen.Width, pictureBoxScreen.Height);
            }
            else
            {
                // Nếu không có hình nền, vẽ nền trắng
                g.FillRectangle(Brushes.White, 0, 0, pictureBoxScreen.Width, pictureBoxScreen.Height);
            }
            // Vẽ dấu chấm tại vị trí click cuối cùng
            if (!lastClickPosition.IsEmpty)
            {
                using (Brush dotBrush = new SolidBrush(Color.Red))
                {
                    int dotSize = 10; // Kích thước dấu chấm
                    g.FillEllipse(dotBrush, lastClickPosition.X - dotSize / 2, lastClickPosition.Y - dotSize / 2, dotSize, dotSize);
                }
            }
            // Vẽ lưới 4x4
            int cellWidth = pictureBoxScreen.Width / 4; // ≈ 56 pixels
            int cellHeight = pictureBoxScreen.Height / 4; // 120 pixels

            using (Pen pen = new Pen(Color.Black, 2))
            {
                // Vẽ đường ngang
                for (int i = 1; i < 4; i++)
                {
                    g.DrawLine(pen, 0, i * cellHeight, pictureBoxScreen.Width, i * cellHeight);
                }
                // Vẽ đường dọc
                for (int i = 1; i < 4; i++)
                {
                    g.DrawLine(pen, i * cellWidth, 0, i * cellWidth, pictureBoxScreen.Height);
                }
            }

            // Vẽ nhãn cho các ô (A1, A2, ..., D4)
            using (Font font = new Font("Segoe UI", 12, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        string label = gridLabels[row, col];
                        SizeF textSize = g.MeasureString(label, font);
                        float x = col * cellWidth + (cellWidth - textSize.Width) / 2;
                        float y = row * cellHeight + (cellHeight - textSize.Height) / 2;
                        g.DrawString(label, font, brush, x, y);
                    }
                }
            }
        }
    }
}
#endregion