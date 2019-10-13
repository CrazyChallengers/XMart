using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class RegisterPara
    {
        [JsonProperty("authCode")]
        public string authCode { get; set; }   //验证码

        [JsonProperty("pwd")]
        public string pwd { get; set; }   //密码

        [JsonProperty("tel")]
        public string tel { get; set; }   //手机号

        [JsonProperty("username")]
        public string username { get; set; }   //用户名
    }
}
