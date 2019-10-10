using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class AdvertiseInfo
    {
        [JsonProperty("clickCount")]
        public int clickCount { get; set; }

        [JsonProperty("endTime")]
        public string endTime { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("note")]
        public string note { get; set; }

        [JsonProperty("orderCount")]
        public int orderCount { get; set; }

        [JsonProperty("pic")]
        public string pic { get; set; }

        [JsonProperty("sort")]
        public int sort { get; set; }

        [JsonProperty("startTime")]
        public string startTime { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("type")]
        public int type { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }
    }
}
