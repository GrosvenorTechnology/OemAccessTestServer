using System.Collections.Generic;
using Itac.OemAccess.TestingServer.ControlApi;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class NetworkNotificationManager
    {
        public void SubmitNetworkEvent(string uri, string serialNumber)
        {
            if (uri.Contains("changes"))
                SubmitQueue(uri, NetworkEventTypes.ChangesRequest, serialNumber);
            else if (uri.Contains("commands"))
                SubmitQueue(uri, NetworkEventTypes.CommandRequest, serialNumber);
            else if (uri.Contains("states/messages"))
                SubmitQueue(uri, NetworkEventTypes.StateNotificationRequest, serialNumber);
            else if (uri.Contains("platformconfiguration"))
                SubmitEvent(uri, NetworkEventTypes.PlatConfig, serialNumber);
            else if (uri.Contains("applicationconfiguration"))
                SubmitEvent(uri, NetworkEventTypes.AppConfig, serialNumber);
            else if (uri.Contains("heartbeat"))
                SubmitEvent(uri, NetworkEventTypes.Heartbeat, serialNumber);
            else if (uri.Contains("hardware"))
                SubmitEvent(uri, NetworkEventTypes.Hardware, serialNumber);
            else if (uri.Contains("events"))
                SubmitEvent(uri, NetworkEventTypes.Event, serialNumber);
            else if (uri.Contains("states"))
                SubmitEvent(uri, NetworkEventTypes.StateNotification, serialNumber);
            else if (uri.Contains("commands"))
                SubmitEvent(uri, NetworkEventTypes.CommandResponse, serialNumber);

        }

        private void SubmitQueue(string uri, NetworkEventTypes type,string serialNumber)
        {
            var count = uri.Split('=')[1];
            OemServerHub.NetworEvent(new NetworkEvent()
            {
                Type = type,
                Uri = uri,
                Values = new Dictionary<string, string>()
                {
                    { "count",count}
                }
            }, serialNumber);
        }

        private void SubmitEvent(string uri, NetworkEventTypes type, string serialNumber)
        {
            OemServerHub.NetworEvent(new NetworkEvent()
            {
                Type = type,
                Uri = uri
            }, serialNumber);
        }

    }
}
