using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace YoutubeDownloader
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText("url.txt", AllURLtext.Text);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            string WorkDirectory = System.IO.Directory.GetCurrentDirectory();
            string cdwd = "cd" + " " + WorkDirectory;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();

            string bestfordl = "youtube-dl" + " -f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext]=mp4/best\" " + "url.txt";
            p.StandardInput.WriteLine(cdwd + "&" + bestfordl + "&exit");
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }
    }
}
