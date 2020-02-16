using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class ProductListRD : CommonRD
    {
        [JsonProperty("result")]
        public ProductListRDResult result { get; set; }   //Comment

    }
}
