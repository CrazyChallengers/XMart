using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Services;
using XMart.ResponseData;
using XMart.Models;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Views;
using Newtonsoft.Json.Linq;

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

        RestSharpService _restSharpService = new RestSharpService();

        public Command CancelCommand { get; set; }
        public Command HomeCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command PayCommand { get; set; }

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

            PayCommand = new Command(() =>
            {
                PlaceAnOrder();
            }, () => { return true; });
        }

        public OrderDetailViewModel(OrderDetail orderDetail)
        {
            Order = orderDetail;
            
            switch (Order.paymentType)
            {
                case 1: Order.PaymentType = "立即支付"; break;
                case 2: Order.PaymentType = "延期一个月"; break;
                case 3: Order.PaymentType = "延期两个月"; break;
                default:
                    break;
            }

            switch (Order.orderStatus)
            {
                case "0": Order.OrderStatus = "未付款"; break;
                case "1": Order.OrderStatus = "已付款"; break;
                case "2": Order.OrderStatus = "未发货"; break;
                case "3": Order.OrderStatus = "已发货"; break;
                case "4": Order.OrderStatus = "交易成功"; break;
                case "5": Order.OrderStatus = "交易关闭"; break;
                case "6": Order.OrderStatus = "交易失败"; break;
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

            PayCommand = new Command(() =>
            {
                PlaceAnOrder();
            }, () => { return true; });
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        private async void CancelOrder()
        {
            try
            {
                SimpleRD simpleRD = await _restSharpService.CancelOrder(Order);

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
        private void InitOrderDetailPage(long orderId)
        {
            try
            {
                OrderDetailRD orderDetailRD = _restSharpService.GetOrderDetailByOrderId(orderId);

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

        private void PlaceAnOrder()
        {
            if (Order.orderStatus == "0")
            {
                string body = "test_body";
                foreach (var item in Order.goodsList)
                {
                    body += item.productName + "//";
                }

                string out_trade_no = "DJ" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string product_code = Order.orderId.ToString();
                string subject = "美而好家具";
                string total_amount = Order.orderTotal.ToString("0.00");

                JObject json = new JObject();
                json.Add("body", body);
                json.Add("out_trade_no", out_trade_no);
                json.Add("product_code", product_code);
                json.Add("subject", subject);
                json.Add("total_amount", total_amount);

                string con = _restSharpService.GetAliPaySign(json.ToString());
                JObject result = JObject.Parse(con);
                string sign = result["data"].ToString();

                MessagingCenter.Send(new object(), "Pay", sign);

                //CrossToastPopUp.Current.ShowToastMessage("支付成功", ToastLength.Long);
                //PayWebPage payWebPage = new PayWebPage();
                //Application.Current.MainPage.Navigation.PushModalAsync(payWebPage);
            }
        }
    }
}
