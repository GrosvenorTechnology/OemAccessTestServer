using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    public abstract class BaseQueueController<T> : ApiController
    {
        protected readonly Devices Devices;

        protected BaseQueueController(Devices devices)
        {
            Devices = devices;
        }

        protected abstract OemServerQueue<T> GetQueue(string serialNumber);

        [Route("messages/head")]
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteStateMessages(string serialNumber, [FromBody]List<Guid> guidList, int count = 1)
        {
            if (count < 1)
                return Request.CreateResponse(400);

            var data = GetQueue(serialNumber).DestructiveGet(count);
            Log.Trace($"Returning {data.Count} items from {this.GetType().Name} :: {serialNumber}");
            
            return Request.CreateResponse(data);
        }
    }
}
