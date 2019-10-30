using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class ProductDetailRD : CommonRD
    {
        [JsonProperty("result")]
        public ProductInfo result { get; set; }   //comment

    }
}
