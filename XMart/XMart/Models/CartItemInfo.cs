using Newtonsoft.Json;

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
        public string productNum { get; set; }   //comment

        [JsonProperty("productImg", NullValueHandling = NullValueHandling.Ignore)]
        public string productImg { get; set; }   //comment

        //private bool isChecked;   //comment
        //
        //public bool IsChecked
        //{
        //    get { return isChecked; }
        //    set { SetProperty(ref isChecked, value); }
        //}

        public CartItemInfo()
        {
            //IsChecked = false;
        }
    }
}
