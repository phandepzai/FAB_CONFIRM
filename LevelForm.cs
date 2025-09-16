using System;
using System.Windows.Forms;

namespace FAB_CONFIRM
{
    public partial class LevelForm : Form
    {
        // Property to store the selected level
        public string SelectedLevel { get; private set; }

        public LevelForm()
        {
            InitializeComponent();
        }

        private void LevelButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SelectedLevel = btn.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}