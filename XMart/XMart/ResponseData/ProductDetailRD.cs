using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class ProductDetailRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public ProductInfo result { get; set; }   //comment

    }
}
