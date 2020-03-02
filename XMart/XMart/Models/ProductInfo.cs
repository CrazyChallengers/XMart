using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class ProductInfo
    {
        [JsonProperty("buyPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long buyPrice { get; set; }   //进货价

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string color { get; set; }   //comment

        [JsonProperty("companyName", NullValueHandling = NullValueHandling.Ignore)]
        public string companyName { get; set; }   //comment

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string country { get; set; }   //comment

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string detail { get; set; }   //comment

        [JsonProperty("hsnum", NullValueHandling = NullValueHandling.Ignore)]
        public long hsnum { get; set; }   //comment

        [JsonProperty("itemAttributeValues", NullValueHandling = NullValueHandling.Ignore)]
        public List<ItemAttributeValues> itemAttributeValues { get; set; }   //comment

        [JsonProperty("itemNo", NullValueHandling = NullValueHandling.Ignore)]
        public string itemNo { get; set; }   //comment

        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public string level { get; set; }   //comment

        [JsonProperty("limitNum", NullValueHandling = NullValueHandling.Ignore)]
        public long limitNum { get; set; }   //comment

        [JsonProperty("look", NullValueHandling = NullValueHandling.Ignore)]
        public string look { get; set; }   //comment

        [JsonProperty("lsnum", NullValueHandling = NullValueHandling.Ignore)]
        public long lsnum { get; set; }   //comment

        [JsonProperty("mainMaterial", NullValueHandling = NullValueHandling.Ignore)]
        public string mainMaterial { get; set; }   //comment

        [JsonProperty("mallPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long mallPrice { get; set; }   //市场价

        [JsonProperty("memberPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long memberPrice { get; set; }   //会员价

        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public long productId { get; set; }   //comment

        [JsonProperty("productImageBig", NullValueHandling = NullValueHandling.Ignore)]
        public string productImageBig { get; set; }   //comment

        [JsonProperty("productImageSmall", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> productImageSmall { get; set; }   //comment

        [JsonProperty("productName", NullValueHandling = NullValueHandling.Ignore)]
        public string productName { get; set; }   //comment

        [JsonProperty("rebate1", NullValueHandling = NullValueHandling.Ignore)]
        public long rebate1 { get; set; }   //comment

        [JsonProperty("rebate2", NullValueHandling = NullValueHandling.Ignore)]
        public long rebate2 { get; set; }   //comment

        [JsonProperty("salePrice", NullValueHandling = NullValueHandling.Ignore)]
        public long salePrice { get; set; }   //comment

        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public string series { get; set; }   //comment

        //[JsonProperty("sprice", NullValueHandling = NullValueHandling.Ignore)]
        //public long sprice { get; set; }   //comment

        [JsonProperty("standard", NullValueHandling = NullValueHandling.Ignore)]
        public string standard { get; set; }   //comment

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public string style { get; set; }   //comment

        [JsonProperty("subTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string subTitle { get; set; }   //comment

        [JsonProperty("technique", NullValueHandling = NullValueHandling.Ignore)]
        public string technique { get; set; }   //comment

        //[JsonProperty("tprice", NullValueHandling = NullValueHandling.Ignore)]
        //public long tprice { get; set; }   //comment

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }   //comment

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string unit { get; set; }   //comment

        [JsonProperty("warpBluk", NullValueHandling = NullValueHandling.Ignore)]
        public string warpBluk { get; set; }   //comment

        [JsonProperty("warpStandard", NullValueHandling = NullValueHandling.Ignore)]
        public string warpStandard { get; set; }   //comment

        [JsonProperty("waterRate", NullValueHandling = NullValueHandling.Ignore)]
        public string waterRate { get; set; }   //comment

        [JsonProperty("wormHoles", NullValueHandling = NullValueHandling.Ignore)]
        public string wormHoles { get; set; }   //comment

    }
}
