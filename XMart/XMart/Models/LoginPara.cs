using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class LoginPara
    {
        [JsonProperty("authCode", NullValueHandling = NullValueHandling.Ignore)]
        public string authCode { get; set; }   //comment

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }   //comment

        //[JsonProperty("userName", NullValueHandling = NullValueHandling.Ignore)]
        //public string userName { get; set; }   //comment

        [JsonProperty("userPwd", NullValueHandling = NullValueHandling.Ignore)]
        public string userPwd { get; set; }   //comment
        
    }
}
