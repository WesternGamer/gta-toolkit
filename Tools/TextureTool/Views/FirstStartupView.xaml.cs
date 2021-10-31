using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RDR2TextureTool.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FirstStartupView : Window
    {
        public FirstStartupView()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult Result = System.Windows.Forms.MessageBox.Show("Are you sure that you want to exit setup?", "Please Confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Result != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        public static void CloseWindow()
        {
            FirstStartupView firstStartupView = new FirstStartupView();
            firstStartupView.Close();
        }
    }
}
