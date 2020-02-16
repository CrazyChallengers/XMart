using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class ProductListRDResult
    {
        [JsonProperty("total")]
        public int total { get; set; }   //Comment

        [JsonProperty("data")]
        public List<ProductListItem> data { get; set; }   //Comment

    }
}
