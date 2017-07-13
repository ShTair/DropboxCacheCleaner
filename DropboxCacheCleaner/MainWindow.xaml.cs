using System;
using System.IO;
using System.Windows;

namespace DropboxCacheCleaner
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var target = Environment.GetCommandLineArgs()[1];

            var fsw = new FileSystemWatcher(Path.Combine(target, ".dropbox.cache"));
            fsw.Created += Fsw_Created;

            fsw.IncludeSubdirectories = true;
            fsw.EnableRaisingEvents = true;
        }

        private void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            CheckDelete(e.FullPath);
        }

        private void CheckDelete(string path)
        {
            if (path.IndexOf("(deleted") != -1)
            {
                Console.WriteLine("Delete: " + path);
                File.Delete(path);
            }
        }
    }
}
