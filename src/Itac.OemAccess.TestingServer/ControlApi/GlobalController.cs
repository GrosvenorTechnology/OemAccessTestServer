using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using CacheCow.Server;
using GT.OemAccess.Configuration;
using GT.OemAccess.Configuration.Application;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Itac.OemAccess.TestingServer.ControlApi
{
    [RoutePrefix("configuration")]
    public class GlobalController : ApiController
    {
        private readonly Devices _devices;
        private readonly GlobalRepository _globalRepository;
        private readonly ICachingHandler _cachingHandler;
        private readonly AreaManager _areaManager;

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public GlobalController(Devices devices, GlobalRepository globalRepository, ICachingHandler cachingHandler, AreaManager areaManager)
        {
            _devices = devices;
            _globalRepository = globalRepository;
            _cachingHandler = cachingHandler;
            _areaManager = areaManager;
        }

        [Route("devices")]
        [HttpGet]
        public HttpResponseMessage GetDevices()
        {
            Log.ConfigUpdate("Get All Controllers");
            var data = _devices.GetAll().Select(x =>
            {
                var bc = x.LoadBootConfig();
                var ac = x.LoadAppConfig();
                return new ControllerContainer
                {
                    Id = Guid.NewGuid().ToString(),
                    SerialNumber = x.SerialNumber,
                    BootConfiguration = (bc == null) ? null : JsonConvert.DeserializeObject<BootConfiguration>(bc),
                    ApplicationConfiguration = (ac == null) ? null : JsonConvert.DeserializeObject<ApplicationConfiguration>(ac),
                };
            });
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(data, JsonSettings), Encoding.UTF8, JsonMediaTypeFormatter.DefaultMediaType.MediaType);
            return response;
        }

        [Route("devices")]
        public HttpResponseMessage PostEntityCreate([FromBody] DeviceEntry body)
        {
            Log.Trace($"Adding Device :: {body.SerialNumber}");
            _devices.AddNew(body.SerialNumber, body.SharedKey);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("areas/adduser")]
        [HttpPost]
        public HttpResponseMessage AddUser([FromBody] List<string> userList)
        {
            Log.Trace("Add Allowed User ");
            _areaManager.AddAllowedUser(userList);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [Route("areas/removeuser")]
        [HttpPost]
        public HttpResponseMessage RemoveUser([FromBody] List<string> userList)
        {
            Log.Trace("Remove Allowed User");
            _areaManager.RemoveAllowedUser(userList);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("entity/{nameSpace}/{typeName}")]
        public HttpResponseMessage GetEntityConfiguration(string nameSpace, string typeName)
        {
            Log.Trace($"Getting Global Entity Configuration {nameSpace} :: {typeName}");
            
            var array = new JArray();

            foreach (var item in _globalRepository.GetAllOfType(nameSpace, typeName).Values)
            {
                var obj = JObject.Parse(item);
                array.Add(obj);
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(array.ToString(), Encoding.UTF8, "application/json")
            };
        }

        [Route("entity/{nameSpace}/{typeName}/{id}")]
        public HttpResponseMessage PostEntityConfiguration(string nameSpace,string typeName, string id)
        {
            Log.Trace($"Adding Global Entity Configuration {nameSpace} :: {typeName} :: {id}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _globalRepository.AddGlobalConfiguration(nameSpace,typeName,id,data);

            //Etag Cache Update handling
            var request = new HttpRequestMessage(HttpMethod.Get,
                Url.Content($"/grosvenor-oem/configuration/{nameSpace}/{typeName}/{id}"));
            _cachingHandler.InvalidateResource(request);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [Route("entity/{nameSpace}/{typeName}/{id}")]
        public HttpResponseMessage DeleteEntityConfiguration(string nameSpace, string typeName, string id)
        {
            Log.Trace($"Removing Global Entity Configuration {nameSpace} :: {typeName} :: {id}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _globalRepository.RemoveGlobalConfiguration(nameSpace, typeName, id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [Route("entity/{nameSpace}/{typeName}/{id}/states/{stateName}")]
        public HttpResponseMessage PostEntityStateRequest(string nameSpace, string typeName, string id,string stateName)
        {
            Log.Trace($"Adding Global Entity Configuration {nameSpace} :: {typeName} :: {id}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _globalRepository.AddGlobalConfiguration(nameSpace, typeName, id, data,stateName);
            return new HttpResponseMessage(HttpStatusCode.NoContent);

        }

    }
}
