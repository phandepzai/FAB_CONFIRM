using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    public partial class PatternForm : Form
    {
        // Property to store the selected patterns (multi-select)
        public string SelectedPattern { get; private set; }

        // Array of pattern names (editable list)
        private readonly string[] patternNames = new string[]
        {
            "PATTERN-001",
            "PATTERN-002",
            "PATTERN-003",
            "PATTERN-004",
            "PATTERN-005",
            "PATTERN-006",
            "PATTERN-007",
            "PATTERN-008",
            "PATTERN-009",
            "PATTERN-010",
            "PATTERN-011",
            "PATTERN-012",
            "PATTERN-013",
            "PATTERN-014",
            "PATTERN-015",
            "PATTERN-016",
            "PATTERN-017",
            "PATTERN-018",
            "PATTERN-019",
            "PATTERN-020",
            "PATTERN-021",
            "PATTERN-022",
            "PATTERN-023",
            "PATTERN-024",
            "PATTERN-025",
            "PATTERN-026",
            "PATTERN-027",
            "PATTERN-028",
            "PATTERN-029",
            "PATTERN-030",
            "PATTERN-031",
            "PATTERN-032",
            "PATTERN-033",
            "PATTERN-034",
            "PATTERN-035",
            "PATTERN-036",
            "PATTERN-037",
            "PATTERN-038",
            "PATTERN-039",
            "PATTERN-040"
        };

        // Dictionary to track selected buttons
        private Dictionary<string, bool> selectedPatterns = new Dictionary<string, bool>
        {
            { "PATTERN-001", false },
            { "PATTERN-002", false },
            { "PATTERN-003", false },
            { "PATTERN-004", false },
            { "PATTERN-005", false },
            { "PATTERN-006", false },
            { "PATTERN-007", false },
            { "PATTERN-008", false },
            { "PATTERN-009", false },
            { "PATTERN-010", false },
            { "PATTERN-011", false },
            { "PATTERN-012", false },
            { "PATTERN-013", false },
            { "PATTERN-014", false },
            { "PATTERN-015", false },
            { "PATTERN-016", false },
            { "PATTERN-017", false },
            { "PATTERN-018", false },
            { "PATTERN-019", false },
            { "PATTERN-020", false },
            { "PATTERN-021", false },
            { "PATTERN-022", false },
            { "PATTERN-023", false },
            { "PATTERN-024", false },
            { "PATTERN-025", false },
            { "PATTERN-026", false },
            { "PATTERN-027", false },
            { "PATTERN-028", false },
            { "PATTERN-029", false },
            { "PATTERN-030", false },
            { "PATTERN-031", false },
            { "PATTERN-032", false },
            { "PATTERN-033", false },
            { "PATTERN-034", false },
            { "PATTERN-035", false },
            { "PATTERN-036", false },
            { "PATTERN-037", false },
            { "PATTERN-038", false },
            { "PATTERN-039", false },
            { "PATTERN-040", false }
        };

        public PatternForm()
        {
            InitializeComponent();
            // Assign pattern names to buttons
            btnPattern1.Text = patternNames[0];
            btnPattern2.Text = patternNames[1];
            btnPattern3.Text = patternNames[2];
            btnPattern4.Text = patternNames[3];
            btnPattern5.Text = patternNames[4];
            btnPattern6.Text = patternNames[5];
            btnPattern7.Text = patternNames[6];
            btnPattern8.Text = patternNames[7];
            btnPattern9.Text = patternNames[8];
            btnPattern10.Text = patternNames[9];
            btnPattern11.Text = patternNames[10];
            btnPattern12.Text = patternNames[11];
            btnPattern13.Text = patternNames[12];
            btnPattern14.Text = patternNames[13];
            btnPattern15.Text = patternNames[14];
            btnPattern16.Text = patternNames[15];
            btnPattern17.Text = patternNames[16];
            btnPattern18.Text = patternNames[17];
            btnPattern19.Text = patternNames[18];
            btnPattern20.Text = patternNames[19];
            btnPattern21.Text = patternNames[20];
            btnPattern22.Text = patternNames[21];
            btnPattern23.Text = patternNames[22];
            btnPattern24.Text = patternNames[23];
            btnPattern25.Text = patternNames[24];
            btnPattern26.Text = patternNames[25];
            btnPattern27.Text = patternNames[26];
            btnPattern28.Text = patternNames[27];
            btnPattern29.Text = patternNames[28];
            btnPattern30.Text = patternNames[29];
            btnPattern31.Text = patternNames[30];
            btnPattern32.Text = patternNames[31];
            btnPattern33.Text = patternNames[32];
            btnPattern34.Text = patternNames[33];
            btnPattern35.Text = patternNames[34];
            btnPattern36.Text = patternNames[35];
            btnPattern37.Text = patternNames[36];
            btnPattern38.Text = patternNames[37];
            btnPattern39.Text = patternNames[38];
            btnPattern40.Text = patternNames[39];
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