using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.ControlApi;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json.Linq;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{    
    [RoutePrefix("grosvenor-oem/device/{serialNumber}")]
    [Authorize]
    public class DeviceController : ApiController
    {
        private readonly Devices _devices;

        public DeviceController(Devices devices)
        {
            _devices = devices;
        }

        [Route("bootconfiguration")]
        [HttpGet]
        public HttpResponseMessage GetBootConfiguration(string serialNumber)
        {
            Log.Trace($"Get Boot Configuration {serialNumber}");
            var data = _devices[serialNumber].LoadBootConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("platformconfiguration")]
        [HttpGet]
        public HttpResponseMessage GetPlatformConfiguration(string serialNumber)
        {
            Log.Trace($"Get Platform Configuration {serialNumber}");
            var data = _devices[serialNumber].LoadPlatformConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("applicationconfiguration")]
        [HttpGet]
        public HttpResponseMessage GetApplicationConfiguration(string serialNumber)
        {
            Log.Trace($"Get Application Configuration {serialNumber}");
            var data = _devices[serialNumber].LoadAppConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("{messageType}")]
        [HttpGet]
        public HttpResponseMessage Get(string serialNumber, string messageType)
        {
            Log.Error($"Bad Request {messageType}");
            return Request.CreateResponse(404);
        }

        [Route("heartbeat")]
        [HttpPost]
        public JsonResult<HeartbeatResponse> PostHeartbeat(string serialNumber, [FromBody] string value)
        {
            var activities = _devices[serialNumber].GetActivities();
            Log.Trace($"Heartbeat {serialNumber} :: Activities [{string.Join(",", activities)}]");
            return Json(new HeartbeatResponse(){ Activity = _devices[serialNumber].GetActivities()});
        }

        [Route("hardware")]
        [HttpPost]
        public HttpResponseMessage PostHardware(string serialNumber, [FromBody] JObject value)
        {
            Log.Error("NOT IMPLEMENTED POST REQUEST");
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("events")]
        [HttpPost]
        public HttpResponseMessage PostEvents(string serialNumber, [FromBody] Event value)
        {
            Log.Trace(value);
            OemServerHub.Event(value, serialNumber);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("states")]
        [HttpPost]
        public HttpResponseMessage PostStates(string serialNumber, [FromBody] StateNotification value)
        {
            Log.Trace(value);
            OemServerHub.State(value, serialNumber);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("commands")]
        [HttpPost]
        public HttpResponseMessage PostCommands(string serialNumber, [FromBody] CommandResponse value)
        {
            Log.Trace(value);
            OemServerHub.CommandResponse(value, serialNumber);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
