using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    [RoutePrefix("grosvenor-oem/device/{serialNumber}/commands")]
    public class CommandQueueController : BaseQueueController<Command>
    {
        public CommandQueueController(Devices devices) : base(devices)
        {
        }

        protected override OemServerQueue<Command> GetQueue(string serialNumber)
        {
            return Devices[serialNumber].CommandRequestQueue;
        }
    }
}
