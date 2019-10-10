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
        public List<AdvertiseInfo> advertiseList { get; set; }   //comment

        [JsonProperty("brandList")]
        public List<BrandInfo> brandList { get; set; }   //comment

        [JsonProperty("homeFlashPromotion")]
        public HomeFlashPromotion homeFlashPromotion { get; set; }   //comment
        
        [JsonProperty("hotProductList")]
        public List<ProductInfo> hotProductList { get; set; }   //comment

        [JsonProperty("newProductList")]
        public List<ProductInfo> newProductList { get; set; }   //comment
        
        [JsonProperty("subjectList")]
        public List<SubjectInfo> subjectList { get; set; }   //comment

    }

    public class HomeFlashPromotion
    {
        [JsonProperty("endTime")]
        public string endTime { get; set; }   //comment

        [JsonProperty("nextEndTime")]
        public string nextEndTime { get; set; }   //comment

        [JsonProperty("nextStartTime")]
        public string nextStartTime { get; set; }   //comment

        [JsonProperty("productList")]
        public List<ProductInfo> productList { get; set; }   //comment
        
        [JsonProperty("startTime")]
        public string startTime { get; set; }   //comment

    }
}
