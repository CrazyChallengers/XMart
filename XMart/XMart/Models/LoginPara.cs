using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class LoginPara
    {
        [JsonProperty("authCode")]
        public string authCode { get; set; }   //comment

        [JsonProperty("tel")]
        public string tel { get; set; }   //comment

        //[JsonProperty("userName")]
        //public string userName { get; set; }   //comment

        [JsonProperty("userPwd")]
        public string userPwd { get; set; }   //comment
        
    }
}
