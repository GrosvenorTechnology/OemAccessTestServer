using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Itac.OemAccess.TestingServer.BuisnessLogic;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    [RoutePrefix("grosvenor-oem/device/{serialNumber}/changes")]
    public class ChangeQueueController : BaseQueueController<JObject>
    {
        public ChangeQueueController(Devices devices) : base(devices)
        {
        }

        protected override OemServerQueue<JObject> GetQueue(string serialNumber)
        {
            return Devices[serialNumber].ChangesQueue;
        }
    }
}
