using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class ItemAttributeValues
    {
        [JsonProperty("attributeId", NullValueHandling = NullValueHandling.Ignore)]
        public long attributeId { get; set; }   //Comment

        [JsonProperty("attributeName", NullValueHandling = NullValueHandling.Ignore)]
        public string attributeName { get; set; }   //Comment

        [JsonProperty("attributeValue", NullValueHandling = NullValueHandling.Ignore)]
        public string attributeValue { get; set; }   //Comment

        [JsonProperty("deleteflag", NullValueHandling = NullValueHandling.Ignore)]
        public long deleteflag { get; set; }   //Comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long id { get; set; }   //Comment

        [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
        public long itemId { get; set; }   //Comment

        [JsonProperty("priceAdd", NullValueHandling = NullValueHandling.Ignore)]
        public long priceAdd { get; set; }   //Comment

        [JsonProperty("mallPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long mallPrice { get; set; }   //Comment

        [JsonProperty("memberPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long memberPrice { get; set; }   //Comment

        [JsonProperty("rebate1", NullValueHandling = NullValueHandling.Ignore)]
        public long rebate1 { get; set; }   //Comment

        [JsonProperty("rebate2", NullValueHandling = NullValueHandling.Ignore)]
        public long rebate2 { get; set; }   //Comment

        [JsonProperty("buyPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long buyPrice { get; set; }   //Comment

    }
}
