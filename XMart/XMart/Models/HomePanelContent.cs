using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XMart.Views;

namespace XMart.Models
{
    public class HomePanelContent
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string created { get; set; }   //comment

        [JsonProperty("fullUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string fullUrl { get; set; }   //comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int id { get; set; }   //comment

        [JsonProperty("panelId", NullValueHandling = NullValueHandling.Ignore)]
        public int panelId { get; set; }   //comment

        [JsonProperty("picUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string picUrl { get; set; }   //comment

        [JsonProperty("picUrl2", NullValueHandling = NullValueHandling.Ignore)]
        public string picUrl2 { get; set; }   //comment

        [JsonProperty("picUrl3", NullValueHandling = NullValueHandling.Ignore)]
        public string picUrl3 { get; set; }   //comment

        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public long productId { get; set; }   //comment

        [JsonProperty("productImageBig", NullValueHandling = NullValueHandling.Ignore)]
        public string productImageBig { get; set; }   //comment

        [JsonProperty("productName", NullValueHandling = NullValueHandling.Ignore)]
        public string productName { get; set; }   //comment

        [JsonProperty("salePrice", NullValueHandling = NullValueHandling.Ignore)]
        public double salePrice { get; set; }   //comment

        [JsonProperty("sortOrder", NullValueHandling = NullValueHandling.Ignore)]
        public int sortOrder { get; set; }   //comment

        [JsonProperty("subTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string subTitle { get; set; }   //comment

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int type { get; set; }   //comment

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public string updated { get; set; }   //comment

        public ICommand ItemTapCommand { set; get; }

        public HomePanelContent()
        {
            ItemTapCommand = new Command(
                execute: () =>
                {
                    ProductDetailPage productDetailPage = new ProductDetailPage(productId.ToString());
                    //Navigation.PushModalAsync(productDetailPage);
                    //Console.WriteLine(productId);
                }
                );
        }
    }
}
