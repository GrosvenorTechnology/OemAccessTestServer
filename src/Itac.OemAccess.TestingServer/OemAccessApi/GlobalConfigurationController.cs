using System.Net.Http;
using System.Web.Http;
using Itac.OemAccess.TestingServer.BusinessLogic;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    [RoutePrefix("grosvenor-oem/configuration/{nameSpace}/{typeName}")]
    [Authorize]
    public class GlobalConfigurationController : ApiController
    {
        private readonly GlobalRepository _globalRepository;
        public GlobalConfigurationController(GlobalRepository globalRepository)
        {
            _globalRepository = globalRepository;
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetEntityConfiguration(string nameSpace, string typeName, string id)
        {
            Log.Trace($"Get Entity Configuration {nameSpace} :: {typeName} :: {id}");
            return _globalRepository.GetGlobalConfiguration(nameSpace,typeName,id);
        }

        [Route("{id}/states/{stateName}")]
        [HttpGet]
        public HttpResponseMessage GetEntityState(string nameSpace, string typeName, string id, string stateName)
        {
            Log.Trace($"Get Entity State {nameSpace} :: {typeName} :: {id} :: {stateName}");
            return _globalRepository.GetGlobalConfiguration(nameSpace, typeName, id,stateName);
        }
    }
}