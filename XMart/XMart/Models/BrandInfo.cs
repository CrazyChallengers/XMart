using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class BrandInfo
    {
        [JsonProperty("bigpic")]
        public string bigpic { get; set; }

        [JsonProperty("brandStory")]
        public string brandStory { get; set; }   //comment

        [JsonProperty("factoryStatus")]
        public int factoryStatus { get; set; }   //comment

        [JsonProperty("firstLetter")]
        public string firstLetter { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("logo")]
        public string logo { get; set; }   //comment

        [JsonProperty("name")]
        public string name { get; set; }   //comment

        [JsonProperty("propductCommentCount")]
        public int propductCommentCount { get; set; }   //comment

        [JsonProperty("propductCount")]
        public int propductCount { get; set; }   //comment

        [JsonProperty("showStatus")]
        public int showStatus { get; set; }   //comment

        [JsonProperty("sort")]
        public int sort { get; set; }   //comment

    }
}
