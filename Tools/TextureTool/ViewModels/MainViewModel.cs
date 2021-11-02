/*
    Copyright(c) 2021 WesternGamer

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using TextureTool.Commands;
using TextureTool.Models;
using TextureTool.Views;

namespace TextureTool.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MainModel model;
        private string title;
        private bool textureFilesVisibility;
        private List<TextureDictionaryViewModel> textureDictionaries;
        private TextureDictionaryViewModel selectedTextureDictionary;
        private List<TextureViewModel> textures;
        private TextureViewModel selectedTexture;

        public ICommand NewCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
               
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        public bool TextureFilesVisibility
        {
            get
            {
                return textureFilesVisibility;
            }
            set
            {
                textureFilesVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public List<TextureDictionaryViewModel> TextureDictionaries
        {
            get
            {
                return textureDictionaries;
            }
            set
            {
                textureDictionaries = value;
                NotifyPropertyChanged();
            }
        }

        public TextureDictionaryViewModel SelectedTextureDictionary
        {
            get
            {
                return selectedTextureDictionary;
            }
            set
            {
                selectedTextureDictionary = value;
                NotifyPropertyChanged();

                // update textures...
                BuildTextureList();
            }
        }

        public List<TextureViewModel> Textures
        {
            get
            {
                return textures;
            }
            set
            {
                textures = value;
                NotifyPropertyChanged();
            }
        }
               
        public TextureViewModel SelectedTexture
        {
            get
            {
                return selectedTexture;
            }
            set
            {
                selectedTexture = value;
                NotifyPropertyChanged();
            }
        }

        public MainViewModel()
        {
            model = new MainModel();
            //model.New();

            Title = "RDR2 Texture Toolkit";
            
            NewCommand = new ActionCommand(New_Execute);
            SaveAsCommand = new ActionCommand(Save_Execute, Save_CanExecute);
            ExitCommand = new ActionCommand(Exit_Execute);
            ImportCommand = new ActionCommand(Import_Execute, Import_CanExecute);
            DeleteCommand = new ActionCommand(Delete_Execute, Delete_CanExecute);
            AboutCommand = new ActionCommand(About_Execute);
        }

        public void BuildTextureDictionaryList()
        {
            var list = new List<TextureDictionaryViewModel>();
            for (int index = 0; index < model.TextureDictionaries.Count; index++)
            {
                var vm = new TextureDictionaryViewModel(model.TextureDictionaries[index]);
                list.Add(vm);
            }

            TextureDictionaries = list;

            // select first texture dictionary
            if (TextureDictionaries.Count > 0)
                SelectedTextureDictionary = TextureDictionaries[0];
            else
                SelectedTextureDictionary = null;
        }

        public void BuildTextureList()
        {
            if (SelectedTextureDictionary != null)
            {
                var list = new List<TextureViewModel>();
                for (int index = 0; index < SelectedTextureDictionary.GetModel().Textures.Count; index++)
                {
                    var texture = SelectedTextureDictionary.GetModel().Textures[index];
                    var textureVM = new TextureViewModel(texture);
                    list.Add(textureVM);
                }
                
                Textures = list;
                if (Textures.Count > 0)
                    SelectedTexture = Textures[0];
            }
            else
            {
                Textures = null;
                SelectedTexture = null;
            }           
        }

        public void New_Execute(object parameter)
        {
            if (textures == null)
            {
                model.New();
                Title = "RDR2 Texture Toolkit";
                TextureFilesVisibility = false;

                BuildTextureDictionaryList();
            }
            else
            {
                DialogResult Result = MessageBox.Show("Are you sure that you want to create a new Ytd file?", "Please Confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Result == DialogResult.Yes)
                {
                    model.New();
                    Title = "RDR2 Texture Toolkit";
                    TextureFilesVisibility = false;

                    BuildTextureDictionaryList();
                }
            }
        }

        public bool Save_CanExecute(object parameter)
        {
            if (model.FileType != FileType.None)
                return true;
            else
                return false;
        }
        
        public void Save_Execute(object parameter)
        { 
            if (textures.Any())
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.FileName = model.FileName;
                saveDialog.Filter = "Texture dictionaries|*.ytd";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    model.Save(saveDialog.FileName);
                    Title = saveDialog.FileName + " - RDR2 Texture Toolkit";
                    try
                    {
                        const string quote = "\"";
                        string execPath = AppDomain.CurrentDomain.BaseDirectory;
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        if (Utill.Config.XMLReader.ConverterDirectory.EndsWith(@"\"))
                        {
                            startInfo.Arguments = "/C cd " + quote + Utill.Config.XMLReader.ConverterDirectory + "RedM.app" + quote + " & CitiCon.com formats:convert " + quote + saveDialog.FileName + quote;
                        }
                        else
                        {
                            startInfo.Arguments = "/C cd " + quote + Utill.Config.XMLReader.ConverterDirectory + @"\RedM.app" + quote + " & CitiCon.com formats:convert " + quote + saveDialog.FileName + quote;
                        }
                        startInfo.Verb = "runas";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception e)
                    {
                        if (e is System.ComponentModel.Win32Exception)
                        {
                            MessageBox.Show("Unable to convert GTA5 texture file to RDR2 texture file because CitiCon.com executable is missing or the program does not have permission to access CitiCon.com.", "An Error has Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(e.Message, "An Error has Occurred.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("There are no files to save. Please add a file(s) to save.", "No files to save.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Exit_Execute(object parameter)
        {
            DialogResult Result = MessageBox.Show("Are you sure that you want to exit?", "Please Confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Result == DialogResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        
        public bool Import_CanExecute(object parameter)
        {
            if (SelectedTextureDictionary != null)
                return true;
            else
                return false;
        }

        public void Import_Execute(object parameter)
        {
            var importDialog = new OpenFileDialog();
            importDialog.FileName = "*.dds";
            importDialog.Filter = "DDS files (.dds)|*.dds";
            importDialog.Multiselect = true;
            if (importDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var fileName in importDialog.FileNames)
                    SelectedTextureDictionary.GetModel().Import(fileName, model.FileType != FileType.TextureDictionaryFile);
            }

            BuildTextureList();
        }
        
        public bool Delete_CanExecute(object parameter)
        {
            if (model.FileType == FileType.TextureDictionaryFile && SelectedTexture != null)
                return true;
            else
                return false;
        }

        public void Delete_Execute(object parameter)
        {
            SelectedTextureDictionary.GetModel().Delete(SelectedTexture.GetModel());

            BuildTextureList();
        }

        public void About_Execute(object parameter)
        {
            if (!System.Windows.Application.Current.Windows.OfType<AboutView>().Any())
            {
                AboutView aboutWindow = new AboutView();
                aboutWindow.Show();
            }
        }
    }
}