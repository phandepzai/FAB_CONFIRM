using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    public partial class DefectForm : Form
    {
        // Thuộc tính để lưu trữ tên Lỗi đã chọn
        public string SelectedDefect { get; private set; }

        public DefectForm()
        {
            InitializeComponent();
            foreach (var name in defectNames)
            {
                Button btn = new Button();
                btn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
                btn.Size = new System.Drawing.Size(120, 40); // Điều chỉnh kích thước nếu cần
                btn.Text = name;
                btn.UseVisualStyleBackColor = true;
                btn.Click += DefectButton_Click;
                flowLayoutPanel.Controls.Add(btn); // Giả sử thêm FlowLayoutPanel như dưới
            }
        }

        // Liệt kê để lưu trữ tên lỗi để dễ bảo trì
        private List<string> defectNames = new List<string>
        {
            "Lỗi phần mềm hệ thốngAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
            "Lỗi phần mềm ứng dụng",
            "Lỗi phần mềm bảo mật",
            "Lỗi phần mềm giao diện",
            "Lỗi phần mềm dữ liệu",
            "Lỗi phần mềm kết nối",
            "Lỗi phần mềm hiệu suất",
            "Lỗi phần mềm tương thích",
            "Lỗi phần mềm cập nhật",
            "Lỗi phần mềm cài đặt",
            "Lỗi phần mềm gỡ bỏ",
            "Lỗi phần mềm đa nhiệm",
            "Lỗi phần mềm đa nền tảng",
            "Lỗi phần mềm đa ngôn ngữ",
            "Lỗi phần mềm đa người dùng",
            "Lỗi phần mềm thời gian thực",
            "Lỗi phần mềm nhúng",
            "Lỗi phần mềm di động",
            "Lỗi phần mềm web",
            "Lỗi phần mềm đám mây",
            "Lỗi phần mềm AI",
            "Lỗi phần mềm học máy",
            "Lỗi phần mềm big data",
            "Lỗi phần mềm IoT",
            "Lỗi phần mềm blockchain",
            "Lỗi phần mềm VR/AR",
            "Lỗi phần mềm game",
            "Lỗi phần mềm multimedia",
            "Lỗi phần mềm khoa học",
            "Lỗi phần mềm kinh doanh",
            "Lỗi phần mềm giáo dục",
            "Lỗi phần mềm y tế",
            "Lỗi phần mềm tài chính",
            "Lỗi phần mềm logistics",
            "Lỗi phần mềm sản xuất",
            "Lỗi phần mềm nông nghiệp",
            "Lỗi phần mềm giao thông",
            "Lỗi phần mềm năng lượng",
            "Lỗi phần mềm môi trường",
            "Lỗi phần mềm xã hội",
            "Lỗi phần mềm chính phủ",
            "Lỗi phần mềm quân sự",
            "Lỗi phần mềm không gian",
            "Lỗi phần mềm robot",
            "Lỗi phần mềm tự động hóa",
            "Lỗi phần mềm kiểm soát",
            "Lỗi phần mềm mô phỏng",
            "Lỗi phần mềm phân tích",
            "Lỗi phần mềm tối ưu hóa",
            "Lỗi phần mềm dự báo",
            "Lỗi phần mềm học sâu",
            "Lỗi phần cứng bộ nhớ",
            "Lỗi phần cứng CPU",
            "Lỗi phần cứng GPU",
            "Lỗi phần cứng đầu vào",
            "Lỗi phần cứng đầu ra",
            "Lỗi phần cứng xử lý",
            "Lỗi phần cứng nguồn",
            "Lỗi phần cứng nhiệt",
            "Lỗi phần cứng tổng hợp"
            // Thêm, bớt tên lỗi ở đây để bảo trì
        };

        private void DefectButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                SelectedDefect = btn.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}