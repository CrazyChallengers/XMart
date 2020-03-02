using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Models
{
    public class RegisterPara
    {
        [JsonProperty("authCode", NullValueHandling = NullValueHandling.Ignore)]
        public string authCode { get; set; }   //验证码

        [JsonProperty("userPwd", NullValueHandling = NullValueHandling.Ignore)]
        public string userPwd { get; set; }   //密码

        [JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore)]
        public string tel { get; set; }   //手机号

        //[JsonProperty("userName", NullValueHandling = NullValueHandling.Ignore)]
        //public string userName { get; set; }   //用户名

        [JsonProperty("userType", NullValueHandling = NullValueHandling.Ignore)]
        public string userType { get; set; }   //用户类型

        [JsonProperty("invitePhone", NullValueHandling = NullValueHandling.Ignore)]
        public string invitePhone { get; set; }   //邀请码
    }
}
