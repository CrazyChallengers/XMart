using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class AddressRD : CommonRD
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<AddressInfo> result { get; set; }   //comment
    }
}
