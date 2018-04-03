using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itac.OemAccess.TestingServer.Model
{
    public sealed class BootConfiguration
    {
        public Boot Boot { get; set; }
    }

    public sealed class Boot
    {
        public string DefaultUri { get; set; }
        public string SharedKey { get; set; }
        public string PlatformConfig { get; set; }
        public ServiceBaseUri[] Services { get; set; } = new ServiceBaseUri[0];
        public BootCustomHeader[] CustomHeaders { get; set; } = new BootCustomHeader[0];
        public BootNetwork Network { get; set; }
    }

    public sealed class BootCustomHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public sealed class BootNetwork
    {
        public bool DhcpEnabled { get; set; }
    }
}
