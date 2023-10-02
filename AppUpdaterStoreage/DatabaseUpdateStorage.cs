using AppUpdaterIStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdaterStoreage
{
    public class DatabaseUpdateStorage : IUpdateStorage
    {
        public Task<string> GetCurrentVersionNumber()
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetVersion(string version)
        {
            throw new NotImplementedException();
        }
    }
}
