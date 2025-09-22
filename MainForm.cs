using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    
    public partial class MainForm : Form
    {
        #region KHAI BÁO BIẾN CÁC THUỘC TÍNH
        private TextBox activeTextBox;
        private int savedCellCount = 0;
        private string filePath;
        private readonly System.Windows.Forms.Timer timer;
        private readonly ToolTip toolTip;

        private readonly Timer rainbowTimer;// Các biến cho hiệu ứng chuyển màu cầu vồng mượt mà
        private bool isRainbowActive = false;
        private Color originalAuthorColor;
        private double rainbowPhase = 0;

        // Đọc danh sách từ các file .ini
        private readonly List<string> patternList;
        private readonly List<string> defectList;
        private readonly ConfigManager patternConfigManager;
        private readonly ConfigManager defectConfigManager;
        #endregion

        #region KHỞI TẠO GIAO DIỆN VÀ CHỨC NĂNG
        public MainForm()
        {
            InitializeComponent();
            string eqpid = ReadEQPIDFromIniFile();
            this.Text = "FAB CONFIRM" + (string.IsNullOrEmpty(eqpid) ? "" : "_" + eqpid + "");
            Button[] numericButtons = new Button[] {
                Btn0, Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8, Btn9, BtnDot
            };

            foreach (Button b in numericButtons)
            {
                if (b != null)
                {
                    b.UseVisualStyleBackColor = false; // Bắt buộc tắt để BackColor hoạt động
                    b.BackColor = Color.White;         // Đặt nền trắng
                }
            }

            LabelTenLoi.TextChanged += (s, e) => AdjustLabelFont(LabelTenLoi);
            LabelLevel.TextChanged += (s, e) => AdjustLabelFont(LabelLevel);
            LabelPattern.TextChanged += (s, e) => AdjustLabelFont(LabelPattern);
            LabelMapping.TextChanged += (s, e) => AdjustLabelFont(LabelMapping);

            // Gán sự kiện Enter cho tất cả các TextBox nhập liệu để bàn phím số hoạt động
            TxtAPN.Enter += OnTextBoxEnter;
            TxtX1.Enter += OnTextBoxEnter;
            TxtY1.Enter += OnTextBoxEnter;
            TxtX2.Enter += OnTextBoxEnter;
            TxtY2.Enter += OnTextBoxEnter;
            TxtX3.Enter += OnTextBoxEnter;
            TxtY3.Enter += OnTextBoxEnter;

            //Cảnh báo tooltip khi nhập từ bàn phím vật lý trên 3 số
            TxtX1.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);
            TxtY1.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);
            TxtX2.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);
            TxtY2.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);
            TxtX3.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);
            TxtY3.KeyPress += new KeyPressEventHandler(CoordinateTextBox_KeyPress);

            // Gán sự kiện KeyDown để chỉ cho phép nhập số cho các ô tọa độ
            TxtX1.KeyDown += TxtCoord_KeyDown;
            TxtY1.KeyDown += TxtCoord_KeyDown;
            TxtX2.KeyDown += TxtCoord_KeyDown;
            TxtY2.KeyDown += TxtCoord_KeyDown;
            TxtX3.KeyDown += TxtCoord_KeyDown;
            TxtY3.KeyDown += TxtCoord_KeyDown;

            // Gán giới hạn 3 ký tự cho các ô tọa độ
            TxtX1.MaxLength = 3;
            TxtY1.MaxLength = 3;
            TxtX2.MaxLength = 3;
            TxtY2.MaxLength = 3;
            TxtX3.MaxLength = 3;
            TxtY3.MaxLength = 3;

            // Gán sự kiện GotFocus và LostFocus cho các TextBox
            TxtAPN.GotFocus += TextBox_GotFocus;
            TxtAPN.LostFocus += TextBox_LostFocus;
            TxtX1.GotFocus += TextBox_GotFocus;
            TxtX1.LostFocus += TextBox_LostFocus;
            TxtY1.GotFocus += TextBox_GotFocus;
            TxtY1.LostFocus += TextBox_LostFocus;
            TxtX2.GotFocus += TextBox_GotFocus;
            TxtX2.LostFocus += TextBox_LostFocus;
            TxtY2.GotFocus += TextBox_GotFocus;
            TxtY2.LostFocus += TextBox_LostFocus;
            TxtX3.GotFocus += TextBox_GotFocus;
            TxtX3.LostFocus += TextBox_LostFocus;
            TxtY3.GotFocus += TextBox_GotFocus;
            TxtY3.LostFocus += TextBox_LostFocus;

            // Khởi tạo timer cho hiệu ứng cầu vồng
            this.rainbowTimer = new Timer
            {
                Interval = 20 // Cập nhật màu mỗi 20ms để mượt hơn
            };
            this.rainbowTimer.Tick += new EventHandler(this.RainbowTimer_Tick);

            // Gắn sự kiện cho lblCopyright
            LabelAuthor.MouseEnter += LabelAuthor_MouseEnter;
            LabelAuthor.MouseLeave += LabelAuthor_MouseLeave;

            // Khởi tạo và cấu hình Timer cho đồng hồ
            timer = new System.Windows.Forms.Timer
            {
                Interval = 500 // Cập nhật mỗi giây (500ms)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            // Tạo đường dẫn file dựa trên ngày hiện tại
            SetFilePath();

            // Thêm giới hạn 300 ký tự cho TxtAPN
            TxtAPN.MaxLength = 300;

            //gán sự kiện Click cho LabelStatus để mở thư mục chứa file khi click
            LabelStatus.Click += LabelStatus_Click;
            LabelSoCellDaLuu.Click += LabelSoCellDaLuu_Click;

            // Tải số APN duy nhất đã lưu khi khởi động
            LoadSavedCount();

            // Thêm sự kiện KeyDown cho TxtAPN để xử lý phím Enter
            TxtAPN.KeyDown += TxtAPN_KeyDown;

            // Khởi tạo ToolTip
            toolTip = new ToolTip
            {
                AutoPopDelay = 5000, // Hiển thị tooltip trong 5 giây
                InitialDelay = 100,   // Thời gian chờ trước khi hiển thị (100ms)
                ReshowDelay = 100,    // Thời gian chờ khi hiển thị lại
                ShowAlways = true    // Hiển thị ngay cả khi mất focus
            };

            toolTip.SetToolTip(LabelSoCellDaLuu, "BẤM VÀO ĐỂ MỞ THƯ MỤC LƯU FILE");

            // Cập nhật trạng thái ban đầu
            UpdateStatus("Sẵn sàng nhập liệu...", System.Drawing.Color.Green);

            // Khai báo các danh sách mặc định
            string defaultPatterns = "PATTERN-001,PATTERN-002,PATTERN-003,PATTERN-004,PATTERN-005,PATTERN-006,PATTERN-007,PATTERN-008,PATTERN-009,PATTERN-010,PATTERN-011,PATTERN-012,PATTERN-013,PATTERN-014,PATTERN-015,PATTERN-016,PATTERN-017,PATTERN-018,PATTERN-019,PATTERN-020,PATTERN-021,PATTERN-022,PATTERN-023,PATTERN-024,PATTERN-025,PATTERN-026,PATTERN-027";
            string defaultDefects = "Lỗi phần mềm hệ thống,Lỗi phần mềm ứng dụng,Lỗi phần mềm bảo mật,Lỗi phần mềm giao diện,Lỗi phần mềm dữ liệu,Lỗi phần mềm kết nối,Lỗi phần mềm hiệu suất,Lỗi phần mềm tương thích,Lỗi phần mềm cập nhật,Lỗi phần mềm bảo mật,Lỗi phần mềm nền tảng,Lỗi phần mềm di động,Lỗi phần mềm web,Lỗi phần mềm cloud,Lỗi phần mềm AI,Lỗi phần mềm IoT,Lỗi phần mềm big data,Lỗi phần mềm học máy,Lỗi phần mềm mạng,Lỗi phần mềm game,Lỗi phần mềm tài chính,Lỗi phần mềm y tế,Lỗi phần mềm giáo dục,Lỗi phần mềm thương mại,Lỗi phần mềm quản lý,Lỗi phần mềm nông nghiệp,Lỗi phần mềm giao thông,Lỗi phần mềm năng lượng,Lỗi phần mềm môi trường,Lỗi phần mềm xã hội,Lỗi phần mềm chính phủ,Lỗi phần mềm quân sự,Lỗi phần mềm không gian,Lỗi phần mềm robot,Lỗi phần mềm tự động hóa,Lỗi phần mềm kiểm soát,Lỗi phần mềm mô phỏng,Lỗi phần mềm phân tích,Lỗi phần mềm tối ưu hóa,Lỗi phần mềm dự báo,Lỗi phần mềm học sâu,Lỗi phần cứng bộ nhớ,Lỗi phần cứng CPU,Lỗi phần cứng GPU,Lỗi phần cứng đầu vào,Lỗi phần cứng đầu ra,Lỗi phần cứng xử lý,Lỗi phần cứng nguồn,Lỗi phần cứng nhiệt,Lỗi phần cứng tổng hợp";

            // Khởi tạo ConfigManager cho mỗi file
            patternConfigManager = new ConfigManager("C:\\FAB_CONFIRM\\Config\\", "PATTERN.ini");
            defectConfigManager = new ConfigManager("C:\\FAB_CONFIRM\\Config\\", "DEFECT.ini");

            // Khởi tạo file nếu chưa tồn tại
            patternConfigManager.InitializeConfigFile(defaultPatterns, "PatternNames");
            defectConfigManager.InitializeConfigFile(defaultDefects, "DefectNames");

            // Đọc danh sách từ các file
            patternList = patternConfigManager.ReadList("PatternNames");
            defectList = defectConfigManager.ReadList("DefectNames");

        }
        #endregion

        #region HIỆU ỨNG PHÍM VÀ TỰ ĐIỀU CHỈNH FONT
        private void AdjustLabelFont(Label lbl)
        {
            if (string.IsNullOrEmpty(lbl.Text)) return;

            float fontSize = lbl.Font.Size;
            Size proposedSize;

            using (Graphics g = lbl.CreateGraphics())
            {
                proposedSize = g.MeasureString(lbl.Text, lbl.Font).ToSize();

                while ((proposedSize.Width > lbl.Width || proposedSize.Height > lbl.Height) && fontSize > 6f)
                {
                    fontSize -= 0.5f;
                    Font newFont = new Font(lbl.Font.FontFamily, fontSize, lbl.Font.Style);
                    proposedSize = g.MeasureString(lbl.Text, newFont).ToSize();

                    // Giải phóng font cũ của Label trước khi gán font mới để tránh leak
                    lbl.Font?.Dispose();
                    lbl.Font = newFont;
                }
            }
        }

        // Hiệu ứng nhấn nút
        private async void ApplyButtonClickEffect(Button button)
        {
            if (button != null)
            {
                button.BackColor = Color.LightGreen;
                await Task.Delay(150); // Chờ 150ms để hiệu ứng hiển thị
                button.BackColor = Color.White;
            }
        }

        // Sự kiện thay đổi màu nền khi TextBox được focus
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = ColorTranslator.FromHtml("#E6F1D8"); // Màu xanh lá mạ nhạt khi focus
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = SystemColors.Window; // Trở về màu trắng mặc định khi mất focus
        }
        #endregion

        #region ĐỒNG HỒ CHẠY THEO GMT +7
        // Sự kiện cho đồng hồ
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Lấy múi giờ GMT+7
                TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                // Lấy thời gian hiện tại theo UTC (GMT+0)
                DateTime utcTime = DateTime.UtcNow;
                // Chuyển đổi thời gian UTC sang thời gian Việt Nam (GMT+7)
                DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, vietnamZone);

                // Hiển thị thời gian đã chuyển đổi
                LabelTime.Text = vietnamTime.ToString("HH:mm:ss");
                LabelDate.Text = vietnamTime.ToString("dd/MM/yyyy");
            }
            catch (TimeZoneNotFoundException ex)
            {
                // Xử lý ngoại lệ nếu không tìm thấy múi giờ "SE Asia Standard Time"
                UpdateStatus($"Lỗi múi giờ: {ex.Message}", Color.Red);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                UpdateStatus($"Lỗi cập nhật đồng hồ: {ex.Message}", Color.Red);
            }
        }

        #endregion

        #region THAY ĐỔI MÀU SẮC KHI DI CHUỘT VÀO TÊN
        // Sự kiện Tick của timer, cập nhật màu sắc
        private void RainbowTimer_Tick(object sender, EventArgs e)
        {
            rainbowPhase += 0.05; // Giảm tốc độ thay đổi để màu chuyển từ từ hơn

            Color newColor = CalculateRainbowColor(rainbowPhase);
            LabelAuthor.ForeColor = newColor;
        }

        // Tính toán màu sắc cầu vồng dựa trên giai đoạn
        private Color CalculateRainbowColor(double phase)
        {
            double red = Math.Sin(phase) * 127 + 128;
            double green = Math.Sin(phase + 2 * Math.PI / 3) * 127 + 128;
            double blue = Math.Sin(phase + 4 * Math.PI / 3) * 127 + 128;

            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            return Color.FromArgb((int)red, (int)green, (int)blue);
        }
        private void LabelAuthor_MouseEnter (object sender, EventArgs e)
        {
            if (!isRainbowActive)
            {
                isRainbowActive = true;
                originalAuthorColor = LabelAuthor.ForeColor;
                rainbowTimer.Start();
            }
        }

        // Khi chuột rời nhãn, tắt hiệu ứng và khôi phục màu gốc
        private void LabelAuthor_MouseLeave(object sender, EventArgs e)
        {
            if (isRainbowActive)
            {
                isRainbowActive = false;
                rainbowTimer.Stop();
                LabelAuthor.ForeColor = originalAuthorColor;
            }
        }
        #endregion

        #region XỬ LÝ HOẠT ĐỘNG CÁC BUTTON
        private void OnTextBoxEnter(object sender, EventArgs e)
        {
            activeTextBox = sender as TextBox;
        }

        // Sự kiện xử lý phím Enter trên TxtAPN
        private void TxtAPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtX1.Focus();
                e.SuppressKeyPress = true; // Ngăn không cho tiếng "ding" xuất hiện
            }
        }

        // Sự kiện xử lý phím cho các ô tọa độ, chỉ cho phép nhập số
        private void TxtCoord_KeyDown(object sender, KeyEventArgs e)
        {
            bool isNumber = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);
            bool isBackspace = e.KeyCode == Keys.Back;
            bool isDot = e.KeyCode == Keys.OemPeriod;
            bool isNavigationKey = e.KeyCode == Keys.Left || e.KeyCode == Keys.Right;

            if (isDot)
            {
                if (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("."))
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    toolTip.Show("Chỉ cho phép nhập dấu chấm ở đầu và không nhập nhiều dấu chấm!", (TextBox)sender, 0, ((TextBox)sender).Height, 3500); // Hiển thị thông báo
                }
            }
            else if (!isNumber && !isBackspace && !isNavigationKey)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                toolTip.Show("Chỉ cho phép nhập số!", (TextBox)sender, 0, ((TextBox)sender).Height, 3500); // Hiển thị thông báo
            }
        }

        private void CoordinateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            // Kiểm tra xem phím được gõ có phải là số hoặc dấu chấm không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Ngăn không cho ký tự không hợp lệ được nhập
                return;
            }

            // Kiểm tra nếu nhập thêm ký tự sẽ vượt quá MaxLength
            if (currentTextBox.Text.Length >= currentTextBox.MaxLength && !char.IsControl(e.KeyChar))
            {
                toolTip.Show("Chỉ cho phép nhập tối đa 3 số!", currentTextBox, 0, currentTextBox.Height, 3500);
                e.Handled = true; // Ngăn không cho ký tự được nhập
            }
        }

        private async void ApplyButtonClickEffectWithOriginalColor(Button button, Color originalColor)
        {
            if (button != null)
            {
                button.BackColor = Color.LightGreen;
                await Task.Delay(150); // Chờ 150ms để hiệu ứng hiển thị
                button.BackColor = originalColor;
            }
        }

        //Phương thức xử lý dữ liệu bàn phím số
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button);
            if (activeTextBox != null)
            {
                Button Btn = sender as Button;
                bool isCoordTextBox = activeTextBox.Name.StartsWith("TxtX") || activeTextBox.Name.StartsWith("TxtY");

                if (isCoordTextBox)
                {
                    string newText = activeTextBox.Text + Btn.Text;
                    if (newText.Length > activeTextBox.MaxLength)
                    {
                        // Hiển thị tooltip cảnh báo khi nhập quá số ký tự tối đa
                        toolTip.Show("Chỉ cho phép nhập tối đa 3 số!", activeTextBox, 0, activeTextBox.Height, 3500);
                        return; // Ngăn không cho nhập thêm
                    }

                    // Ngăn không cho nhập dấu chấm ở bất kỳ vị trí nào trừ vị trí đầu tiên
                    if (Btn.Text == "." && activeTextBox.Text.Length > 0)
                    {
                        return;
                    }

                    // Ngăn không cho nhập '0' ở vị trí đầu tiên
                    if (Btn.Text != "." && activeTextBox.Text.Length == 0 && Btn.Text == "0")
                    {
                        return;
                    }
                }

                activeTextBox.Text += Btn.Text;
                activeTextBox.SelectionStart = activeTextBox.Text.Length;
                activeTextBox.Focus();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            if (activeTextBox != null && activeTextBox.Text.Length > 0)
            {
                activeTextBox.Text = activeTextBox.Text.Remove(activeTextBox.Text.Length - 1);
                activeTextBox.SelectionStart = activeTextBox.Text.Length;               
                activeTextBox.Focus();
            }
        }

        private void BtnPattern_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;

            // Tạo một instance của PatternForm và truyền danh sách pattern đã đọc từ file
            using (var patternForm = new PatternForm(patternList))
            {
                if (patternForm.ShowDialog() == DialogResult.OK)
                {
                    LabelPattern.Text = patternForm.SelectedPattern;
                    UpdateStatus("Đã chọn Pattern.", System.Drawing.Color.Blue);
                }
            }
        }

        private void BtnTenLoi_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;

            // Tạo một instance của DefectForm và truyền danh sách defect đã đọc từ file
            using (var defectForm = new DefectForm(defectList))
            {
                if (defectForm.ShowDialog() == DialogResult.OK)
                {
                    LabelTenLoi.Text = defectForm.SelectedDefect;
                    UpdateStatus("Đã chọn tên lỗi.", System.Drawing.Color.Blue);
                }
            }
        }

        private void BtnLevel_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var levelForm = new LevelForm())
            {
                if (levelForm.ShowDialog() == DialogResult.OK)
                {
                    LabelLevel.Text = levelForm.SelectedLevel;
                    UpdateStatus("Đã chọn Level.", System.Drawing.Color.Blue);
                }
            }
        }
        private void BtnDK_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffectWithOriginalColor(BtnDK, BtnDK.BackColor);
            if (!string.IsNullOrEmpty(LabelLevel.Text))
            {
                if (LabelLevel.Text.EndsWith("^DK"))
                {
                    LabelLevel.Text = LabelLevel.Text.Replace("^DK", ""); // Hủy nếu đã có ^DK
                    UpdateStatus("Đã hủy phân loại Level tối (DK).", System.Drawing.Color.Blue);
                }
                else if (!LabelLevel.Text.Contains("^"))
                {
                    LabelLevel.Text += "^DK"; // Ghép ^DK nếu chưa có hậu tố
                    UpdateStatus("Đã chọn Level tối (DK).", System.Drawing.Color.Blue);
                }
                // Nếu đã có ^BR, thay bằng ^DK
                else if (LabelLevel.Text.EndsWith("^BR"))
                {
                    LabelLevel.Text = LabelLevel.Text.Replace("^BR", "^DK");
                    UpdateStatus("Đã thay bằng Level tối (DK).", System.Drawing.Color.Blue);
                }
            }
        }

        private void BtnBR_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffectWithOriginalColor(BtnBR, BtnBR.BackColor);
            if (!string.IsNullOrEmpty(LabelLevel.Text))
            {
                if (LabelLevel.Text.EndsWith("^BR"))
                {
                    LabelLevel.Text = LabelLevel.Text.Replace("^BR", ""); // Hủy nếu đã có ^BR
                    UpdateStatus("Đã hủy phân loại Level sáng (BR).", System.Drawing.Color.Blue);
                }
                else if (!LabelLevel.Text.Contains("^"))
                {
                    LabelLevel.Text += "^BR"; // Ghép ^BR nếu chưa có hậu tố
                    UpdateStatus("Đã chọn Level sáng (BR).", System.Drawing.Color.Blue);
                }
                // Nếu đã có ^DK, thay bằng ^BR
                else if (LabelLevel.Text.EndsWith("^DK"))
                {
                    LabelLevel.Text = LabelLevel.Text.Replace("^DK", "^BR");
                    UpdateStatus("Đã thay bằng Level sáng (BR).", System.Drawing.Color.Blue);
                }
            }
        }

        private void BtnMapping_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var mappingForm = new MappingForm())
            {
                if (mappingForm.ShowDialog() == DialogResult.OK)
                {
                    LabelMapping.Text = mappingForm.SelectedMapping;
                    UpdateStatus("Đã chọn Mapping.", System.Drawing.Color.Blue);
                }
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            // Cập nhật lại filePath trước khi lưu để đảm bảo đúng ca
            SetFilePath();
            //Hiệu ứng nút nhấn
            ApplyButtonClickEffectWithOriginalColor(BtnXacNhan, BtnXacNhan.BackColor);
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(TxtAPN.Text))
            {
                toolTip.Show("Vui lòng nhập APN", TxtAPN, 0, TxtAPN.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtX1.Text))
            {
                toolTip.Show("Vui lòng nhập tọa độ", TxtX1, 0, TxtX1.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtY1.Text))
            {
                toolTip.Show("Vui lòng nhập tọa độ", TxtY1, 0, TxtY1.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            //Kiểm tra tên lỗi
            //if (string.IsNullOrWhiteSpace(LabelTenLoi.Text))
            //{
            //    toolTip.Show("Vui lòng chọn TÊN LỖI", LabelTenLoi, 0, LabelTenLoi.Height, 3500); // Hiển thị tooltip 3 giây
            //    isValidationFailed = true;
            //    return;
            //}

            string apn = TxtAPN.Text;
            string x1 = TxtX1.Text;
            string y1 = TxtY1.Text;
            string x2 = TxtX2.Text;
            string y2 = TxtY2.Text;
            string x3 = TxtX3.Text;
            string y3 = TxtY3.Text;
            string tenLoi = LabelTenLoi.Text; // Thay TxtTenLoi bằng LabelTenLoi
            string level = LabelLevel.Text; // Thay LabelLevel bằng LabelLevel
            string pattern = LabelPattern.Text; // Thay TxtPattern bằng LabelPattern
            string mapping = LabelMapping.Text; // Thay TxtMapping bằng LabelMapping
                                                //Kiểm tra các phần xem đã nhập dữ liệu chưa mới lưu
            if (string.IsNullOrEmpty(apn) || string.IsNullOrEmpty(x1) || string.IsNullOrEmpty(y1)) //|| string.IsNullOrEmpty(tenLoi) || string.IsNullOrEmpty(level) || string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(mapping))
            {
                UpdateStatus("Vui lòng nhập đầy đủ dữ liệu bắt buộc!", System.Drawing.Color.Red);
                return;
            }

            try
            {
                // Đảm bảo thư mục tồn tại
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Kiểm tra quyền ghi
                if (!IsDirectoryWritable(directoryPath))
                {
                    UpdateStatus($"Không có quyền ghi vào thư mục: {directoryPath}", System.Drawing.Color.Red);
                    return;
                }
                // Bao bọc pattern và mapping bằng ngoặc kép nếu chứa dấu phẩy
                string formattedPattern = pattern.Contains(",") ? $"\"{pattern}\"" : pattern;
                string escapedMapping = mapping.Contains(",") ? $"\"{mapping}\"" : mapping;

                // Nhóm cặp tọa độ thành x,y và bao bọc bằng ngoặc kép
                string coordX1Y1 = $"\"{x1},{y1}\"";
                string coordX2Y2 = string.IsNullOrEmpty(x2) && string.IsNullOrEmpty(y2) ? "" : $"\"{x2},{y2}\"";
                string coordX3Y3 = string.IsNullOrEmpty(x3) && string.IsNullOrEmpty(y3) ? "" : $"\"{x3},{y3}\"";

                // Tạo dòng dữ liệu với phân cách bằng dấu phẩy
                string dataLine = $"{apn},{coordX1Y1},{coordX2Y2},{coordX3Y3},{tenLoi},{formattedPattern},{level},{escapedMapping},{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";

                // Kiểm tra nếu file chưa tồn tại thì ghi thêm dòng tiêu đề
                if (!File.Exists(filePath))
                {
                    string headerLine = "APN,\"X1,Y1\",\"X2,Y2\",\"X3,Y3\",TEN_LOI,PATTERN,LEVEL,MAPPING,THOI_GIAN\n";
                    File.AppendAllText(filePath, headerLine, Encoding.UTF8);
                }

                // Lưu dữ liệu vào file với định dạng UTF-8
                File.AppendAllText(filePath, dataLine, Encoding.UTF8);

                savedCellCount++;
                LabelCount.Text = savedCellCount.ToString();
                LabelTime.Text = DateTime.Now.ToString("HH:mm:ss\ndd/MM/yyyy");
                UpdateStatus($"Lưu thành công !\nDữ liệu đã được ghi lại: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nVị trí lưu: {filePath}", System.Drawing.Color.ForestGreen);

                // Xóa dữ liệu các trường sau khi lưu thành công
                TxtAPN.Text = "";
                TxtX1.Text = "";
                TxtY1.Text = "";
                TxtX2.Text = "";
                TxtY2.Text = "";
                TxtX3.Text = "";
                TxtY3.Text = "";
                LabelTenLoi.Text = ""; // Thay TxtTenLoi bằng LabelTenLoi
                LabelLevel.Text = ""; // Thay LabelLevel bằng LabelLevel
                LabelPattern.Text = ""; // Thay TxtPattern bằng LabelPattern
                LabelMapping.Text = ""; // Thay TxtMapping bằng LabelMapping
                TxtAPN.Focus();
                // Cập nhật lại số APN duy nhất sau khi lưu
                LoadSavedCount();
            }
            catch (UnauthorizedAccessException ex)
            {
                UpdateStatus($"Lỗi quyền truy cập: {ex.Message}", System.Drawing.Color.Red);
            }
            catch (DirectoryNotFoundException ex)
            {
                UpdateStatus($"Đường dẫn không hợp lệ: {ex.Message}", System.Drawing.Color.Red);
            }
            catch (IOException ex)
            {
                UpdateStatus($"Lỗi I/O khi lưu file: {ex.Message}", System.Drawing.Color.Red);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Lỗi không xác định: {ex.Message}", System.Drawing.Color.Red);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffectWithOriginalColor(BtnReset, BtnReset.BackColor);
            TxtAPN.Text = "";
            TxtX1.Text = "";
            TxtY1.Text = "";
            TxtX2.Text = "";
            TxtY2.Text = "";
            TxtX3.Text = "";
            TxtY3.Text = "";
            LabelTenLoi.Text = ""; // Thay TxtTenLoi bằng LabelTenLoi
            LabelLevel.Text = ""; // Thay LabelLevel bằng LabelLevel
            LabelPattern.Text = ""; // Thay TxtPattern bằng LabelPattern
            LabelMapping.Text = ""; // Thay TxtMapping bằng LabelMapping

            TxtAPN.Focus();
            UpdateStatus("Giao diện UI đã khởi tạo lại.", System.Drawing.Color.Teal);
        }
        #endregion

        #region CẬP NHẬT TRẠNG THÁI
        private void UpdateStatus(string message, System.Drawing.Color? color = null)
        {
            LabelStatus.Text = message;
            if (color.HasValue)
            {
                LabelStatus.ForeColor = color.Value;
            }
            else
            {
                LabelStatus.ForeColor = System.Drawing.Color.Black;
            }

            // Kích hoạt tooltip khi lưu thành công (màu xanh lá)
            if (color == System.Drawing.Color.Green)
            {
                toolTip.SetToolTip(LabelStatus, "BẤM VÀO ĐỂ MỞ THƯ MỤC");
            }
            else
            {
                toolTip.SetToolTip(LabelStatus, "");
            }
        }
        private void LoadSavedCount()
        {
            if (File.Exists(filePath))
            {
                HashSet<string> uniqueApns = new HashSet<string>();
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("APN")) // Bỏ qua dòng tiêu đề
                    {
                        string[] fields = line.Split(',');
                        if (fields.Length > 0)
                        {
                            uniqueApns.Add(fields[0]); // Thêm APN vào HashSet
                        }
                    }
                }
                savedCellCount = uniqueApns.Count;
                LabelCount.Text = savedCellCount.ToString();
            }
        }
        private void LabelStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", directoryPath);
                }
                else
                {
                    UpdateStatus("Thư mục không tồn tại!", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Lỗi khi mở thư mục: {ex.Message}", System.Drawing.Color.Red);
            }
        }
        private void LabelSoCellDaLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", directoryPath);
                }
                else
                {
                    UpdateStatus("Thư mục không tồn tại!", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Lỗi khi mở thư mục: {ex.Message}", System.Drawing.Color.Red);
            }
        }
        #endregion

        #region PHƯƠNG THỨC LƯU FILE
        //PHƯƠNG THỨC LƯU DỮ LIỆU VÀO FILE
        private void SetFilePath()
        {
            // Lấy múi giờ GMT+7
            TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);

            // Xác định ngày và ca làm việc
            string dateString;
            string shift;

            // Nếu thời gian từ 20:00 hôm nay đến trước 08:00 hôm sau, sử dụng ngày bắt đầu ca đêm
            if (vietnamTime.Hour >= 20 || vietnamTime.Hour < 8)
            {
                // Nếu thời gian từ 00:00 đến 07:59:59, sử dụng ngày hôm trước
                if (vietnamTime.Hour < 8)
                {
                    dateString = vietnamTime.AddDays(-1).ToString("yyyyMMdd");
                }
                else
                {
                    dateString = vietnamTime.ToString("yyyyMMdd");
                }
                shift = "NIGHT";
            }
            // Nếu thời gian từ 08:00 đến trước 20:00, sử dụng ngày hiện tại và ca ngày
            else
            {
                dateString = vietnamTime.ToString("yyyyMMdd");
                shift = "DAY";
            }

            string eqpid = ReadEQPIDFromIniFile(); // Lấy EQPID từ file ini
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directoryPath = Path.Combine(desktopPath, "FAB_CONFIRM");
            // Tạo tên file với EQPID (nếu có)
            string fileName = string.IsNullOrEmpty(eqpid) ? $"FAB_{dateString}_{shift}.csv" : $"FAB_{eqpid}_{dateString}_{shift}.csv";
            filePath = Path.Combine(directoryPath, fileName);
            // Đảm bảo thư mục tồn tại
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Lỗi tạo thư mục: {ex.Message}", System.Drawing.Color.Red);
            }
        }
        private bool IsDirectoryWritable(string directoryPath)
        {
            try
            {
                using (FileStream fs = File.Create(Path.Combine(directoryPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                { }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ĐỌC THÔNG TIN TỪ FILE INI
        /// <summary>
        /// Đọc giá trị EQPID từ file MachineParam.ini
        /// </summary>
        /// <returns>Giá trị EQPID hoặc chuỗi rỗng nếu không tìm thấy.</returns>
        private string ReadEQPIDFromIniFile()
        {
            string filePath = @"C:\samsung\Debug\Config\MachineParam.ini";
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("EQPID="))
                        {
                            return line.Substring("EQPID=".Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi đọc file, ví dụ: không có quyền truy cập
                    MessageBox.Show("Lỗi khi đọc file MachineParam.ini: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return "";
        }
        #endregion
    }
}