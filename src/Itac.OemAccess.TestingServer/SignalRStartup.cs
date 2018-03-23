using Microsoft.AspNet.SignalR;
using Owin;

namespace Itac.OemAccess.TestingServer
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR(hubConfiguration);
        }
    }
}
