using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class ApplicationRepository
    {
        private Dictionary<string,string> _store = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);

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
