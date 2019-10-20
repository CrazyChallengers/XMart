using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class LoginRD : CommonRD
    {
        [JsonProperty("result")]
        public UserInfo result { get; set; }   //comment
    }
}
