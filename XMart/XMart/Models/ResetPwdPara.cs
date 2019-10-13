using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class ResetPwdPara
    {
        [JsonProperty("authCode")]
        public string authCode { get; set; }   //comment

        [JsonProperty("newPwd")]
        public string newPwd { get; set; }   //comment

        [JsonProperty("tel")]
        public string tel { get; set; }   //comment

    }
}
