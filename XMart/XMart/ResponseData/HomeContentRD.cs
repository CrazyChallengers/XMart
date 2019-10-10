using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XMart.Models;
using Newtonsoft.Json;

namespace XMart.ResponseData
{
    public class HomeContentRD
    {
        [JsonProperty("code")]
        public int code { get; set; }

        [JsonProperty("data")]
        public HomeContentDataList data { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }

    public class HomeContentDataList
    {
        [JsonProperty("advertiseList")]
        public List<AdvertiseInfo> advertiseList;   //comment

        [JsonProperty("brandList")]
        public List<BrandInfo> brandList;   //comment

        [JsonProperty("subjectList")]
        public List<SubjectInfo> subjectList;   //comment

    }
}
