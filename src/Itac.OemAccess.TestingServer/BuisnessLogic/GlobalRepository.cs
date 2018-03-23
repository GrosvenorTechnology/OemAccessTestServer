using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class GlobalRepository
    {
        private readonly Dictionary<string,string> _dictionary = new Dictionary<string, string>();
        
        public void AddGlobalConfiguration(string nameSpace, string typeName, string id, string data, string stateName = null)
        {
            var key = stateName == null ? $"{nameSpace}.{typeName}:{id}" : $"{nameSpace}.{typeName}:{id}-{stateName}";

            if (_dictionary.ContainsKey(key))
                _dictionary.Remove(key);
            _dictionary.Add(key,data);
        }

        public HttpResponseMessage GetGlobalConfiguration(string nameSpace, string typeName, string id, string stateName = null)
        {
            var key = stateName == null ? $"{nameSpace}.{typeName}:{id}" : $"{nameSpace}.{typeName}:{id}-{stateName}";
            
            if (!_dictionary.TryGetValue(key,out var value))
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            return new HttpResponseMessage(HttpStatusCode.OK)
            { 
                Content = new StringContent(value, Encoding.UTF8, "application/json")
            };
        }

    }
}
