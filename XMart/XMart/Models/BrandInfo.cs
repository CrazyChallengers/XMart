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
        public string brandStory;   //comment

        [JsonProperty("factoryStatus")]
        public int factoryStatus;   //comment

        [JsonProperty("firstLetter")]
        public string firstLetter;   //comment

        [JsonProperty("id")]
        public int id;   //comment

        [JsonProperty("logo")]
        public string logo;   //comment

        [JsonProperty("name")]
        public string name;   //comment

        [JsonProperty("propductCommentCount")]
        public int propductCommentCount;   //comment

        [JsonProperty("propductCount")]
        public int propductCount;   //comment

        [JsonProperty("showStatus")]
        public int showStatus;   //comment

        [JsonProperty("sort")]
        public int sort;   //comment
        
    }
}
