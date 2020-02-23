using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XMart.ViewModels;

namespace XMart.Models
{
    public class AddressInfo
    {
        [JsonProperty("addressId")]
        public long addressId { get; set; }   //Comment

        [JsonProperty("userId")]
        public long userId { get; set; }   //Comment

        [JsonProperty("userName")]
        public string userName { get; set; }   //Comment

        [JsonProperty("tel")]
        public string tel { get; set; }   //Comment

        [JsonProperty("streetName")]
        public string streetName { get; set; }   //Comment

        [JsonProperty("isDefault")]
        public bool isDefault { get; set; }   //Comment

        private Command EditCommand { get; set; }
    }
}
