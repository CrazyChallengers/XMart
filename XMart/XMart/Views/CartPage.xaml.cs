using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using XMart.Util;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        CartViewModel cartViewModel = new CartViewModel();

        public CartPage()
        {
            InitializeComponent();

            BindingContext = cartViewModel;
        }

        /*
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            cartViewModel.InitCart();
        }
        */
    }
}