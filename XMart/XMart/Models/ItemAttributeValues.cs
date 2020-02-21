using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class ItemAttributeValues
    {
        [JsonProperty("attributeId", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 attributeId { get; set; }   //Comment

        [JsonProperty("attributeName", NullValueHandling = NullValueHandling.Ignore)]
        public string attributeName { get; set; }   //Comment

        [JsonProperty("attributeValue", NullValueHandling = NullValueHandling.Ignore)]
        public string attributeValue { get; set; }   //Comment

        [JsonProperty("deleteflag", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 deleteflag { get; set; }   //Comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 id { get; set; }   //Comment

        [JsonProperty("itemId", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 itemId { get; set; }   //Comment

        [JsonProperty("priceAdd", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 priceAdd { get; set; }   //Comment
    }
}
