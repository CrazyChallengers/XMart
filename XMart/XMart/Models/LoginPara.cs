using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class LoginPara
    {
        [JsonProperty("challenge")]
        public string challenge { get; set; }   //comment

        [JsonProperty("seccode")]
        public string seccode { get; set; }   //comment

        [JsonProperty("statusKey")]
        public string statusKey { get; set; }   //comment

        [JsonProperty("userName")]
        public string userName { get; set; }   //comment

        [JsonProperty("userPwd")]
        public string userPwd { get; set; }   //comment

        [JsonProperty("validate")]
        public string validate { get; set; }   //comment

    }
}
