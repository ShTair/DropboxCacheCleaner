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
            fsw.Changed += Fsw_Changed;
            fsw.Created += Fsw_Created;
            fsw.Deleted += Fsw_Deleted;
            fsw.Disposed += Fsw_Disposed;
            fsw.Error += Fsw_Error;
            fsw.Renamed += Fsw_Renamed;

            fsw.IncludeSubdirectories = true;
            fsw.EnableRaisingEvents = true;
        }

        private void Fsw_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Fsw_Renamed {e.ChangeType}: {e.OldFullPath} -> {e.FullPath}");
        }

        private void Fsw_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"Fsw_Error: {e}");
        }

        private void Fsw_Disposed(object sender, EventArgs e)
        {
            Console.WriteLine($"Fsw_Disposed");
        }

        private void Fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Fsw_Deleted {e.ChangeType}: {e.FullPath}");
        }

        private void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Fsw_Created {e.ChangeType}: {e.FullPath}");
            CheckDelete(e.FullPath);
        }

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Fsw_Changed {e.ChangeType}: {e.FullPath}");
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
