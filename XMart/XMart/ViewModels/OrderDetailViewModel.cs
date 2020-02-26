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

        private string orderStatus;   //Comment
        public string OrderStatus
        {
            get { return orderStatus; }
            set { SetProperty(ref orderStatus, value); }
        }

        RestService _restService = new RestService();

        public Command CancelCommand { get; set; }
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

            switch (Order.orderStatus)
            {
                case "0": OrderStatus = "未付款"; break;
                case "1": OrderStatus = "已付款"; break;
                case "2": OrderStatus = "未发货"; break;
                case "3": OrderStatus = "已发货"; break;
                case "4": OrderStatus = "交易成功"; break;
                case "5": OrderStatus = "交易关闭"; break;
                case "6": OrderStatus = "交易失败"; break;
                default:
                    break;
            }

            ItemNum = 0;
            foreach (var item in Order.goodsList)
            {
                ItemNum += item.productNum;
            }

            CancelCommand = new Command(() =>
            {
                CancelOrder();
            }, () => { return true; });

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

            switch (Order.orderStatus)
            {
                case "0": OrderStatus = "未付款"; break;
                case "1": OrderStatus = "已付款"; break;
                case "2": OrderStatus = "未发货"; break;
                case "3": OrderStatus = "已发货"; break;
                case "4": OrderStatus = "交易成功"; break;
                case "5": OrderStatus = "交易关闭"; break;
                case "6": OrderStatus = "交易失败"; break;
                default:
                    break;
            }

            ItemNum = 0;
            foreach (var item in Order.goodsList)
            {
                ItemNum += item.productNum;
            }

            CancelCommand = new Command(() =>
            {
                CancelOrder();
            }, () => { return true; });

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
        /// 取消订单
        /// </summary>
        private async void CancelOrder()
        {
            try
            {
                SimpleRD simpleRD = await _restService.CancelOrder(Order);

                if (simpleRD.success)
                {
                    CrossToastPopUp.Current.ShowToastSuccess("该订单已关闭！", ToastLength.Long);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("该订单取消失败！请联系客服人员！", ToastLength.Long);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="orderId"></param>
        private async void InitOrderDetailPage(long orderId)
        {
            try
            {
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
