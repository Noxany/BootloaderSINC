using AppUpdaterIStorage;
using System.Data.SqlTypes;
using System.IO;

namespace AppUpdaterStoreage
{
    public class FilesystemUpdateStorage : IUpdateStorage
    {
        private readonly string versionDirectory = "C:\\SampleApplication";

        public async Task<string> GetCurrentVersionNumber()
        {
            foreach (var directory in new DirectoryInfo(versionDirectory).GetDirectories().OrderByDescending(n => n.Name))
            {
                var files = directory.EnumerateFiles();
                if (files.Any(f => f.Extension == ".exe"))
                    return directory.Name;
            }

            return String.Empty;
            //var highestLocalVerison = from directory in new DirectoryInfo(versionDirectory).GetDirectories().OrderByDescending(n => n.Name)
            //                          where directory.EnumerateFiles().Any(f => f.Extension == ".exe")
            //                          select directory;
            //return highestLocalVerison?.FirstOrDefault()?.Name ?? String.Empty;
        }

        public async Task<byte[]> GetVersion(string version)
        {
            var requestVersion = from directory in new DirectoryInfo(versionDirectory).GetDirectories().Where(n => n.Name == version)
                                 where directory.EnumerateFiles().Any(f => f.Extension == ".exe")
                                 select directory;

            if(!requestVersion.Any())
                return Array.Empty<byte>();

            using var memStream = new MemoryStream();

            await requestVersion.FirstOrDefault()
                                .EnumerateFiles()
                                .FirstOrDefault(f => f.Extension == ".exe")
                                .OpenRead()
                                .CopyToAsync(memStream);

            return memStream.ToArray();

        }
    }
}