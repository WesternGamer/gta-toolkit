using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using System.Runtime.InteropServices;
using System;
using System.IO;
using TextureTool.Utill.Config;
using TextureTool.Views;

namespace RDR2TextureTool.Views
{
    /// <summary>
    /// Interaction logic for FirstStartupPart2.xaml
    /// </summary>
    public partial class FirstStartupPart2 : Page
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public FirstStartupPart2()
        {
            InitializeComponent();
        }

        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(RedMInstallationFolderTextBox.Text))
            {
                XMLWriter.WriteXMLEntry("false", RedMInstallationFolderTextBox.Text);

                var w = Application.Current.Windows[0];
                w.Hide();

                MainView mainView = new MainView();
                mainView.Show();
            }
            else
            {
                if (RedMInstallationFolderTextBox.Text == "")
                {
                    MessageBox.Show("No path has been selected. Please select a path to continue.", "An Error has Occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(RedMInstallationFolderTextBox.Text + " is not a valid path. Please select a vaild path.", "An Error has Occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            FirstStartupPart1 firstStartupPart1 = new FirstStartupPart1();
            this.NavigationService.Navigate(firstStartupPart1);
        }

        private void OpenFileExplorer(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.ShowDialog(GetForegroundWindow());
            RedMInstallationFolderTextBox.Text = folderBrowserDialog.SelectedPath;
        }
    }
}
