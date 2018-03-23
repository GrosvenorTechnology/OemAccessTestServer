using System.Collections.Generic;

namespace Itac.OemAccess.TestingServer.Model
{
    public class HeartbeatResponse
    {
        public List<string> Activity { get; set; }
    }

    public enum Activities
    {
        bootConfig,
        platformConfig,
        applicationConfig,
        changes,
        commands,
        states
    }

}
