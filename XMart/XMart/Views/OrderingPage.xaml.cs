using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Models;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderingPage : ContentPage
    {
        OrderingViewModel orderingViewModel;

        public OrderingPage(List<CartItemInfo> productList)
        {
            InitializeComponent();

            orderingViewModel = new OrderingViewModel(productList);

            BindingContext = orderingViewModel;
        }
        /*
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Application.Current.MainPage.Navigation.RemovePage(this);
        }*/
    }
}