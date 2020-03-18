using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;
using XMart.ResponseData;
using XMart.Services;
using Rg.Plugins.Popup.Services;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        ProductDetailVM productDetailVM;

        public ProductDetailPage(string productId)
        {
            InitializeComponent();

            //InitProductDetailPageAsync(productId);
            //productDetailVM.product = productInfo;

            productDetailVM = new ProductDetailVM(productId);
            //var htmlSource = new HtmlWebViewSource();
            //htmlSource.Html = @"<html><body>" + productDetailVM.Product.detail + "</body></html>";
            //webView.Source = htmlSource;

            BindingContext = productDetailVM;
        }

        private async void WeChatShare_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果
            string action = await DisplayActionSheet("微信分享", "取消", "", "分享到好友", "分享到朋友圈");//选择分享到朋友圈还是分享到对话。
            if (action == "分享到好友")
            {
                MessagingCenter.Send(new object(), "ShareToFriend", "欢迎来到美而好家具！");
            }
            if (action == "分享到朋友圈")
            {
                MessagingCenter.Send(new object(), "ShareToTimeline", "欢迎来到美而好家具！");
            }
        }
    }
}