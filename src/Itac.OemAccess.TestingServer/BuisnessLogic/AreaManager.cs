using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class AreaManager
    {
        private struct ResultStructure
        {
            public string Result;
        }
        private static List<string> _allowedUsers = new List<string>();

        public HttpResponseMessage GetResult(AreaMovement movementRequest)
        {
            if (string.IsNullOrEmpty(movementRequest.Entity) || movementRequest.From.Count != 0 || movementRequest.To.Count != 0)
            {
                Log.Error("Bad Request");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var resultStructure = _allowedUsers.Contains(movementRequest.Entity) ? new ResultStructure{Result = "allowed"} : new ResultStructure{Result = "denied"};
            var some =  new StringContent(JsonConvert.SerializeObject(resultStructure), Encoding.UTF8, "application/json");
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = some};
        }
        

        public void AddAllowedUser(List<string> userList)
        {
            foreach (var user in userList)
            {
                if (!_allowedUsers.Contains(user))
                    _allowedUsers.Add(user);
            }
        }

        public void RemoveAllowedUser(List<string> userList)
        {
            foreach (var user in userList)
            {
                _allowedUsers.Remove(user);
            }
        }
    }
}
