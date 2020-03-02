using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class AdvertiseInfo
    {
        [JsonProperty("clickCount", NullValueHandling = NullValueHandling.Ignore)]
        public int clickCount { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public string endTime { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string note { get; set; }

        [JsonProperty("orderCount", NullValueHandling = NullValueHandling.Ignore)]
        public int orderCount { get; set; }

        [JsonProperty("pic", NullValueHandling = NullValueHandling.Ignore)]
        public string pic { get; set; }

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public int sort { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public string startTime { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int status { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int type { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
    }
}
