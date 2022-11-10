/*Copyright (c) 2021 WesternGamer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using System;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using System.Runtime.InteropServices;
using System;
using System.IO;
using TextureTool.Utill.Config;
using TextureTool.Views;
using System.Xml;
using System.Text.RegularExpressions;

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
            RedMInstallationFolderTextBox.Text = XMLReader.ConverterDirectory;
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(RedMInstallationFolderTextBox.Text))
            {
                if (!RedMInstallationFolderTextBox.Text.EndsWith(@"\"))
                {
                    RedMInstallationFolderTextBox.Text += @"\";
                }
                XMLWriter.WriteXMLEntry("false", RedMInstallationFolderTextBox.Text);
                XMLReader.ConverterDirectory = RedMInstallationFolderTextBox.Text;
                ApplyButton.IsEnabled = false;
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
            if (Directory.Exists(RedMInstallationFolderTextBox.Text))
            {
                if (!RedMInstallationFolderTextBox.Text.EndsWith(@"\"))
                {
                    RedMInstallationFolderTextBox.Text += @"\";
                }
                XMLWriter.WriteXMLEntry("false", RedMInstallationFolderTextBox.Text);
                XMLReader.ConverterDirectory = RedMInstallationFolderTextBox.Text;
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

                this.Close();
        }
    }
}
