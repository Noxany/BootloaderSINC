using AppUpdaterService;
using System;
using System.Diagnostics;
using System.Linq;

namespace BootloaderSINC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var localPath = args.Length > 0 ? args[0] ?? Environment.CurrentDirectory : Environment.CurrentDirectory;
            var fileName = args.Length > 1 ? args[1] ?? "SampleApplication.exe" : "SampleApplication.exe";

            var filePath = Path.Combine(localPath, fileName);

            var currentVersion = GetFileVersion(filePath);

            var updaterService = new WebApiUpdateService();

            var serverVersionNumber = await updaterService.GetCurrentVersionNumber();

            if(!currentVersion.Equals(serverVersionNumber,StringComparison.InvariantCultureIgnoreCase))
            {
                var serverVersion = await updaterService.GetVersion(serverVersionNumber);
                await File.WriteAllBytesAsync(filePath, serverVersion);
            }

            using var process = Process.Start(filePath);
        }

        private static string GetFileVersion(string filePath)
        {
            try
            {
                return FileVersionInfo.GetVersionInfo(filePath).FileVersion ?? String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}