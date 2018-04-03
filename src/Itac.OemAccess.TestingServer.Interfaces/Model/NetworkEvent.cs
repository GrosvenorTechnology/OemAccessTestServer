using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itac.OemAccess.TestingServer.Model
{
    public enum NetworkEventTypes
    {
        Heartbeat,
        AppConfig,
        PlatConfig,
        ChangesRequest,
        CommandRequest,
        StateNotificationRequest,
        Event,
        StateNotification,
        CommandResponse,
        Hardware
    }
    public class NetworkEvent
    {
        public NetworkEventTypes Type { get; set; }
        public string Uri { get; set; }
        public Dictionary<string, string> Values;

    }
}
