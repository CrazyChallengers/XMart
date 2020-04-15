using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Util;
using XMart.Views;

namespace XMart.ViewModels
{
    public class OrderListViewModel : BaseViewModel
    {
		private ObservableCollection<OrderDetail> orderList;   //Comment
		public ObservableCollection<OrderDetail> OrderList
		{
			get { return orderList; }
			set { SetProperty(ref orderList, value); }
		}

		private bool visible;   //Comment
		public bool Visible
		{
			get { return visible; }
			set { SetProperty(ref visible, value); }
		}

		private bool indicatorIsRunning;   //Comment
		public bool IndicatorIsRunning
		{
			get { return indicatorIsRunning; }
			set { SetProperty(ref indicatorIsRunning, value); }
		}

		private string loadMoreButtonText;   //Comment
		public string LoadMoreButtonText
		{
			get { return loadMoreButtonText; }
			set { SetProperty(ref loadMoreButtonText, value); }
		}

		private bool buttonIsEnable;   //Comment
		public bool ButtonIsEnable
		{
			get { return buttonIsEnable; }
			set { SetProperty(ref buttonIsEnable, value); }
		}

		private int totalOrderNum;   //Comment
		public int TotalOrderNum
		{
			get { return totalOrderNum; }
			set { SetProperty(ref totalOrderNum, value); }
		}

		private int orderNum;   //Comment
		public int OrderNum
		{
			get { return orderNum; }
			set { SetProperty(ref orderNum, value); }
		}

		private int page { get; set; }

		public Command<long> OneTappedCommand { get; set; }
		public Command RefreshCommand { get; set; }
		public Command BackCommand { get; set; }
		public Command LoadMoreCommand { get; set; }

		public OrderListViewModel()
		{
			OrderList = new ObservableCollection<OrderDetail>();
			page = 1;
			TotalOrderNum = 0;
			OrderNum = 0;

			OneTappedCommand = new Command<long>((id) =>
			{
				OrderDetailPage orderDetailPage = new OrderDetailPage(id);
				Application.Current.MainPage.Navigation.PushModalAsync(orderDetailPage);
			}, (id) => { return true; });

			RefreshCommand = new Command(() =>
			{
				TotalOrderNum = 0;
				OrderNum = 0;
				OrderList.Clear();
				InitOrderList();
			}, () => { return true; });

			LoadMoreCommand = new Command(() =>
			{
				//CrossToastPopUp.Current.ShowToastWarning(str + "/" + ProductNum, ToastLength.Short);
				page++;
				InitOrderList();
			}, () => { return true; });

			BackCommand = new Command(() =>
			{
				Application.Current.MainPage.Navigation.PopModalAsync();
			}, () => { return true; });

			/*
			if (GlobalVariables.IsLogged)
			{
				InitOrderList();
			}*/
		}

		public async void InitOrderList()
		{
			try
			{
				IndicatorIsRunning = true;
				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
					return;
				}

				RestSharpService _restSharpService = new RestSharpService();
				int size = 10;
				OrderListRD orderListRD = await _restSharpService.GetOrderListById(GlobalVariables.LoggedUser.id, page, size);

				if (orderListRD.result.data.Count != 0)
				{
					foreach (var item in orderListRD.result.data)
					{
						OrderList.Add(item);
					}

					TotalOrderNum = orderListRD.result.total;
					OrderNum += orderListRD.result.data.Count;
					Visible = false;

					foreach (var item in OrderList)
					{
						switch (item.paymentType)
						{
							case 1: item.PaymentType = "立即支付"; break;
							case 2: item.PaymentType = "延期一个月"; break;
							case 3: item.PaymentType = "延期两个月"; break;
							default:
								break;
						}

						switch (item.orderStatus)
						{
							case "0": item.OrderStatus = "未付款"; break;
							case "1": item.OrderStatus = "已付款"; break;
							case "2": item.OrderStatus = "未发货"; break;
							case "3": item.OrderStatus = "已发货"; break;
							case "4": item.OrderStatus = "交易成功"; break;
							case "5": item.OrderStatus = "交易关闭"; break;
							case "6": item.OrderStatus = "交易失败"; break;
							default:
								break;
						}
					}
				}
				else
				{
					Visible = true;
				}
				ChangeButtonText();
				IndicatorIsRunning = false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void ChangeButtonText()
		{
			if (OrderNum == TotalOrderNum)
			{
				LoadMoreButtonText = OrderNum.ToString() + "/" + TotalOrderNum.ToString() + "，" + "已全部加载";
				ButtonIsEnable = false;
			}
			else
			{
				LoadMoreButtonText = OrderNum.ToString() + "/" + TotalOrderNum.ToString() + "，" + "点击加载更多";
				ButtonIsEnable = true;
			}
		}
	}
}
