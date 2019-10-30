using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        ProductListVM productListVM = new ProductListVM();

        public ProductListPage(SubCategoryInfo subCategoryInfo)
        {
            InitializeComponent();

            productListVM.ProductList = new List<ProductInfo>
            {
                new ProductInfo{productName = "产品一", subTitle="这里是产品一的简介balabala", tprice = 300.00, productImageBig="circle.png"},
                new ProductInfo{productName = "产品二", subTitle="这里是产品二的简介balabala", tprice = 300.00, productImageBig="circle.png"},
                new ProductInfo{productName = "产品三", subTitle="这里是产品三的简介balabala", tprice = 300.00, productImageBig="circle.png"},
                new ProductInfo{productName = "产品四", subTitle="这里是产品四的简介balabala", tprice = 300.00, productImageBig="circle.png"}
            };

            BindingContext = productListVM;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ItemFrame_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            int index = ItemStack.Children.IndexOf(frame);

            ProductInfo productInfo = productListVM.ProductList[index];
            ProductDetailPage productDetailPage = new ProductDetailPage(productInfo.productId.ToString());
            Navigation.PushModalAsync(productDetailPage);
        }
    }
}