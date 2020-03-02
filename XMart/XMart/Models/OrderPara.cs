using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class OrderPara
    {
        [JsonProperty("addressId", NullValueHandling = NullValueHandling.Ignore)]
        public long addressId { get; set; }   //Comment

        [JsonProperty("goodsList", NullValueHandling = NullValueHandling.Ignore)]
        public List<CartItemInfo> goodsList { get; set; }   //Comment

        [JsonProperty("orderTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long orderTotal { get; set; }   //Comment

        [JsonProperty("paymentType", NullValueHandling = NullValueHandling.Ignore)]
        public int paymentType { get; set; }   //Comment

        [JsonProperty("streetName", NullValueHandling = NullValueHandling.Ignore)]
        public string streetName { get; set; }   //Comment

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }   //Comment

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string userId { get; set; }   //Comment

        [JsonProperty("userName", NullValueHandling = NullValueHandling.Ignore)]
        public string userName { get; set; }   //Comment


    }
}
