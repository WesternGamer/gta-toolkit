using System;
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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        private bool? IsSettingsChanged = null;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public SettingsView()
        {
            InitializeComponent();
            IsSettingsChanged = false;
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(RedMInstallationFolderTextBox.Text))
            {
                XMLWriter.WriteXMLEntry("false", RedMInstallationFolderTextBox.Text);
                ApplyButton.IsEnabled = false;
                IsSettingsChanged = true;
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

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenFileExplorer(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.ShowDialog(GetForegroundWindow());
            RedMInstallationFolderTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            if (IsSettingsChanged == false)
            {
                if (Directory.Exists(RedMInstallationFolderTextBox.Text))
                {
                    XMLWriter.WriteXMLEntry("false", RedMInstallationFolderTextBox.Text);
                    ApplyButton.IsEnabled = false;
                    IsSettingsChanged = true;
                    this.Close();
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
            else
            {
                this.Close();
            }
        }
    }
}
