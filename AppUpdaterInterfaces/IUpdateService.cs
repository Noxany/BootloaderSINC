using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdaterIService
{
    public interface IUpdateService
    {
        Task<string> GetCurrentVersionNumber();
        Task<byte[]> GetVersion(string version);
    }
}
