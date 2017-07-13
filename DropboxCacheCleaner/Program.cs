using System;
using System.IO;

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

        private static void CheckDelete(string path)
        {
            if (path.IndexOf("(deleted") != -1)
            {
                Console.WriteLine("Delete: " + path);
                File.Delete(path);
            }
        }
    }
}
