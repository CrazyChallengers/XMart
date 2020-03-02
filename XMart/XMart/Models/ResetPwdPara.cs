using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class ResetPwdPara
    {
        [JsonProperty("authCode", NullValueHandling = NullValueHandling.Ignore)]
        public string authCode { get; set; }   //comment

        [JsonProperty("newPwd", NullValueHandling = NullValueHandling.Ignore)]
        public string newPwd { get; set; }   //comment

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }   //comment

    }
}
