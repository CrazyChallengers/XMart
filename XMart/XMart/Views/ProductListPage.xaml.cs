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
        RestService _restService = new RestService();

        ProductListVM productListVM = new ProductListVM();

        public ProductListPage(Category subCategoryInfo)
        {
            InitializeComponent();

            BindingContext = productListVM;

            GetProductList(subCategoryInfo);

        }

        private async Task GetProductList(Category subCategoryInfo)
        {
            int page = 1;
            int size = 20;
            string sort = "1";
            long cid = subCategoryInfo.id;
            int priceGt = -1;
            int priceLte = -1;
            ProductListRD productListRD = await _restService.GetProductList(page, size, sort, cid, priceGt, priceLte);

            productListVM.ProductList = productListRD.result.data;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ItemFrame_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            int index = ItemStack.Children.IndexOf(frame);

            ProductListItem productListItem = productListVM.ProductList[index];
            ProductDetailPage productDetailPage = new ProductDetailPage(productListItem.productId.ToString());
            Navigation.PushModalAsync(productDetailPage);
        }
    }
}