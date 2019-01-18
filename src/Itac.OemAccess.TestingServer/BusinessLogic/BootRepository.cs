using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itac.OemAccess.TestingServer.BusinessLogic
{
    public class BootRepository
    {
        private readonly Dictionary<string, string> _store = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string Load(string serialNumber)
        {
            return _store[serialNumber];
        }

        public void Save(string serialNumber, string data)
        {
            _store[serialNumber] = data;
        }
    }
}
