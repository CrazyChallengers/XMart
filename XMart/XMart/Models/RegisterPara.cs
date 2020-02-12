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

        [JsonProperty("userPwd")]
        public string userPwd { get; set; }   //密码

        [JsonProperty("tel")]
        public string tel { get; set; }   //手机号

        //[JsonProperty("userName")]
        //public string userName { get; set; }   //用户名

        [JsonProperty("userType")]
        public string userType { get; set; }   //用户类型

        [JsonProperty("invitePhone")]
        public string invitePhone { get; set; }   //邀请码
    }
}
