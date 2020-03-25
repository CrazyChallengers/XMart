using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

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

            //ProductListScrollView.Scrolled += (object sender, ScrolledEventArgs e) => {
            //    //label.Text = "Position: " + e.ScrollX + " x " + e.ScrollY;
            //    CrossToastPopUp.Current.ShowToastWarning(e.ScrollX + "/" + e.ScrollY + "/" + ((ScrollView)sender).ContentSize, ToastLength.Short);
            //};
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