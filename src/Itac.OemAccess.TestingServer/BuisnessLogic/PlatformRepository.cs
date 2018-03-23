using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class PlatformRepository
    {
        private static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GrosvenorTechnology", "OemServer", "platform");

        public string Load(string serialNumber)
        {
            return File.ReadAllText(Path.Combine(Folder, $"{serialNumber}.json"));
        }

        public void Save(string serialNumber, string data)
        {
            Directory.CreateDirectory(Folder);
            var path = Path.Combine(Folder, $"{serialNumber}.json");
            File.WriteAllText(path, data);
        }
    }
}
