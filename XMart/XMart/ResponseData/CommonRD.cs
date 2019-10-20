using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ResponseData
{
    public class CommonRD
    {
        [JsonProperty("code")]
        public int code { get; set; }
        
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("success")]
        public bool success { get; set; }   //comment

        [JsonProperty("timestamp")]
        public Int64 timestamp { get; set; }   //comment

    }
}
