using Newtonsoft.Json;
using XMart.Util;
using XMart.ViewModels;

namespace XMart.Models
{
    public class CartItemInfo : ProductInfo
    {
        [JsonProperty("checked", NullValueHandling = NullValueHandling.Ignore)]
        public string _checked { get; set; }   //comment

        [JsonProperty("deliveryAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string deliveryAddress { get; set; }   //comment

        [JsonProperty("deliveryWay", NullValueHandling = NullValueHandling.Ignore)]
        public string deliveryWay { get; set; }   //comment

        [JsonProperty("productNum", NullValueHandling = NullValueHandling.Ignore)]
        public int productNum { get; set; }   //comment

        [JsonProperty("productImg", NullValueHandling = NullValueHandling.Ignore)]
        public string productImg { get; set; }   //comment

        //private bool isChecked;   //comment
        //
        //public bool IsChecked
        //{
        //    get { return isChecked; }
        //    set { SetProperty(ref isChecked, value); }
        //}

        public bool MemberPriceVisible { get; set; }
        public bool CusPriceVisible { get; set; }

        public bool Checked { get; set; }

        public CartItemInfo()
        {
            Checked = _checked == "1";
            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;

        }
    }
}
