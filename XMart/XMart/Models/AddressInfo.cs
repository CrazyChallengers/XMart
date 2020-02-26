using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XMart.ViewModels;

namespace XMart.Models
{
    public class AddressInfo
    {
        [JsonProperty("addressId", NullValueHandling = NullValueHandling.Ignore)]
        public long addressId { get; set; }   //Comment

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public long userId { get; set; }   //Comment

        [JsonProperty("userName", NullValueHandling = NullValueHandling.Ignore)]
        public string userName { get; set; }   //Comment

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }   //Comment

        [JsonProperty("streetName", NullValueHandling = NullValueHandling.Ignore)]
        public string streetName { get; set; }   //Comment

        [JsonProperty("isDefault", NullValueHandling = NullValueHandling.Ignore)]
        public bool isDefault { get; set; }   //Comment

    }
}
