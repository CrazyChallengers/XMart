using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class OrderDetail
    {
        [JsonProperty("addressInfo", NullValueHandling = NullValueHandling.Ignore)]
        public AddressInfo addressInfo { get; set; }   //Comment

        [JsonProperty("buyerMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string buyerMessage { get; set; }   //Comment

        [JsonProperty("closeDate", NullValueHandling = NullValueHandling.Ignore)]
        public string closeDate { get; set; }   //Comment

        [JsonProperty("contract", NullValueHandling = NullValueHandling.Ignore)]
        public string contract { get; set; }   //Comment

        [JsonProperty("createDate", NullValueHandling = NullValueHandling.Ignore)]
        public string createDate { get; set; }   //Comment

        [JsonProperty("deleteflag", NullValueHandling = NullValueHandling.Ignore)]
        public int deleteflag { get; set; }   //Comment

        [JsonProperty("deliveryStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int deliveryStatus { get; set; }   //Comment

        [JsonProperty("finishDate", NullValueHandling = NullValueHandling.Ignore)]
        public string finishDate { get; set; }   //Comment

        [JsonProperty("goodsList", NullValueHandling = NullValueHandling.Ignore)]
        public List<CartItemInfo> goodsList { get; set; }   //Comment

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public long orderId { get; set; }   //Comment

        [JsonProperty("orderStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string orderStatus { get; set; }   //Comment

        [JsonProperty("orderTotal", NullValueHandling = NullValueHandling.Ignore)]
        public double orderTotal { get; set; }   //Comment

        [JsonProperty("payDate", NullValueHandling = NullValueHandling.Ignore)]
        public string payDate { get; set; }   //Comment

        [JsonProperty("paymentType", NullValueHandling = NullValueHandling.Ignore)]
        public int paymentType { get; set; }   //Comment


    }
}
