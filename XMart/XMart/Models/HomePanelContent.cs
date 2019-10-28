using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class HomePanelContent
    {
        [JsonProperty("created")]
        public string created { get; set; }   //comment

        [JsonProperty("fullUrl")]
        public string fullUrl { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("panelId")]
        public int panelId { get; set; }   //comment

        [JsonProperty("picUrl")]
        public string picUrl { get; set; }   //comment

        [JsonProperty("picUrl2")]
        public string picUrl2 { get; set; }   //comment

        [JsonProperty("picUrl3")]
        public string picUrl3 { get; set; }   //comment

        [JsonProperty("productId")]
        public int productId { get; set; }   //comment

        [JsonProperty("productImageBig")]
        public string productImageBig { get; set; }   //comment

        [JsonProperty("productName")]
        public string productName { get; set; }   //comment

        [JsonProperty("salePrice")]
        public double salePrice { get; set; }   //comment

        [JsonProperty("sortOrder")]
        public int sortOrder { get; set; }   //comment

        [JsonProperty("subTitle")]
        public string subTitle { get; set; }   //comment

        [JsonProperty("type")]
        public int type { get; set; }   //comment

        [JsonProperty("updated")]
        public string updated { get; set; }   //comment

    }
}
