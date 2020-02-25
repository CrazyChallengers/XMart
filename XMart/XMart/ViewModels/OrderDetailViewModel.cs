using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Services;
using XMart.ResponseData;
using XMart.Models;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace XMart.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        private OrderDetail order;   //Comment
        public OrderDetail Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        private string paymentType;   //Comment
        public string PaymentType
        {
            get { return paymentType; }
            set { SetProperty(ref paymentType, value); }
        }

        private int itemNum;   //Comment
        public int ItemNum
        {
            get { return itemNum; }
            set { SetProperty(ref itemNum, value); }
        }



        public Command HomeCommand { get; set; }
        public Command BackCommand { get; set; }

        public OrderDetailViewModel(long orderId)
        {
            InitOrderDetailPage(orderId);

            switch (Order.paymentType)
            {
                case 1: PaymentType = "立即支付";break;
                case 2: PaymentType = "延期一个月";break;
                case 3: PaymentType = "延期两个月";break;
                default:
                    break;
            }

            ItemNum = 0;
            foreach (var item in Order.goodsList)
            {
                ItemNum += item.productNum;
            }

            HomeCommand = new Command(() =>
            {
                //Application.Current.MainPage.Navigation.PopToRootAsync();
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        public OrderDetailViewModel(OrderDetail orderDetail)
        {
            Order = orderDetail;

            switch (Order.paymentType)
            {
                case 1: PaymentType = "立即支付"; break;
                case 2: PaymentType = "延期一个月"; break;
                case 3: PaymentType = "延期两个月"; break;
                default:
                    break;
            }

            ItemNum = 0;
            foreach (var item in Order.goodsList)
            {
                ItemNum += item.productNum;
            }

            HomeCommand = new Command(() =>
            {
                //Application.Current.MainPage.Navigation.PopToRootAsync();
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="orderId"></param>
        private async void InitOrderDetailPage(long orderId)
        {
            try
            {
                RestService _restService = new RestService();
                OrderDetailRD orderDetailRD = await _restService.GetOrderDetailByOrderId(orderId);

                if (orderDetailRD.success)
                {
                    Order = orderDetailRD.result;
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(orderDetailRD.message, ToastLength.Long);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
