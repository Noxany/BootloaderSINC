using AppUpdaterIStorage;
using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;
using System.IO;

namespace AppUpdaterStoreage
{
    public class FilesystemUpdateStorage : IUpdateStorage
    {
        private readonly string versionDirectory = String.Empty;

        public FilesystemUpdateStorage(IConfiguration configuration)
        {
            if(configuration != null)
                versionDirectory = configuration["VersionDirectory"];
        }

        public async Task<string> GetCurrentVersionNumber()
        {
            foreach (var directory in new DirectoryInfo(versionDirectory).GetDirectories().OrderByDescending(n => n.Name))
            {
                var files = directory.EnumerateFiles();
                if (files.Any(f => f.Extension == ".exe"))
                    return directory.Name;
            }

            return String.Empty;
        }

        public async Task<byte[]> GetVersion(string version)
        {
            var directory = new DirectoryInfo(versionDirectory).GetDirectories()
                .FirstOrDefault(n => n.Name.Equals(version, StringComparison.InvariantCultureIgnoreCase) 
                && n.EnumerateFiles().Any(f => f.Extension == ".exe"));

            if(directory == null)
                return Array.Empty<byte>();

            using var memStream = new MemoryStream();

            await directory.EnumerateFiles().First(f => f.Extension == ".exe").OpenRead().CopyToAsync(memStream);

            return memStream.ToArray();
        }
    }
}