using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class SubjectInfo
    {
        [JsonProperty("albumPics", NullValueHandling = NullValueHandling.Ignore)]
        public string albumPics { get; set; }   //comment

        [JsonProperty("categoryId", NullValueHandling = NullValueHandling.Ignore)]
        public int categoryId { get; set; }   //comment

        [JsonProperty("categoryName", NullValueHandling = NullValueHandling.Ignore)]
        public string categoryName { get; set; }   //comment

        [JsonProperty("collectCount", NullValueHandling = NullValueHandling.Ignore)]
        public int collectCount { get; set; }   //comment

        [JsonProperty("commentCount", NullValueHandling = NullValueHandling.Ignore)]
        public int commentCount { get; set; }   //comment

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string content { get; set; }   //comment

        [JsonProperty("createTime", NullValueHandling = NullValueHandling.Ignore)]
        public string createTime { get; set; }   //comment

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }   //comment

        [JsonProperty("forwardCount", NullValueHandling = NullValueHandling.Ignore)]
        public int forwardCount { get; set; }   //comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }   //comment

        [JsonProperty("pic", NullValueHandling = NullValueHandling.Ignore)]
        public string pic { get; set; }   //comment

        [JsonProperty("productCount", NullValueHandling = NullValueHandling.Ignore)]
        public int productCount { get; set; }   //comment

        [JsonProperty("readCount", NullValueHandling = NullValueHandling.Ignore)]
        public int readCount { get; set; }   //comment

        [JsonProperty("recommendStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int recommendStatus { get; set; }   //comment

        [JsonProperty("showStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int showStatus { get; set; }   //comment

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }   //comment
        
    }
}
