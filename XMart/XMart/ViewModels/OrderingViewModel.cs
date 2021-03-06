﻿using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Util;
using Xamarin.Forms;
using XMart.Views;

namespace XMart.ViewModels
{
    public class OrderingViewModel : BaseViewModel
    {
        private string userName;   //Comment
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string tel;   //Comment
        public string Tel
        {
            get { return tel; }
            set { SetProperty(ref tel, value); }
        }

        private bool isDefault;   //Comment
        public bool IsDefault
        {
            get { return isDefault; }
            set { SetProperty(ref isDefault, value); }
        }

        private string streetName;   //Comment
        public string StreetName
        {
            get { return streetName; }
            set { SetProperty(ref streetName, value); }
        }

        private List<CartItemInfo> productList;   //Comment
        public List<CartItemInfo> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }

        private long totalSelectedPrice;   //Comment
        public long TotalSelectedPrice
        {
            get { return totalSelectedPrice; }
            set { SetProperty(ref totalSelectedPrice, value); }
        }

        private int itemNum;   //Comment
        public int ItemNum
        {
            get { return itemNum; }
            set { SetProperty(ref itemNum, value); }
        }

        private List<string> paymentTypeList;   //Comment
        public List<string> PaymentTypeList
        {
            get { return paymentTypeList; }
            set { SetProperty(ref paymentTypeList, value); }
        }

        private int selectedTypeIndex;   //Comment
        public int SelectedTypeIndex
        {
            get { return selectedTypeIndex; }
            set { SetProperty(ref selectedTypeIndex, value); }
        }

        private long AddressId { get; set; }
        public Command OrderCommand { get; set; }
        public Command AddressManageCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command<long> OneTappedCommand { get; set; }

        public OrderingViewModel(List<CartItemInfo> _productList)
        {
            UserName = default;
            Tel = default;
            IsDefault = false;

            InitAddress();

            PaymentTypeList = new List<string>
            {
                "立即在线支付", "延期一个月", "延期两个月"
            };

            SelectedTypeIndex = 0;

            ProductList = _productList;
            ItemNum = 0;

            foreach (var item in ProductList)
            {
                TotalSelectedPrice += item.memberPrice*item.productNum;
                ItemNum += item.productNum;
            }

            OrderCommand = new Command(() =>
            {
                OrderAsync();
            }, () => { return true; });

            AddressManageCommand = new Command(() =>
            {
                AddressManagePage addressManagePage = new AddressManagePage();
                Application.Current.MainPage.Navigation.PushModalAsync(addressManagePage);
            }, () => { return true; });

            OneTappedCommand = new Command<long>((id) =>
            {
                ProductDetailPage productDetailPage = new ProductDetailPage(id.ToString());
                Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            }, (id) => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

        }

        /// <summary>
        /// 提交订单
        /// </summary>
        private async void OrderAsync()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();
                OrderPara orderPara = new OrderPara
                {
                    addressId = AddressId,
                    goodsList = ProductList,
                    orderTotal = TotalSelectedPrice,
                    paymentType = SelectedTypeIndex + 1,
                    streetName = StreetName,
                    tel = Tel,
                    userId = GlobalVariables.LoggedUser.id.ToString(),
                    userName = UserName
                };
                foreach (var item in orderPara.goodsList)
                {
                    item.salePrice = item.memberPrice;
                }

                StupidRD stupidRD = _restSharpService.Order(orderPara);

                if (stupidRD.result != 0)
                {
                    CrossToastPopUp.Current.ShowToastSuccess("提交订单成功！请及时支付！", ToastLength.Long);

                    //var stack1 = Application.Current.MainPage.Navigation.NavigationStack;
                    //var stack2 = Application.Current.MainPage.Navigation.ModalStack;
                    //Application.Current.MainPage.Navigation.RemovePage(stack2[stack2.Count - 1]);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    OrderDetailPage orderDetailPage = new OrderDetailPage(stupidRD.result);
                    await Application.Current.MainPage.Navigation.PushModalAsync(orderDetailPage);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("提交订单失败！有问题请联系客服人员。", ToastLength.Long);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async void InitAddress()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();

                string memberId = GlobalVariables.LoggedUser.id.ToString();

                AddressRD addressRD = await _restSharpService.GetAddressListById(memberId);

                if (addressRD.result.Count != 0)
                {
                    foreach (var item in addressRD.result)
                    {
                        if (item.isDefault)
                        {
                            AddressId = item.addressId;
                            UserName = item.userName;
                            Tel = item.tel;
                            IsDefault = item.isDefault;
                            StreetName = item.streetName;
                            break;
                        }
                    }

                    if (AddressId == 0)
                    {
                        UserName = "无收货地址";
                    }
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("无收货地址列表，请添加。", ToastLength.Long);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
