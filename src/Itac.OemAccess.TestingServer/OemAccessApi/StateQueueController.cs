using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Itac.OemAccess.TestingServer.BuisnessLogic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    [RoutePrefix("grosvenor-oem/device/{serialNumber}/states")]
    public class StateQueueController : BaseQueueController<StateRequest>
    {
        public StateQueueController(Devices devices) : base(devices)
        {
        }

        protected override OemServerQueue<StateRequest> GetQueue(string serialNumber)
        {
            return Devices[serialNumber].StateRequestQueue;
        }
    }
}
