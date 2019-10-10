using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class SubjectInfo
    {
        [JsonProperty("albumPics")]
        public string albumPics;   //comment

        [JsonProperty("categoryId")]
        public int categoryId;   //comment

        [JsonProperty("categoryName")]
        public string categoryName;   //comment

        [JsonProperty("collectCount")]
        public int collectCount;   //comment

        [JsonProperty("commentCount")]
        public int commentCount;   //comment

        [JsonProperty("content")]
        public string content;   //comment

        [JsonProperty("createTime")]
        public string createTime;   //comment

        [JsonProperty("description")]
        public string description;   //comment

        [JsonProperty("forwardCount")]
        public int forwardCount;   //comment

        [JsonProperty("id")]
        public int id;   //comment

        [JsonProperty("pic")]
        public string pic;   //comment

        [JsonProperty("productCount")]
        public int productCount;   //comment

        [JsonProperty("readCount")]
        public int readCount;   //comment

        [JsonProperty("recommendStatus")]
        public int recommendStatus;   //comment

        [JsonProperty("showStatus")]
        public int showStatus;   //comment

        [JsonProperty("title")]
        public string title;   //comment
        
    }
}
