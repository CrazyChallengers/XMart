using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class Category
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string created { get; set; }   //Comment

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string icon { get; set; }   //Comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }   //Comment

        [JsonProperty("isParent", NullValueHandling = NullValueHandling.Ignore)]
        public bool isParent { get; set; }   //Comment

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }   //Comment

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public int parentId { get; set; }   //Comment

        [JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore)]
        public string remark { get; set; }   //Comment

        [JsonProperty("sortOrder", NullValueHandling = NullValueHandling.Ignore)]
        public int sortOrder { get; set; }   //Comment

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int status { get; set; }   //Comment

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public string updated { get; set; }   //Comment

    }

}
