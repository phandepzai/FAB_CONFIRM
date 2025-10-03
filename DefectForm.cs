using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    #region HIỂN THỊ FORM DEFECT
    public partial class DefectForm : Form
    {
        public string SelectedDefect { get; private set; }

        private readonly List<string> defectNames = new List<string>
        {
            "Lỗi phần mềm hệ thống", "Lỗi phần mềm ứng dụng", "Lỗi phần mềm bảo mật",
            "Lỗi phần mềm giao diện", "Lỗi phần mềm dữ liệu", "Lỗi phần mềm kết nối",
            "Lỗi phần mềm hiệu suất", "Lỗi phần mềm tương thích", "Lỗi phần mềm cập nhật",
            "Lỗi phần mềm bảo mật", "Lỗi phần mềm nền tảng", "Lỗi phần mềm di động",
            "Lỗi phần mềm web", "Lỗi phần mềm cloud", "Lỗi phần mềm AI",
            "Lỗi phần mềm IoT", "Lỗi phần mềm big data", "Lỗi phần mềm học máy",
            "Lỗi phần mềm mạng", "Lỗi phần mềm game", "Lỗi phần mềm tài chính",
            "Lỗi phần mềm y tế", "Lỗi phần mềm giáo dục", "Lỗi phần mềm thương mại",
            "Lỗi phần mềm quản lý", "Lỗi phần mềm nông nghiệp", "Lỗi phần mềm giao thông",
            "Lỗi phần mềm năng lượng", "Lỗi phần mềm môi trường", "Lỗi phần mềm xã hội",
            "Lỗi phần mềm chính phủ", "Lỗi phần mềm quân sự", "Lỗi phần mềm không gian",
            "Lỗi phần mềm robot", "Lỗi phần mềm tự động hóa", "Lỗi phần mềm kiểm soát",
            "Lỗi phần mềm mô phỏng", "Lỗi phần mềm phân tích", "Lỗi phần mềm tối ưu hóa",
            "Lỗi phần mềm dự báo", "Lỗi phần mềm học sâu", "Lỗi phần cứng bộ nhớ",
            "Lỗi phần cứng CPU", "Lỗi phần cứng GPU", "Lỗi phần cứng đầu vào",
            "Lỗi phần cứng đầu ra", "Lỗi phần cứng xử lý", "Lỗi phần cứng nguồn",
            "Lỗi phần cứng nhiệt", "Lỗi phần cứng tổng hợp"
        };

        // Constructor mặc định, sử dụng danh sách hardcode
        public DefectForm()
        {
            InitializeComponent();
            CreateDefectButtons(defectNames);
        }

        // Constructor mới, nhận danh sách từ MainForm
        public DefectForm(List<string> defects)
        {
            InitializeComponent();
            CreateDefectButtons(defects);
        }

        // Phương thức chung để tạo các nút từ một danh sách
        private void CreateDefectButtons(List<string> names)
        {
            foreach (var name in names)
            {
                Button btn = new Button
                {
                    Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold),
                    Size = new System.Drawing.Size(120, 40),
                    Text = name,
                    UseVisualStyleBackColor = true
                };
                btn.Click += DefectButton_Click;
                flowLayoutPanel.Controls.Add(btn);
            }
        }

        private void DefectButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                SelectedDefect = btn.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
    #endregion
}