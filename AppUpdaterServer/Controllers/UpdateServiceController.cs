using AppUpdaterIStorage;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace AppUpdaterServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateServiceController : ControllerBase
    {
        private readonly IUpdateStorage updateStorage;

        public UpdateServiceController(IUpdateStorage updateStorage) 
        {
            this.updateStorage = updateStorage;
        }


        [HttpGet("GetCurrentVersionNumber")]
        public async Task<string> GetCurrentVersionNumber()
        {
            return await updateStorage.GetCurrentVersionNumber();
        }

        [HttpGet("GetVersion")]
        public async Task<byte[]> GetVersion(string version)
        {
            return await updateStorage.GetVersion(version);
        }
    }
}