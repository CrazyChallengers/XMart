using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionPage : ContentPage
    {
        CollectionViewModel collectionViewModel;

        public CollectionPage()
        {
            InitializeComponent();

            collectionViewModel = new CollectionViewModel();
            BindingContext = collectionViewModel;
        }

        private void ItemFrame_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            int index = CollectionItemStack.Children.IndexOf(frame);

            ProductListItem productListItem = collectionViewModel.ProductList[index];
            ProductDetailPage productDetailPage = new ProductDetailPage(productListItem.productId.ToString());
            Navigation.PushModalAsync(productDetailPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionViewModel.Init();
        }
    }
}