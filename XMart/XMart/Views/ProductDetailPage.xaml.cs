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
        //ProductDetailVM productDetailVM;

        public ProductDetailPage(string productId)
        {
            InitializeComponent();

            //InitProductDetailPageAsync(productId);
            //productDetailVM.product = productInfo;

            BindingContext = new ProductDetailVM(productId);
        }

        private void CarouselItem_Tapped(object sender, EventArgs e)
        {

        }

        private void AddToCartBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void BuyButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}