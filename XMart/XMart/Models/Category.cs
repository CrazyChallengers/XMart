using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class Category
    {
        [JsonProperty("created")]
        public string created { get; set; }   //Comment

        [JsonProperty("icon")]
        public string icon { get; set; }   //Comment

        [JsonProperty("id")]
        public int id { get; set; }   //Comment

        [JsonProperty("isParent")]
        public bool isParent { get; set; }   //Comment

        [JsonProperty("name")]
        public string name { get; set; }   //Comment

        [JsonProperty("parentId")]
        public int parentId { get; set; }   //Comment

        [JsonProperty("remark")]
        public string remark { get; set; }   //Comment

        [JsonProperty("sortOrder")]
        public int sortOrder { get; set; }   //Comment

        [JsonProperty("status")]
        public int status { get; set; }   //Comment

        [JsonProperty("updated")]
        public string updated { get; set; }   //Comment

    }

}
