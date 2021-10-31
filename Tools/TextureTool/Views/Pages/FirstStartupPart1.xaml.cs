using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace RDR2TextureTool.Views
{
    /// <summary>
    /// Interaction logic for FirstStartupPart1.xaml
    /// </summary>
    public partial class FirstStartupPart1 : Page
    {
        public FirstStartupPart1()
        {
            InitializeComponent();
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            FirstStartupPart2 firstStartupPart2 = new FirstStartupPart2();
            this.NavigationService.Navigate(firstStartupPart2);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            DialogResult Result = System.Windows.Forms.MessageBox.Show("Are you sure that you want to exit setup?", "Please Confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Result == DialogResult.Yes)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.Kill();
            }
        }
    }
}
