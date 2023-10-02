using AppUpdaterService;
using System;
using System.Diagnostics;
using System.Linq;

namespace BootloaderSINC
{
    internal class Program
    {
        private const string DefaultFileName = "SampleApplication.exe";
        private const string DefaultApiUrl = "https://localhost:7229/";

        static async Task Main(string[] args)
        {
            var localPath = GetArgumentOrDefault(args, 0, Environment.CurrentDirectory);
            var fileName = GetArgumentOrDefault(args, 1, DefaultFileName);
            var apiUrl = GetArgumentOrDefault(args, 2, DefaultApiUrl);

            var filePath = Path.Combine(localPath, fileName);

            var currentVersion = GetFileVersion(filePath);

            var updaterService = new WebApiUpdateService(apiUrl);

            var serverVersionNumber = await updaterService.GetCurrentVersionNumber();

            if(!currentVersion.Equals(serverVersionNumber,StringComparison.InvariantCultureIgnoreCase))
            {
                await UpdateApplication(filePath, updaterService, serverVersionNumber);
            }

            LaunchApplication(filePath);
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

        private static string GetArgumentOrDefault(string[] args, int index, string defaultValue)
        {
            return args.Length > index ? args[index] ?? defaultValue : defaultValue;
        }

        private static async Task UpdateApplication(string filePath, WebApiUpdateService updaterService, string serverVersionNumber)
        {
            var serverVersion = await updaterService.GetVersion(serverVersionNumber);
            await File.WriteAllBytesAsync(filePath, serverVersion);
        }

        private static void LaunchApplication(string filePath)
        {
            using var process = Process.Start(filePath);
        }
    }
}