using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ResponseData
{
    public class SimpleRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public string result { get; set; }   //comment

    }
}
