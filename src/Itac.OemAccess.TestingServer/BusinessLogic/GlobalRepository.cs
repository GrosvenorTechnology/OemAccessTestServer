using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Itac.OemAccess.TestingServer.BusinessLogic
{
    public class GlobalRepository
    {
        private readonly Dictionary<string,string> _dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        public void AddGlobalConfiguration(string nameSpace, string typeName, string id, string data, string stateName = null)
        {
            var key = stateName == null ? $"{nameSpace}.{typeName}:{id}" : $"{nameSpace}.{typeName}:{id}-{stateName}";

            if (_dictionary.ContainsKey(key))
                _dictionary.Remove(key);
            _dictionary.Add(key,data);
        }

        public Dictionary<string, string> GetAllOfType(string nameSpace, string typeName)
        {
            var key = $"{nameSpace}.{typeName}:";
            return _dictionary.Where(x => x.Key.StartsWith(key)).ToDictionary(x => x.Key, x => x.Value);
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

        public void RemoveGlobalConfiguration(string nameSpace, string typeName, string id)
        {
            var key = $"{nameSpace}.{typeName}:{id}";
            _dictionary.Remove(key);
        }

    }
}
