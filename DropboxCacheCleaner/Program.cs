using System;
using System.IO;
using System.Threading.Tasks;

namespace DropboxCacheCleaner
{
    class Program
    {
        private static FileSystemWatcher _fsw;

        static void Main(string[] args)
        {
            _fsw = new FileSystemWatcher(Path.Combine(args[0], ".dropbox.cache"));
            _fsw.Created += _fsw_Created; ;
            _fsw.IncludeSubdirectories = true;
            _fsw.EnableRaisingEvents = true;

            Console.ReadLine();
        }

        private static void _fsw_Created(object sender, FileSystemEventArgs e)
        {
            CheckDelete(e.FullPath);
        }

        private static void CheckDelete(string path, int count = 5)
        {
            if (path.IndexOf("(deleted") != -1)
            {
                Console.WriteLine($"{count} Delete: \"{path}\"");
                try { File.Delete(path); }
                catch { if (count > 0) Task.Delay(1000).ContinueWith(_ => CheckDelete(path, count - 1)); }
            }
        }
    }
}
