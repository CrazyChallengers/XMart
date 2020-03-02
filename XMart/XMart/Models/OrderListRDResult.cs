using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class OrderListRDResult
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int total { get; set; }   //Comment

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderDetail> data { get; set; }   //Comment

    }
}
