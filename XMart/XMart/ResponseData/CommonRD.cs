using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ResponseData
{
    public class CommonRD
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int code { get; set; }
        
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public bool success { get; set; }   //comment

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long timestamp { get; set; }   //comment

    }
}
