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

        [JsonProperty("userType", NullValueHandling = NullValueHandling.Ignore)]
        public string userType { get; set; }   //comment

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string address { get; set; }   //comment

        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public int balance { get; set; }   //comment

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }   //comment

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string email { get; set; }   //comment

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
        
        [JsonProperty("rebateTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long rebateTotal { get; set; }   //comment

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public long created { get; set; }   //Comment

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public long updated { get; set; }   //Comment

        [JsonProperty("buyCompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string buyCompanyName { get; set; }   //Comment

        [JsonProperty("personName", NullValueHandling = NullValueHandling.Ignore)]
        public string personName { get; set; }   //Comment

        [JsonProperty("pricture", NullValueHandling = NullValueHandling.Ignore)]
        public string pricture { get; set; }   //Comment

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string country { get; set; }   //Comment

        [JsonProperty("companyProvince", NullValueHandling = NullValueHandling.Ignore)]
        public string companyProvince { get; set; }   //Comment

        [JsonProperty("companyCity", NullValueHandling = NullValueHandling.Ignore)]
        public string companyCity { get; set; }   //Comment

        [JsonProperty("orderTotal", NullValueHandling = NullValueHandling.Ignore)]
        public int orderTotal { get; set; }   //Comment

        [JsonProperty("collectionTotal", NullValueHandling = NullValueHandling.Ignore)]
        public int collectionTotal { get; set; }   //Comment

        [JsonProperty("clientTotal", NullValueHandling = NullValueHandling.Ignore)]
        public int clientTotal { get; set; }   //Comment

        [JsonProperty("todayRebate", NullValueHandling = NullValueHandling.Ignore)]
        public long todayRebate { get; set; }   //Comment

        [JsonProperty("withDraw", NullValueHandling = NullValueHandling.Ignore)]
        public long withDraw { get; set; }   //Comment


    }
}
