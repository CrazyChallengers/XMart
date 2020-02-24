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
        ProductListVM productListVM;

        public ProductListPage(Category subCategoryInfo)
        {
            InitializeComponent();

            productListVM = new ProductListVM(subCategoryInfo);
            BindingContext = productListVM;
        }

        public ProductListPage(string index)
        {
            InitializeComponent();

            productListVM = new ProductListVM(index);
            BindingContext = productListVM;
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