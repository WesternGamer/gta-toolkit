using System;
using TextureTool;
using TextureTool.Utill;
using TextureTool.Utill.Config;


namespace RDR2TextureTool
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FileSystem.Init();
            XMLWriter.Init();
            XMLReader.Init();

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
