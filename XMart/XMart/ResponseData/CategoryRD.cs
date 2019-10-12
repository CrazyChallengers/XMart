using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class CategoryRD : CommonRD
    {
        [JsonProperty("data")]
        public Category data { get; set; }   //comment
    }

}
