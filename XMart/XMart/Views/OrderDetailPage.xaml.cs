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
    public partial class OrderDetailPage : ContentPage
    {
        OrderDetailViewModel orderDetailViewModel;

        public OrderDetailPage(long orderId)
        {
            InitializeComponent();

            orderDetailViewModel = new OrderDetailViewModel(orderId);
            BindingContext = orderDetailViewModel;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            //base.OnAppearing();
            orderDetailViewModel.InitOrderDetailPage(orderDetailViewModel.Order.orderId);
        }
    }
}