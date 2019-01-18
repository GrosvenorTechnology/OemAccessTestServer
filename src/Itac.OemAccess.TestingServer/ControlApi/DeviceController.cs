using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using CacheCow.Server;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json.Linq;

namespace Itac.OemAccess.TestingServer.ControlApi
{
    [RoutePrefix("configuration/{serialNumber}")]
    public class ConfigurationController : ApiController
    {
        private readonly Devices _devices;
        private readonly ICachingHandler _cachingHandler;

        public ConfigurationController(Devices devices, ICachingHandler cachingHandler)
        {
            _devices = devices;
            _cachingHandler = cachingHandler;
        }

        private void ResetCache(string url)
        {
            var relatedResource = new HttpRequestMessage(HttpMethod.Get, Url.Content(url));
            _cachingHandler.InvalidateResource(relatedResource);
        }

        [Route("boot")]
        [HttpGet]
        public HttpResponseMessage GetBootConfiguration(string serialNumber)
        {
            Log.ConfigUpdate($"Get Boot Config : {serialNumber}");
            var data = _devices[serialNumber].LoadBootConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("boot")]
        [HttpPost]
        public HttpResponseMessage PostBootConfiguration(string serialNumber)
        {
            Log.ConfigUpdate($"Post Boot Config : {serialNumber}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _devices[serialNumber].SaveBootConfig(data);
            ResetCache("boot");
            ResetCache($"/grosvenor-oem/device/{serialNumber}/bootconfiguration");
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("platform")]
        [HttpGet]
        public HttpResponseMessage GetPlatformConfiguration(string serialNumber)
        {
            Log.ConfigUpdate($"Get Platform Config : {serialNumber}");
            var data = _devices[serialNumber].LoadPlatformConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("platform")]
        [HttpPost]
        public HttpResponseMessage PostPlatformConfiguration(string serialNumber)
        {
            Log.ConfigUpdate($"Post Platform Config : {serialNumber}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _devices[serialNumber].SavePlatformConfig(data);
            ResetCache("platform");
            ResetCache($"/grosvenor-oem/device/{serialNumber}/platformconfiguration");
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("application")]
        [HttpGet]
        public HttpResponseMessage GetApplication(string serialNumber)
        {
            Log.ConfigUpdate($"Get Application Config : {serialNumber}");
            var data = _devices[serialNumber].LoadAppConfig();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(data, Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("application")]
        [HttpPost]
        public HttpResponseMessage PostApplication(string serialNumber)
        {
            Log.ConfigUpdate($"Post Application Config : {serialNumber}");
            var data = Request.Content.ReadAsStringAsync().Result;
            _devices[serialNumber].SaveAppConfig(data);
            ResetCache("application");
            ResetCache($"/grosvenor-oem/device/{serialNumber}/applicationconfiguration");
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("command")]
        [HttpPost]
        public HttpResponseMessage PostCommand(string serialNumber, [FromBody] Command command)
        {
            Log.ConfigUpdate($"Post Command : {serialNumber}");
            if (command.CommandName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            _devices[serialNumber].CommandRequestQueue.Put(command);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("staterequest")]
        [HttpPost]
        public HttpResponseMessage PostStateRequest(string serialNumber, [FromBody] StateRequest request)
        {
            Log.ConfigUpdate($"Post State Request : {serialNumber}");
            if (request.Entity == "" || request.StateName == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            _devices[serialNumber].StateRequestQueue.Put(request);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("change")]
        public HttpResponseMessage PostEntityReconcile(string serialNumber, [FromBody] JObject body)
        {
            Log.Trace($"Adding item to changes queue :: {serialNumber}");
            _devices[serialNumber].ChangesQueue.Put(body);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

    }
}
