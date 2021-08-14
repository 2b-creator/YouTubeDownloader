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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using YoutubeDownloader.Properties;
using System.IO;

namespace YoutubeDownloader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SavePath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "请选择文件夹";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SavePath.Text = fd.SelectedPath;
                string text = SavePath.Text;
                File.WriteAllText("path.txt", text);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void GoDownloader_Click(object sender, RoutedEventArgs e)
        {
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

            if (Merge.IsChecked == true)
            {
                string directory = SavePath.Text;
                string proxy = proxyIP.Text;
                string bestfordl = "youtube-dl" + " -f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext]=mp4/best\" " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + bestfordl + "&exit");
            }
            else if (Divide.IsChecked == true)
            {
                string directory = SavePath.Text;
                string dividedl = "youtube-dl " + "-f \"bestvideo,bestaudio\" " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + dividedl + "&exit");
            }
            else if (Video.IsChecked == true)
            {
                string directory = SavePath.Text;
                string onlyVideodl = "youtube-dl " + "-f bestvideo " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + onlyVideodl + "&exit");
            }
            else if (Audio.IsChecked == true)
            {
                string directory = SavePath.Text;
                string onlyAudiodl = "youtube-dl " + "-f bestaudio " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + onlyAudiodl + "&exit");
            }
            if (subzh.IsChecked == true)
            {
                string ChineseSrtDownload = "youtube-dl --sub-lang zh-Hans " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + ChineseSrtDownload + "&exit");
            }
            else if (suben.IsChecked == true)
            {
                string EnglishSrtDownload = "youtube-dl --sub-lang English " + dlurl.Text;
                p.StandardInput.WriteLine(cdwd + "&" + EnglishSrtDownload + "&exit");
            }
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            proxyIP.Text = "//127.0.0.1:1080/";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
        }
    }
}
