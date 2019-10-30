using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class ProductInfo
    {
        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public long productId { get; set; }   //comment

        [JsonProperty("salePrice", NullValueHandling = NullValueHandling.Ignore)]
        public double salePrice { get; set; }   //comment

        [JsonProperty("productName", NullValueHandling = NullValueHandling.Ignore)]
        public string productName { get; set; }   //comment

        [JsonProperty("subTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string subTitle { get; set; }   //comment

        [JsonProperty("limitNum", NullValueHandling = NullValueHandling.Ignore)]
        public int limitNum { get; set; }   //comment

        [JsonProperty("productImageBig", NullValueHandling = NullValueHandling.Ignore)]
        public string productImageBig { get; set; }   //comment

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string detail { get; set; }   //comment

        [JsonProperty("sprice", NullValueHandling = NullValueHandling.Ignore)]
        public double sprice { get; set; }   //comment

        [JsonProperty("tprice", NullValueHandling = NullValueHandling.Ignore)]
        public double tprice { get; set; }   //comment

        [JsonProperty("lsnum", NullValueHandling = NullValueHandling.Ignore)]
        public string lsnum { get; set; }   //comment

        [JsonProperty("hsnum", NullValueHandling = NullValueHandling.Ignore)]
        public string hsnum { get; set; }   //comment

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string unit { get; set; }   //comment

        [JsonProperty("productImageSmall", NullValueHandling = NullValueHandling.Ignore)]
        public string[] productImageSmall { get; set; }   //comment

    }
}
