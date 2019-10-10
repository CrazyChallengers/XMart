using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XMart.ResponseData;
using Newtonsoft.Json;

namespace XMart.Services
{
    public class RestService
    {
        HttpClient _client = new HttpClient();
        string rootUrl = "http://118.24.96.42:8082";    //测试

        public RestService()
        {

        }
        
        public async Task<HomeContentRD> GetHomeContent()
        {
            string url = rootUrl + "/home/content";

            string responseBody = await GetStringDataAsync(url);

            HomeContentRD homeContentRD = JsonConvert.DeserializeObject<HomeContentRD>(responseBody);

            return homeContentRD;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 截取字符串，处理网站返回值
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        private string GetSubString(string content, string startStr, string endStr)
        {
            int startIndex = content.IndexOf(startStr);
            //int endIndex = content.LastIndexOf(endStr);
            string str = "";

            str = content.Substring(startIndex, content.Length - startIndex);
            str = str.Substring(0, str.LastIndexOf(endStr) + 1);

            return str;
        }

        /// <summary>
        /// Get方法
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<string> GetDataAsync(string uri)
        {
            string responseData = "";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                    //responseData = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Get string方法
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<string> GetStringDataAsync(string uri)
        {
            string responseData = "";
            try
            {
                string response = await _client.GetStringAsync(uri);
                responseData = GetSubString(response, "{", "}");
                //responseData = JsonConvert.DeserializeObject<string>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private async Task<string> PostAsync(string uri, string httpContent)
        {
            string responseData = "";
            var _httpContent = new StringContent(httpContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                    //responseData = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Post Form表单的方法
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private async Task<string> PostFormAsync(string uri, string httpContent)
        {
            string responseData = "";
            var _httpContent = new StringContent(httpContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {0}", ex.Message);
            }

            return responseData;
        }
    }
}
