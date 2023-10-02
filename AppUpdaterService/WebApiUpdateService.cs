using AppUpdaterIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdaterService
{
    public class WebApiUpdateService : IUpdateService
    {
        private const string apiUrl = "https://localhost:7229/UpdateService/";

        public async Task<string> GetCurrentVersionNumber()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(apiUrl + "GetCurrentVersionNumber");

                if (!response.IsSuccessStatusCode)
                    return String.Empty;

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }
        }

        public async Task<byte[]> GetVersion(string version)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync($"{apiUrl}GetVersion?Version={version}");

                if (!response.IsSuccessStatusCode)
                    return Array.Empty<byte>();

                var resultString = (await response.Content.ReadAsStringAsync()).Replace("\"", string.Empty);

                return Convert.FromBase64String(resultString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }
    }
}
