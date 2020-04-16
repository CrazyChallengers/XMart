using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Util;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderListPage : ContentPage
    {
        OrderListViewModel orderListViewModel = new OrderListViewModel();

        public OrderListPage()
        {
            InitializeComponent();

            BindingContext = orderListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (GlobalVariables.IsLogged)
            {
                orderListViewModel.TotalOrderNum = 0;
                orderListViewModel.OrderNum = 0;
                orderListViewModel.page = 1;
                orderListViewModel.OrderList.Clear();
                orderListViewModel.InitOrderList();
            }
            //var orderingPage = Navigation.ModalStack.FirstOrDefault(p => p is OrderingPage);
            //Navigation.RemovePage(orderingPage);
        }
    }
}