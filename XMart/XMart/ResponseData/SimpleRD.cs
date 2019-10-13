using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ResponseData
{
    public class SimpleRD : CommonRD
    {
        [JsonProperty("data")]
        public string data { get; set; }   //comment

    }
}
