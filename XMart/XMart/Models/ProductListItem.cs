using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Util;

namespace XMart.Models
{
    public class ProductListItem
    {
        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 productId { get; set; }   //Comment

        [JsonProperty("salePrice", NullValueHandling = NullValueHandling.Ignore)]
        public double salePrice { get; set; }   //Comment

        [JsonProperty("productName", NullValueHandling = NullValueHandling.Ignore)]
        public string productName { get; set; }   //Comment

        [JsonProperty("subTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string subTitle { get; set; }   //Comment

        [JsonProperty("productImageBig", NullValueHandling = NullValueHandling.Ignore)]
        public string productImageBig { get; set; }   //Comment

        public bool MemberPriceVisible { get; set; }
        public bool CusPriceVisible { get; set; }

        public ProductListItem()
        {
            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;
        }
    }
}
