using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Domain.Infrastructure.PortAdapters
{
    public class RestAdapter : IAdapter
    {
        private readonly RestClient _client;

        public RestAdapter(Uri baseUri)
        {
            _client = new RestClient(baseUri);
        }

        public T Get<T>(string path)
        {
            if(string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            var request = new RestRequest($"{path}", Method.GET);
            var response = _client.Execute(request).Content;

            return JsonConvert.DeserializeObject<T>(response,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }

        public async Task<T> GetAsync<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            var request = new RestRequest($"{path}", Method.GET);
            var response = _client.Execute(request).Content;

            return JsonConvert.DeserializeObject<T>(response,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
