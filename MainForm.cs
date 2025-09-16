using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    
    public partial class MainForm : Form
    {
        #region KHAI BÁO BIẾN
        private TextBox activeTextBox;
        private int savedCellCount = 0;
        private string filePath;
        private System.Windows.Forms.Timer timer;
        private readonly ToolTip toolTip;

        private Timer rainbowTimer;// Các biến cho hiệu ứng chuyển màu cầu vồng mượt mà
        private bool isRainbowActive = false;
        private Color originalAuthorColor;
        private double rainbowPhase = 0;
        #endregion

        #region KHỞI TẠO FORM ỨNG DỤNG
        public MainForm()
        {
            InitializeComponent();

            Button[] numericButtons = new Button[] {
                btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnDot
            };

            foreach (Button b in numericButtons)
            {
                if (b != null)
                {
                    b.UseVisualStyleBackColor = false; // Bắt buộc tắt để BackColor hoạt động
                    b.BackColor = Color.White;         // Đặt nền trắng
                }
            }

            labelTenLoi.TextChanged += (s, e) => AdjustLabelFont(labelTenLoi);
            labelLevel.TextChanged += (s, e) => AdjustLabelFont(labelLevel);
            labelPattern.TextChanged += (s, e) => AdjustLabelFont(labelPattern);
            labelMapping.TextChanged += (s, e) => AdjustLabelFont(labelMapping);

            // Gán sự kiện Enter cho tất cả các TextBox nhập liệu để bàn phím số hoạt động
            txtAPN.Enter += OnTextBoxEnter;
            txtX1.Enter += OnTextBoxEnter;
            txtY1.Enter += OnTextBoxEnter;
            txtX2.Enter += OnTextBoxEnter;
            txtY2.Enter += OnTextBoxEnter;
            txtX3.Enter += OnTextBoxEnter;
            txtY3.Enter += OnTextBoxEnter;


            // Gán sự kiện KeyDown để chỉ cho phép nhập số cho các ô tọa độ
            txtX1.KeyDown += txtCoord_KeyDown;
            txtY1.KeyDown += txtCoord_KeyDown;
            txtX2.KeyDown += txtCoord_KeyDown;
            txtY2.KeyDown += txtCoord_KeyDown;
            txtX3.KeyDown += txtCoord_KeyDown;
            txtY3.KeyDown += txtCoord_KeyDown;

            // Gán giới hạn 3 ký tự cho các ô tọa độ
            txtX1.MaxLength = 3;
            txtY1.MaxLength = 3;
            txtX2.MaxLength = 3;
            txtY2.MaxLength = 3;
            txtX3.MaxLength = 3;
            txtY3.MaxLength = 3;

            // Gán sự kiện GotFocus và LostFocus cho các TextBox
            txtAPN.GotFocus += TextBox_GotFocus;
            txtAPN.LostFocus += TextBox_LostFocus;
            txtX1.GotFocus += TextBox_GotFocus;
            txtX1.LostFocus += TextBox_LostFocus;
            txtY1.GotFocus += TextBox_GotFocus;
            txtY1.LostFocus += TextBox_LostFocus;
            txtX2.GotFocus += TextBox_GotFocus;
            txtX2.LostFocus += TextBox_LostFocus;
            txtY2.GotFocus += TextBox_GotFocus;
            txtY2.LostFocus += TextBox_LostFocus;
            txtX3.GotFocus += TextBox_GotFocus;
            txtX3.LostFocus += TextBox_LostFocus;
            txtY3.GotFocus += TextBox_GotFocus;
            txtY3.LostFocus += TextBox_LostFocus;

            // Khởi tạo timer cho hiệu ứng cầu vồng
            this.rainbowTimer = new Timer();
            this.rainbowTimer.Interval = 20; // Cập nhật màu mỗi 20ms để mượt hơn
            this.rainbowTimer.Tick += new EventHandler(this.RainbowTimer_Tick);

            // Gắn sự kiện cho lblCopyright
            labelAuthor.MouseEnter += labelAuthor_MouseEnter;
            labelAuthor.MouseLeave += labelAuthor_MouseLeave;

            // Khởi tạo và cấu hình Timer cho đồng hồ
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 500; // Cập nhật mỗi giây (500ms)
            timer.Tick += Timer_Tick;
            timer.Start();

            // Tạo đường dẫn file dựa trên ngày hiện tại
            SetFilePath();

            // Thêm giới hạn 300 ký tự cho txtAPN
            txtAPN.MaxLength = 300;

            //gán sự kiện Click cho labelStatus để mở thư mục chứa file khi click
            labelStatus.Click += labelStatus_Click;
            labelSoCellDaLuu.Click += labelSoCellDaLuu_Click;

            // Tải số APN duy nhất đã lưu khi khởi động
            LoadSavedCount();

            // Thêm sự kiện KeyDown cho txtAPN để xử lý phím Enter
            txtAPN.KeyDown += txtAPN_KeyDown;

            // Khởi tạo ToolTip
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000; // Hiển thị tooltip trong 5 giây
            toolTip.InitialDelay = 100;   // Thời gian chờ trước khi hiển thị (100ms)
            toolTip.ReshowDelay = 100;    // Thời gian chờ khi hiển thị lại
            toolTip.ShowAlways = true;    // Hiển thị ngay cả khi mất focus

            toolTip.SetToolTip(labelSoCellDaLuu, "BẤM VÀO ĐỂ MỞ THƯ MỤC LƯU FILE");

            // Cập nhật trạng thái ban đầu
            UpdateStatus("Sẵn sàng nhập liệu...", System.Drawing.Color.Green);
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
                    if (lbl.Font != null) lbl.Font.Dispose();
                    lbl.Font = newFont;
                }
            }
        }

        // Hiệu ứng nhấn nút
        private async void ApplyButtonClickEffect(Button button)
        {
            Color originalColor = button.BackColor;
            button.BackColor = ColorTranslator.FromHtml("#00BFFF"); // Màu khi nhấn
            await Task.Delay(200); // Chờ 200ms

            // Nếu là nút số hoặc nút dấu chấm thì khôi phục về trắng
            if (button.Name.StartsWith("btn") &&
                (button.Text.All(char.IsDigit) || button.Text == "."))
            {
                button.BackColor = Color.White;
            }
            else
            {
                button.BackColor = originalColor; // Các nút đặc biệt giữ màu gốc
            }

            this.ActiveControl = null; // 👈 thêm dòng này
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

        // Sự kiện cho đồng hồ
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian theo GMT+7
            TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // GMT+7
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);
            labelDateTime.Text = vietnamTime.ToString("HH:mm:ss\ndd/MM/yyyy");
        }
        #endregion

        #region THAY ĐỔI MÀU SẮC KHI DI CHUỘT VÀO TÊN TÁC GIẢ
        // Sự kiện Tick của timer, cập nhật màu sắc
        private void RainbowTimer_Tick(object sender, EventArgs e)
        {
            rainbowPhase += 0.05; // Giảm tốc độ thay đổi để màu chuyển từ từ hơn

            Color newColor = CalculateRainbowColor(rainbowPhase);
            labelAuthor.ForeColor = newColor;
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
        private void labelAuthor_MouseEnter (object sender, EventArgs e)
        {
            if (!isRainbowActive)
            {
                isRainbowActive = true;
                originalAuthorColor = labelAuthor.ForeColor;
                rainbowTimer.Start();
            }
        }

        // Khi chuột rời nhãn, tắt hiệu ứng và khôi phục màu gốc
        private void labelAuthor_MouseLeave(object sender, EventArgs e)
        {
            if (isRainbowActive)
            {
                isRainbowActive = false;
                rainbowTimer.Stop();
                labelAuthor.ForeColor = originalAuthorColor;
            }
        }
        #endregion

        #region XỬ LÝ HOẠT ĐỘNG CÁC BUTTON
        private void OnTextBoxEnter(object sender, EventArgs e)
        {
            activeTextBox = sender as TextBox;
        }

        // Sự kiện xử lý phím Enter trên txtAPN
        private void txtAPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtX1.Focus();
                e.SuppressKeyPress = true; // Ngăn không cho tiếng "ding" xuất hiện
            }
        }

        // Sự kiện xử lý phím cho các ô tọa độ, chỉ cho phép nhập số
        private void txtCoord_KeyDown(object sender, KeyEventArgs e)
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

        private void btnNumber_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            if (activeTextBox != null)
            {
                Button btn = sender as Button;
                bool isCoordTextBox = activeTextBox.Name.StartsWith("txtX") || activeTextBox.Name.StartsWith("txtY");

                if (isCoordTextBox)
                {
                    string newText = activeTextBox.Text + btn.Text;
                    if (newText.Length > activeTextBox.MaxLength)
                    {
                        return; // Ngăn không cho nhập quá giới hạn
                    }

                    // Ngăn không cho nhập dấu trừ ở giữa, cuối hoặc nhiều lần
                    if (btn.Text == "." && activeTextBox.Text.Length > 0)
                    {
                        return;
                    }

                    // Ngăn không cho nhập số 0 đầu tiên
                    if (btn.Text != "." && activeTextBox.Text.Length == 0 && btn.Text == "0")
                    {
                        return;
                    }
                }

                activeTextBox.Text += btn.Text;
                activeTextBox.SelectionStart = activeTextBox.Text.Length;               
                activeTextBox.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            if (activeTextBox != null && activeTextBox.Text.Length > 0)
            {
                activeTextBox.Text = activeTextBox.Text.Remove(activeTextBox.Text.Length - 1);
                activeTextBox.SelectionStart = activeTextBox.Text.Length;               
                activeTextBox.Focus();
            }
        }

        private void btnTenLoi_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var DefectForm = new DefectForm())
            {
                if (DefectForm.ShowDialog() == DialogResult.OK)
                {
                    labelTenLoi.Text = DefectForm.SelectedDefect;
                    UpdateStatus("Đã chọn tên lỗi.", System.Drawing.Color.Blue);
                }
            }
        }


        private void btnLevel_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var levelForm = new LevelForm())
            {
                if (levelForm.ShowDialog() == DialogResult.OK)
                {
                    labelLevel.Text = levelForm.SelectedLevel;
                    UpdateStatus("Đã chọn Level.", System.Drawing.Color.Blue);
                }
            }
        }
        private void btnDK_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            if (!string.IsNullOrEmpty(labelLevel.Text))
            {
                if (labelLevel.Text.EndsWith("^DK"))
                {
                    labelLevel.Text = labelLevel.Text.Replace("^DK", ""); // Hủy nếu đã có ^DK
                    UpdateStatus("Đã hủy phân loại Level tối (DK).", System.Drawing.Color.Blue);
                }
                else if (!labelLevel.Text.Contains("^"))
                {
                    labelLevel.Text = labelLevel.Text + "^DK"; // Ghép ^DK nếu chưa có hậu tố
                    UpdateStatus("Đã chọn Level tối (DK).", System.Drawing.Color.Blue);
                }
                // Nếu đã có ^BR, thay bằng ^DK
                else if (labelLevel.Text.EndsWith("^BR"))
                {
                    labelLevel.Text = labelLevel.Text.Replace("^BR", "^DK");
                    UpdateStatus("Đã thay bằng Level tối (DK).", System.Drawing.Color.Blue);
                }
            }
        }

        private void btnBR_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            if (!string.IsNullOrEmpty(labelLevel.Text))
            {
                if (labelLevel.Text.EndsWith("^BR"))
                {
                    labelLevel.Text = labelLevel.Text.Replace("^BR", ""); // Hủy nếu đã có ^BR
                    UpdateStatus("Đã hủy phân loại Level sáng (BR).", System.Drawing.Color.Blue);
                }
                else if (!labelLevel.Text.Contains("^"))
                {
                    labelLevel.Text = labelLevel.Text + "^BR"; // Ghép ^BR nếu chưa có hậu tố
                    UpdateStatus("Đã chọn Level sáng (BR).", System.Drawing.Color.Blue);
                }
                // Nếu đã có ^DK, thay bằng ^BR
                else if (labelLevel.Text.EndsWith("^DK"))
                {
                    labelLevel.Text = labelLevel.Text.Replace("^DK", "^BR");
                    UpdateStatus("Đã thay bằng Level sáng (BR).", System.Drawing.Color.Blue);
                }
            }
        }
        private void btnPattern_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var patternForm = new PatternForm())
            {
                if (patternForm.ShowDialog() == DialogResult.OK)
                {
                    labelPattern.Text = patternForm.SelectedPattern;
                    UpdateStatus("Đã chọn Pattern.", System.Drawing.Color.Blue);
                }
            }
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            this.ActiveControl = null;
            using (var mappingForm = new MappingForm())
            {
                if (mappingForm.ShowDialog() == DialogResult.OK)
                {
                    labelMapping.Text = mappingForm.SelectedMapping;
                    UpdateStatus("Đã chọn Mapping.", System.Drawing.Color.Blue);
                }
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(txtAPN.Text))
            {
                toolTip.Show("Vui lòng nhập APN", txtAPN, 0, txtAPN.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            if (string.IsNullOrWhiteSpace(txtX1.Text))
            {
                toolTip.Show("Vui lòng nhập tọa độ", txtX1, 0, txtX1.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            if (string.IsNullOrWhiteSpace(txtY1.Text))
            {
                toolTip.Show("Vui lòng nhập tọa độ", txtY1, 0, txtY1.Height, 3500); // Hiển thị tooltip 3 giây
                return;
            }
            //Kiểm tra tên lỗi
            //if (string.IsNullOrWhiteSpace(labelTenLoi.Text))
            //{
            //    toolTip.Show("Vui lòng chọn TÊN LỖI", labelTenLoi, 0, labelTenLoi.Height, 3500); // Hiển thị tooltip 3 giây
            //    isValidationFailed = true;
            //    return;
            //}

            string apn = txtAPN.Text;
            string x1 = txtX1.Text;
            string y1 = txtY1.Text;
            string x2 = txtX2.Text;
            string y2 = txtY2.Text;
            string x3 = txtX3.Text;
            string y3 = txtY3.Text;
            string tenLoi = labelTenLoi.Text; // Thay txtTenLoi bằng labelTenLoi
            string level = labelLevel.Text; // Thay labelLevel bằng labelLevel
            string pattern = labelPattern.Text; // Thay txtPattern bằng labelPattern
            string mapping = labelMapping.Text; // Thay txtMapping bằng labelMapping
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
                labelCount.Text = savedCellCount.ToString();
                labelDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                UpdateStatus($"Lưu thành công !\nDữ liệu đã được ghi lại: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nĐường dẫn file: {filePath}", System.Drawing.Color.ForestGreen);

                // Xóa dữ liệu các trường sau khi lưu thành công
                txtAPN.Text = "";
                txtX1.Text = "";
                txtY1.Text = "";
                txtX2.Text = "";
                txtY2.Text = "";
                txtX3.Text = "";
                txtY3.Text = "";
                labelTenLoi.Text = ""; // Thay txtTenLoi bằng labelTenLoi
                labelLevel.Text = ""; // Thay labelLevel bằng labelLevel
                labelPattern.Text = ""; // Thay txtPattern bằng labelPattern
                labelMapping.Text = ""; // Thay txtMapping bằng labelMapping
                txtAPN.Focus();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            ApplyButtonClickEffect(sender as Button); // Thêm hiệu ứng
            txtAPN.Text = "";
            txtX1.Text = "";
            txtY1.Text = "";
            txtX2.Text = "";
            txtY2.Text = "";
            txtX3.Text = "";
            txtY3.Text = "";
            labelTenLoi.Text = ""; // Thay txtTenLoi bằng labelTenLoi
            labelLevel.Text = ""; // Thay labelLevel bằng labelLevel
            labelPattern.Text = ""; // Thay txtPattern bằng labelPattern
            labelMapping.Text = ""; // Thay txtMapping bằng labelMapping

            txtAPN.Focus();
            UpdateStatus("Giao diện UI đã khởi tạo lại.", System.Drawing.Color.Teal);
        }
        #endregion

        #region CẬP NHẬT TRẠNG THÁI
        private void UpdateStatus(string message, System.Drawing.Color? color = null)
        {
            labelStatus.Text = message;
            if (color.HasValue)
            {
                labelStatus.ForeColor = color.Value;
            }
            else
            {
                labelStatus.ForeColor = System.Drawing.Color.Black;
            }

            // Kích hoạt tooltip khi lưu thành công (màu xanh lá)
            if (color == System.Drawing.Color.Green)
            {
                toolTip.SetToolTip(labelStatus, "BẤM VÀO ĐỂ MỞ THƯ MỤC");
            }
            else
            {
                toolTip.SetToolTip(labelStatus, "");
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
                labelCount.Text = savedCellCount.ToString();
            }
        }
        private void labelStatus_Click(object sender, EventArgs e)
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
        private void labelSoCellDaLuu_Click(object sender, EventArgs e)
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
        private void SetFilePath()
        {
            // Lấy múi giờ GMT+7
            TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);
            string dateString = vietnamTime.ToString("yyyyMMdd");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directoryPath = Path.Combine(desktopPath, "FAB_CONFIRM");
            filePath = Path.Combine(directoryPath, $"FAB_CONFIRM_{dateString}.csv");

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
    }
}