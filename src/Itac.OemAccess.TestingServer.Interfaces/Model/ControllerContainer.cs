using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT.OemAccess.Configuration;
using GT.OemAccess.Configuration.Application;

namespace Itac.OemAccess.TestingServer.Model
{
    public class ControllerContainer
    {
        public string Id { get; set; }
        public string SerialNumber { get; set; }
        public BootConfiguration BootConfiguration { get; set; }
        public ApplicationConfiguration ApplicationConfiguration { get; set; }
        public bool CustomAppConfig { get; set; }
        //public HardwareReport HardwareReport { get; set; }
    }
}
