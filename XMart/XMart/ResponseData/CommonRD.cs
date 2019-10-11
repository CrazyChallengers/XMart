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
    }
}
