using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class OrderListRDResult
    {
        [JsonProperty("total")]
        public int total { get; set; }   //Comment

        [JsonProperty("data")]
        public List<OrderDetail> data { get; set; }   //Comment

    }
}
