using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class OrderDetail
    {
        [JsonProperty("addressInfo")]
        public AddressInfo addressInfo { get; set; }   //Comment

        [JsonProperty("buyerMessage")]
        public string buyerMessage { get; set; }   //Comment

        [JsonProperty("closeDate")]
        public string closeDate { get; set; }   //Comment

        [JsonProperty("contract")]
        public string contract { get; set; }   //Comment

        [JsonProperty("createDate")]
        public string createDate { get; set; }   //Comment

        [JsonProperty("deleteflag")]
        public int deleteflag { get; set; }   //Comment

        [JsonProperty("deliveryStatus")]
        public int deliveryStatus { get; set; }   //Comment

        [JsonProperty("finishDate")]
        public string finishDate { get; set; }   //Comment

        [JsonProperty("goodsList")]
        public List<CartItemInfo> goodsList { get; set; }   //Comment

        [JsonProperty("orderId")]
        public long orderId { get; set; }   //Comment

        [JsonProperty("orderStatus")]
        public string orderStatus { get; set; }   //Comment

        [JsonProperty("orderTotal")]
        public double orderTotal { get; set; }   //Comment

        [JsonProperty("payDate")]
        public string payDate { get; set; }   //Comment

        [JsonProperty("paymentType")]
        public int paymentType { get; set; }   //Comment


    }
}
