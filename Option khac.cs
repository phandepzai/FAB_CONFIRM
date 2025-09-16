Hướng dẫn sử dụng:

Tạo file defects.ini và patterns.ini trong cùng thư mục với file exe (sau khi build).
Nội dung file như ví dụ trên, lưu với encoding UTF-8 (sử dụng Notepad++ hoặc VS Code để đảm bảo).
Nếu file không tồn tại, form sẽ hiển thị lỗi và list rỗng.
Bạn có thể chỉnh sửa file .ini để thêm/sửa tên mà không cần compile lại code.
MainForm.cs không cần sửa vì nó chỉ gọi các form này.


// Để hỗ trợ đọc file .ini với UTF-8, chúng ta cần một parser đơn giản vì API kernel32 mặc định là ANSI và không hỗ trợ tốt Unicode/Tiếng Việt.
// Thêm class IniParser này vào project (có thể tạo file riêng IniParser.cs).

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FAB_CONFIRM
{
    public class IniParser
    {
        private readonly Dictionary<string, Dictionary<string, string>> sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        public IniParser(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File .ini không tồn tại: {filePath}");
            }

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string currentSection = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#")) continue; // Bỏ qua comment và dòng trống

                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = line.Substring(1, line.Length - 2).Trim();
                        if (!sections.ContainsKey(currentSection))
                        {
                            sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                    }
                    else if (currentSection != null && line.Contains("="))
                    {
                        var parts = line.Split(new char[] { '=' }, 2);
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        sections[currentSection][key] = value;
                    }
                }
            }
        }

        public List<string> GetValuesInSection(string sectionName)
        {
            var values = new List<string>();
            if (sections.TryGetValue(sectionName, out var section))
            {
                foreach (var kvp in section)
                {
                    values.Add(kvp.Value); // Lấy values, bỏ qua keys (giả sử keys là Name1, Name2,...)
                }
            }
            return values;
        }
    }
}


// Sửa DefectForm.cs: Load defectNames từ defects.ini thay vì hardcode.
// File defects.ini đặt ở cùng thư mục với exe (Application.StartupPath).
// Nội dung mẫu defects.ini (UTF-8):
// [Defects]
// Name1=Lỗi phần mềm hệ thốngAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
// Name2=Lỗi phần mềm ứng dụng
// ... (thêm các tên khác tương tự, hỗ trợ tiếng Việt)

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace FAB_CONFIRM
{
    public partial class DefectForm : Form
    {
        // Thuộc tính để lưu trữ tên Lỗi đã chọn
        public string SelectedDefect { get; private set; }

        public DefectForm()
        {
            InitializeComponent();
            var defectNames = LoadDefectsFromIni();
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

        private List<string> LoadDefectsFromIni()
        {
            string filePath = Path.Combine(Application.StartupPath, "defects.ini");
            try
            {
                var parser = new IniParser(filePath);
                return parser.GetValuesInSection("Defects");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load defects.ini: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>(); // Trả về list rỗng nếu lỗi
            }
        }

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



// Sửa PatternForm.cs: Load patternNames từ patterns.ini thay vì hardcode.
// Để làm động (nếu số pattern thay đổi), tôi sửa để dùng FlowLayoutPanel tương tự DefectForm.
// Xóa các buttons fixed trong Designer.cs và thêm FlowLayoutPanel.
// File patterns.ini (UTF-8):
// [Patterns]
// Name1=PATTERN-001
// Name2=PATTERN-002
// ... (thêm các tên khác)

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace FAB_CONFIRM
{
    public partial class PatternForm : Form
    {
        // Property to store the selected patterns (multi-select)
        public string SelectedPattern { get; private set; }

        // Dictionary to track selected buttons
        private Dictionary<string, bool> selectedPatterns = new Dictionary<string, bool>();

        public PatternForm()
        {
            InitializeComponent();
            var patternNames = LoadPatternsFromIni();

            // Khởi tạo selectedPatterns và tạo buttons động
            foreach (var name in patternNames)
            {
                selectedPatterns[name] = false;

                Button btn = new Button();
                btn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
                btn.Size = new System.Drawing.Size(122, 40);
                btn.Text = name;
                btn.UseVisualStyleBackColor = true;
                btn.BackColor = Color.White;
                btn.Click += PatternButton_Click;
                flowLayoutPanel.Controls.Add(btn); // Thêm FlowLayoutPanel vào Designer và dùng nó
            }
        }

        private List<string> LoadPatternsFromIni()
        {
            string filePath = Path.Combine(Application.StartupPath, "patterns.ini");
            try
            {
                var parser = new IniParser(filePath);
                var names = parser.GetValuesInSection("Patterns");
                foreach (var name in names)
                {
                    selectedPatterns[name] = false; // Khởi tạo dictionary
                }
                return names;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load patterns.ini: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>(); // Trả về list rỗng nếu lỗi
            }
        }

        private void PatternButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string pattern = btn.Text;
                // Toggle selection
                selectedPatterns[pattern] = !selectedPatterns[pattern];

                // Change button appearance based on selection
                if (selectedPatterns[pattern])
                {
                    btn.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Get all selected patterns
            var selectedPatternsList = new List<string>();
            foreach (var pattern in selectedPatterns)
            {
                if (pattern.Value)
                {
                    selectedPatternsList.Add(pattern.Key);
                }
            }

            if (selectedPatternsList.Count > 0)
            {
                SelectedPattern = string.Join(",", selectedPatternsList); // Join with comma
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một Pattern!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


// Sửa PatternForm.Designer.cs: Xóa các buttons fixed (btnPattern1 đến btnPattern40), thêm FlowLayoutPanel tương tự DefectForm.

namespace FAB_CONFIRM
{
    partial class PatternForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternForm));
            this.labelTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(245, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(172, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "CHỌN PATTERN";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 41);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(634, 379);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(246, 426);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 40);
            this.btnSelect.TabIndex = 41;
            this.btnSelect.Text = "Chọn";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(336, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PatternForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 473);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatternForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CHỌN PATTERN";
            this.ResumeLayout(false);

        }
    }
}

