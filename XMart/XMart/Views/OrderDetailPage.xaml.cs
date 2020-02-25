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
        public OrderDetailPage(long orderId)
        {
            InitializeComponent();

            BindingContext = new OrderDetailViewModel(orderId);
        }

        public OrderDetailPage(OrderDetail orderDetail)
        {
            InitializeComponent();

            BindingContext = new OrderDetailViewModel(orderDetail);
        }
    }
}