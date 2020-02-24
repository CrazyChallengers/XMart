using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ResponseData
{
    public class StupidRD : CommonRD
    {
        [JsonProperty("result")]
        public long result { get; set; }   //Comment

    }
}
