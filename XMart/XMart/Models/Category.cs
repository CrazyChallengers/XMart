using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class Category
    {
        [JsonProperty("categories")]
        public List<SubCategoryInfo> categories { get; set; }   //comment

        [JsonProperty("parents")]
        public List<ParentCategoryInfo> parents { get; set; }   //comment

    }

    public class ParentCategoryInfo
    {
        [JsonProperty("description")]
        public string description { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("name")]
        public string name { get; set; }   //comment

    }

    public class SubCategoryInfo
    {
        [JsonProperty("icon")]
        public string icon { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("name")]
        public string name { get; set; }   //comment

        [JsonProperty("parentId")]
        public int parentId { get; set; }   //comment

        [JsonProperty("unit")]
        public string unit { get; set; }   //comment

    }
}
