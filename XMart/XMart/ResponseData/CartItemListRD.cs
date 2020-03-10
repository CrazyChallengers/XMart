using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class CartItemListRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<CartItemInfo> result { get; set; }   //comment
    }
}
