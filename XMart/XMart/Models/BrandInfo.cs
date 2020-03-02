using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class BrandInfo
    {
        [JsonProperty("bigpic", NullValueHandling = NullValueHandling.Ignore)]
        public string bigpic { get; set; }

        [JsonProperty("brandStory", NullValueHandling = NullValueHandling.Ignore)]
        public string brandStory { get; set; }   //comment

        [JsonProperty("factoryStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int factoryStatus { get; set; }   //comment

        [JsonProperty("firstLetter", NullValueHandling = NullValueHandling.Ignore)]
        public string firstLetter { get; set; }   //comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }   //comment

        [JsonProperty("logo", NullValueHandling = NullValueHandling.Ignore)]
        public string logo { get; set; }   //comment

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }   //comment

        [JsonProperty("propductCommentCount", NullValueHandling = NullValueHandling.Ignore)]
        public int propductCommentCount { get; set; }   //comment

        [JsonProperty("propductCount", NullValueHandling = NullValueHandling.Ignore)]
        public int propductCount { get; set; }   //comment

        [JsonProperty("showStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int showStatus { get; set; }   //comment

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public int sort { get; set; }   //comment

    }
}
