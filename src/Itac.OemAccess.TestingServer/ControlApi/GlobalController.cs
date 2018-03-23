using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CacheCow.Server;
using Itac.OemAccess.TestingServer.BuisnessLogic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.ControlApi
{
    [RoutePrefix("configuration")]
    public class GlobalController : ApiController
    {
        private readonly Devices _devices;
        private readonly GlobalRepository _globalRepository;
        private readonly ICachingHandler _cachingHandler;
        private readonly AreaManager _areaManager;

        public GlobalController(Devices devices, GlobalRepository globalRepository, ICachingHandler cachingHandler,AreaManager areaManager)
        {
            _devices = devices;
            _globalRepository = globalRepository;
            _cachingHandler = cachingHandler;
            _areaManager = areaManager;
        }


        [Route("devices")]
        public HttpResponseMessage PostEntityReconcile([FromBody] DeviceEntry body)
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

        [Route("entity/{nameSpace}/{typeName}/{id}")]
        public HttpResponseMessage PostEntityConfiguration(string nameSpace,string typeName, string id)
        {
            Log.Trace($"Adding Global Entity Configuration {nameSpace} :: {typeName} :: {id}");
            var data = Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _globalRepository.AddGlobalConfiguration(nameSpace,typeName,id,data);

            //Etag Cashe Update handling
            var request = new HttpRequestMessage(HttpMethod.Get,
                Url.Content($"/grosvenor-oem/configuration/{nameSpace}/{typeName}/{id}"));
            _cachingHandler.InvalidateResource(request);

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
