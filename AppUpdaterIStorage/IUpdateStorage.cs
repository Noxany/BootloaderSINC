using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdaterIStorage
{
    public interface IUpdateStorage
    {
        Task<string> GetCurrentVersionNumber();
        Task<byte[]> GetVersion(string version);
    }
}
