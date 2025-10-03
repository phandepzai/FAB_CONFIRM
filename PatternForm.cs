using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace FAB_CONFIRM
{
    #region HIỂN THỊ FORM PATTERN
    public partial class PatternForm : Form
    {
        public string SelectedPattern { get; private set; }

        private readonly List<string> patternNames = new List<string>
        {
            "PATTERN-001", "PATTERN-002", "PATTERN-003", "PATTERN-004", "PATTERN-005", "PATTERN-006",
            "PATTERN-007", "PATTERN-008", "PATTERN-009", "PATTERN-010", "PATTERN-011", "PATTERN-012",
            "PATTERN-013", "PATTERN-014", "PATTERN-015", "PATTERN-016", "PATTERN-017", "PATTERN-018",
            "PATTERN-019", "PATTERN-020", "PATTERN-021", "PATTERN-022", "PATTERN-023", "PATTERN-024",
            "PATTERN-025", "PATTERN-026", "PATTERN-027", "PATTERN-028", "PATTERN-029", "PATTERN-030",
        };

        // Constructor mặc định, sử dụng danh sách hardcode
        public PatternForm()
        {
            InitializeComponent();
            CreatePatternButtons(patternNames);
        }

        // Constructor mới, nhận danh sách từ MainForm
        public PatternForm(List<string> patterns)
        {
            InitializeComponent();
            CreatePatternButtons(patterns);
        }

        // Phương thức chung để tạo các nút từ một danh sách
        private void CreatePatternButtons(List<string> names)
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
                btn.Click += PatternButton_Click;
                flowLayoutPanel.Controls.Add(btn);
            }
        }

        private void PatternButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.BackColor == System.Drawing.Color.LightGreen)
                {
                    btn.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            var selectedPatternsList = new List<string>();
            foreach (Control control in flowLayoutPanel.Controls)
            {
                if (control is Button btn && btn.BackColor == System.Drawing.Color.LightGreen)
                {
                    selectedPatternsList.Add(btn.Text);
                }
            }

            if (selectedPatternsList.Count > 0)
            {
                SelectedPattern = string.Join(",", selectedPatternsList);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một Pattern!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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