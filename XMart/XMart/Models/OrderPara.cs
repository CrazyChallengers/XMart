using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class OrderPara
    {
        [JsonProperty("addressId")]
        public long addressId { get; set; }   //Comment

        [JsonProperty("goodsList")]
        public List<CartItemInfo> goodsList { get; set; }   //Comment

        [JsonProperty("orderTotal")]
        public long orderTotal { get; set; }   //Comment

        [JsonProperty("paymentType")]
        public int paymentType { get; set; }   //Comment

        [JsonProperty("streetName")]
        public string streetName { get; set; }   //Comment

        [JsonProperty("tel")]
        public string tel { get; set; }   //Comment

        [JsonProperty("userId")]
        public string userId { get; set; }   //Comment

        [JsonProperty("userName")]
        public string userName { get; set; }   //Comment


    }
}
