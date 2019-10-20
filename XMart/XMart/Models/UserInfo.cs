using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class UserInfo
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }   //comment

        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string username { get; set; }   //comment

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string address { get; set; }   //comment

        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public int balance { get; set; }   //comment

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }   //comment

        [JsonProperty("eamil", NullValueHandling = NullValueHandling.Ignore)]
        public string eamil { get; set; }   //comment

        [JsonProperty("file", NullValueHandling = NullValueHandling.Ignore)]
        public string file { get; set; }   //comment

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }   //comment

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string phone { get; set; }   //comment

        [JsonProperty("points", NullValueHandling = NullValueHandling.Ignore)]
        public int points { get; set; }   //comment

        [JsonProperty("sex", NullValueHandling = NullValueHandling.Ignore)]
        public string sex { get; set; }   //comment

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public int state { get; set; }   //comment

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string token { get; set; }   //comment

    }
}
