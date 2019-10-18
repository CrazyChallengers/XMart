using Newtonsoft.Json;

namespace XMart.Models
{
    public class CartItemInfo : BaseModel
    {
        [JsonProperty("createDate")]
        public string createDate { get; set; }   //comment

        [JsonProperty("deleteStatus")]
        public int deleteStatus { get; set; }    //comment

        [JsonProperty("id")]
        public int id { get; set; }    //comment

        [JsonProperty("memberId")]
        public int memberId { get; set; }    //comment

        [JsonProperty("memberNickName")]
        public string memberNickName { get; set; }    //comment

        [JsonProperty("modifyDate")]
        public string modifyDate { get; set; }    //comment

        [JsonProperty("price")]
        public double price { get; set; }    //comment

        [JsonProperty("productAttr")]
        public string productAttr { get; set; }    //comment

        [JsonProperty("productBrand")]
        public string productBrand { get; set; }    //comment

        [JsonProperty("productCategoryId")]
        public int productCategoryId { get; set; }    //comment

        [JsonProperty("productId")]
        public int productId { get; set; }    //comment

        [JsonProperty("productName")]
        public string productName { get; set; }    //comment

        [JsonProperty("productPic")]
        public string productPic { get; set; }    //comment

        [JsonProperty("productSkuCode")]
        public string productSkuCode { get; set; }    //comment

        [JsonProperty("productSkuId")]
        public int productSkuId { get; set; }    //comment

        [JsonProperty("productSn")]
        public string productSn { get; set; }    //comment

        [JsonProperty("productSubTitle")]
        public string productSubTitle { get; set; }    //comment

        [JsonProperty("quantity")]
        public int quantity { get; set; }    //comment

        [JsonProperty("sp1")]
        public string sp1 { get; set; }    //comment

        [JsonProperty("sp2")]
        public string sp2 { get; set; }    //comment

        [JsonProperty("sp3")]
        public string sp3 { get; set; }    //comment

        private bool isChecked;   //comment

        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }

        public CartItemInfo()
        {
            IsChecked = false;
        }

    }
}
