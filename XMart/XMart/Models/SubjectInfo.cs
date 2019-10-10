using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class SubjectInfo
    {
        [JsonProperty("albumPics")]
        public string albumPics { get; set; }   //comment

        [JsonProperty("categoryId")]
        public int categoryId { get; set; }   //comment

        [JsonProperty("categoryName")]
        public string categoryName { get; set; }   //comment

        [JsonProperty("collectCount")]
        public int collectCount { get; set; }   //comment

        [JsonProperty("commentCount")]
        public int commentCount { get; set; }   //comment

        [JsonProperty("content")]
        public string content { get; set; }   //comment

        [JsonProperty("createTime")]
        public string createTime { get; set; }   //comment

        [JsonProperty("description")]
        public string description { get; set; }   //comment

        [JsonProperty("forwardCount")]
        public int forwardCount { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("pic")]
        public string pic { get; set; }   //comment

        [JsonProperty("productCount")]
        public int productCount { get; set; }   //comment

        [JsonProperty("readCount")]
        public int readCount { get; set; }   //comment

        [JsonProperty("recommendStatus")]
        public int recommendStatus { get; set; }   //comment

        [JsonProperty("showStatus")]
        public int showStatus { get; set; }   //comment

        [JsonProperty("title")]
        public string title { get; set; }   //comment
        
    }
}
