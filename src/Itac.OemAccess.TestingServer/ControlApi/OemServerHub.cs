using Itac.OemAccess.TestingServer.Model;
using Microsoft.AspNet.SignalR;

namespace Itac.OemAccess.TestingServer.ControlApi
{
    public class OemServerHub : Hub
    {
        private static readonly IHubContext MyHubContext = GlobalHost.ConnectionManager.GetHubContext<OemServerHub>();

        //--------------------------------
        //     Hub Connection management
        //--------------------------------
        public void EnterGroup(string serialNumber)
        {
            Log.ConnectionEven("Connected : " + serialNumber);
            Groups.Add(Context.ConnectionId, serialNumber);
        }

        public void LeaveGroup(string serialNumber)
        {
            Log.ConnectionEven("Disconnected : " + serialNumber);
            Groups.Remove(Context.ConnectionId, serialNumber);
        }


        //---------------------------------------
        // Events States and Command Responses
        //---------------------------------------

        public static void Event(Event evt, string serialNumber)
        {
            MyHubContext.Clients.Group(serialNumber).EventResponse(evt);
            Log.Hub($"Event Submitted to Group :: {serialNumber}");
        }

        public static void State(StateNotification state, string serialNumber)
        {
            MyHubContext.Clients.Group(serialNumber).StateNotificationResonse(state);
            Log.Hub($"State Notification Submitted to Group :: {serialNumber}");
        }

        public static void CommandResponse(CommandResponse command, string serialNumber)
        {
            MyHubContext.Clients.Group(serialNumber).CommandResponse(command);
            Log.Hub($"Command Response Submitted to Grpuo :: {serialNumber}");
        }

        public static void NetworEvent(NetworkEvent evt, string serialNumber)
        {
            MyHubContext.Clients.Group(serialNumber).NetworkEvent(evt);
            Log.Hub($"Network Event {evt.Type} Submitted to Group :: {serialNumber}");
        }
    }
}
