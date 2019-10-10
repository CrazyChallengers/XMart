using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XMart.Models
{
    public class ProductInfo
    {
        [JsonProperty("albumPics")]
        public string albumPics { get; set; }   //comment

        [JsonProperty("brandId")]
        public int brandId { get; set; }   //comment

        [JsonProperty("brandName")]
        public string brandName { get; set; }   //comment

        [JsonProperty("deleteStatus")]
        public int deleteStatus { get; set; }   //comment

        [JsonProperty("description")]
        public string description { get; set; }   //comment

        [JsonProperty("detailDesc")]
        public string detailDesc { get; set; }   //comment

        [JsonProperty("detailHtml")]
        public string detailHtml { get; set; }   //comment

        [JsonProperty("detailMobileHtml")]
        public string detailMobileHtml { get; set; }   //comment

        [JsonProperty("detailTitle")]
        public string detailTitle { get; set; }   //comment

        [JsonProperty("feightTemplateId")]
        public int feightTemplateId { get; set; }   //comment

        [JsonProperty("giftGrowth")]
        public int giftGrowth { get; set; }   //comment

        [JsonProperty("giftPoint")]
        public int giftPoint { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("keywords")]
        public string keywords { get; set; }   //comment

        [JsonProperty("lowStock")]
        public int lowStock { get; set; }   //comment

        [JsonProperty("name")]
        public string name { get; set; }   //comment

        [JsonProperty("newStatus")]
        public int newStatus { get; set; }   //comment

        [JsonProperty("note")]
        public string note { get; set; }   //comment

        [JsonProperty("originalPrice")]
        public double originalPrice { get; set; }   //comment

        [JsonProperty("pic")]
        public string pic { get; set; }   //comment

        [JsonProperty("previewStatus")]
        public int previewStatus { get; set; }   //comment

        [JsonProperty("price")]
        public double price { get; set; }   //comment

        [JsonProperty("productAttributeCategoryId")]
        public int productAttributeCategoryId { get; set; }   //comment

        [JsonProperty("productCategoryId")]
        public int productCategoryId { get; set; }   //comment

        [JsonProperty("productCategoryName")]
        public string productCategoryName { get; set; }   //comment

        [JsonProperty("productSn")]
        public string productSn { get; set; }   //comment

        [JsonProperty("promotionEndTime")]
        public string promotionEndTime { get; set; }   //comment

        [JsonProperty("promotionPerLimit")]
        public int promotionPerLimit { get; set; }   //comment

        [JsonProperty("promotionPrice")]
        public int promotionPrice { get; set; }   //comment

        [JsonProperty("promotionStartTime")]
        public string promotionStartTime { get; set; }   //comment

        [JsonProperty("promotionType")]
        public int promotionType { get; set; }   //comment

        [JsonProperty("publishStatus")]
        public int publishStatus { get; set; }   //comment

        [JsonProperty("recommandStatus")]
        public int recommandStatus { get; set; }   //comment

        [JsonProperty("sale")]
        public int sale { get; set; }   //comment

        [JsonProperty("serviceIds")]
        public string serviceIds { get; set; }   //comment

        [JsonProperty("sort")]
        public int sort { get; set; }   //comment

        [JsonProperty("stock")]
        public int stock { get; set; }   //comment

        [JsonProperty("subTitle")]
        public string subTitle { get; set; }   //comment

        [JsonProperty("unit")]
        public string unit { get; set; }   //comment

        [JsonProperty("usePointLimit")]
        public int usePointLimit { get; set; }   //comment

        [JsonProperty("verifyStatus")]
        public int verifyStatus { get; set; }   //comment

        [JsonProperty("weight")]
        public double weight { get; set; }   //comment
        
    }
}
