using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XMart.Models;

namespace XMart.ResponseData
{
    public class OrderDetailRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public OrderDetail result { get; set; }   //Comment

    }
}
