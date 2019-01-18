using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Itac.OemAccess.TestingServer.BusinessLogic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.OemAccessApi
{
    [RoutePrefix("grosvenor-oem/areas")]
    public class AreaController : ApiController
    {
        private AreaManager _areaManager;
        public AreaController(AreaManager areaManager)
        {
            _areaManager = areaManager;
        }
        
        [Route("requestmovement")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage RequestMovement([FromBody] AreaMovement movement)
        {
            Log.Trace($"Area Movement Request for {movement.Entity}");
            return _areaManager.GetResult(movement);
        }
    }
}
