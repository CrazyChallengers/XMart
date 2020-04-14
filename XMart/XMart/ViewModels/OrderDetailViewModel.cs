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
using System.Threading.Tasks;
using XMart.Util;

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

        private bool indicatorIsRunning;   //Comment
        public bool IndicatorIsRunning
        {
            get { return indicatorIsRunning; }
            set { SetProperty(ref indicatorIsRunning, value); }
        }

        private bool payBtnVisible;   //Comment
        public bool PayBtnVisible
        {
            get { return payBtnVisible; }
            set { SetProperty(ref payBtnVisible, value); }
        }

        private bool deleteBtnVisible;   //Comment
        public bool DeleteBtnVisible
        {
            get { return deleteBtnVisible; }
            set { SetProperty(ref deleteBtnVisible, value); }
        }

        private bool cancelButtonEnable;   //Comment
        public bool CancelButtonEnable
        {
            get { return cancelButtonEnable; }
            set { SetProperty(ref cancelButtonEnable, value); }
        }

        RestSharpService _restSharpService = new RestSharpService();

        public Command CancelCommand { get; set; }
        public Command HomeCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command PayCommand { get; set; }
        public Command DeleteOrderCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command<long> OneTappedCommand { get; set; }

        public OrderDetailViewModel(long orderId)
        {
            Order = new OrderDetail();

            DeleteBtnVisible = false;
            InitOrderDetailPage(orderId);

            CancelCommand = new Command(async () =>
            {
                bool action = await Application.Current.MainPage.DisplayAlert("取消订单", "您想要取消订单吗？取消后，我们将为您退回货款并拦截快递。", "是的", "算了");
                if (action)
                {
                    CancelOrder();
                    InitOrderDetailPage(orderId);
                }
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
                InitOrderDetailPage(orderId);
            }, () => { return true; });

            DeleteOrderCommand = new Command(() =>
            {
                DeleteOrderAsync();
            }, () => { return true; });

            DeleteCommand = new Command(() =>
            {
                DeleteBtnVisible = !DeleteBtnVisible;
            }, () => { return true; });

            OneTappedCommand = new Command<long>((id) =>
            {
                ProductDetailPage productDetailPage = new ProductDetailPage(id.ToString());
                Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            }, (id) => { return true; });

            MessagingCenter.Subscribe<object, string>(this, "PaySuccess", (sender, resultStatus) =>
            {
                switch (resultStatus)
                {
                    case "9000": CrossToastPopUp.Current.ShowToastSuccess("订单支付成功！", ToastLength.Long); break;
                    case "8000": CrossToastPopUp.Current.ShowToastWarning("正在处理中！", ToastLength.Long); break;
                    case "4000": CrossToastPopUp.Current.ShowToastError("订单支付失败！", ToastLength.Long); break;
                    case "6001": CrossToastPopUp.Current.ShowToastWarning("用户中途取消！", ToastLength.Long); break;
                    case "6002": CrossToastPopUp.Current.ShowToastError("网络连接出错！", ToastLength.Long); break;
                    default: break;
                }

                InitOrderDetailPage(orderId);
            });
        }

        private async void DeleteOrderAsync()
        {
            try
            {
                StupidRD stupidRD = await _restSharpService.DeleteOrder(Order.orderId.ToString());

                if (stupidRD.success)
                {
                    CrossToastPopUp.Current.ShowToastSuccess("删除成功！", ToastLength.Long);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(stupidRD.message, ToastLength.Long);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        private async void CancelOrder()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

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
        public void InitOrderDetailPage(long orderId)
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                IndicatorIsRunning = true;
                PayBtnVisible = false;

                OrderDetailRD orderDetailRD = _restSharpService.GetOrderDetailByOrderId(orderId);

                if (orderDetailRD.success)
                {
                    Order = orderDetailRD.result;

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
                        case "0": OrderStatus = "未付款"; PayBtnVisible = true; break;
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
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError(orderDetailRD.message, ToastLength.Long);
                }

                IndicatorIsRunning = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        private void PlaceAnOrder()
        {
            if (Order.orderStatus == "0")
            {
                string body = "test_body";
                foreach (var item in Order.goodsList)
                {
                    body += item.productName + "//";
                }

                //string out_trade_no = "DJ" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string out_trade_no = Order.orderId.ToString();
                string product_code = "QUICK_MSECURITY_PAY";
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
