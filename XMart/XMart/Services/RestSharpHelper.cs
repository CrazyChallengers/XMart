using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XMart.Services
{
    public static class RestSharpHelper<T>
    {
        static RestClient _restClient = new RestClient("http://120.26.3.153:7777");

        public static async Task<T> PostAsync(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse<T> responsePost = await _restClient.ExecuteAsync<T>(requestPost);
            T t = JsonConvert.DeserializeObject<T>(responsePost.Content);
            return t;
        }

        public static async Task<T> GetAsync(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse<T> responseGet = await _restClient.ExecuteAsync<T>(requestGet);
            T t = JsonConvert.DeserializeObject<T>(responseGet.Content);
            return t;
        }

        public static T Get(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = _restClient.Execute(requestGet);
            T t = JsonConvert.DeserializeObject<T>(responseGet.Content);
            return t;
        }

        public static T Post(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse<T> responsePost = _restClient.Execute<T>(requestPost);
            T t = JsonConvert.DeserializeObject<T>(responsePost.Content);
            return t;
        }

        public static async Task<string> GetAsyncWithoutDeserialization(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = await _restClient.ExecuteAsync(requestGet);
            return responseGet.Content; 
        }

        public static async Task<string> PostAsyncWithoutDeserialization(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse responsePost = await _restClient.ExecuteAsync(requestPost);
            return responsePost.Content;
        }
    }
}
