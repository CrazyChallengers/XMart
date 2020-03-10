using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XMart.Models;

namespace XMart.ResponseData
{
    public class OrderListRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public OrderListRDResult result { get; set; }   //Comment

    }
}
