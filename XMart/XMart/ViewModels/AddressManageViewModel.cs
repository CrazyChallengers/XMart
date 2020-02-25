﻿using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;
using XMart.Services;
using XMart.Util;
using XMart.ResponseData;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Forms;
using XMart.Views;

namespace XMart.ViewModels
{
    public class AddressManageViewModel : BaseViewModel
    {
        private List<AddressInfo> addressList;   //Comment
        public List<AddressInfo> AddressList
        {
            get { return addressList; }
            set { SetProperty(ref addressList, value); }
        }

        private bool isRefreshing;   //Comment
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        private bool visible;   //Comment
        public bool Visible
        {
            get { return visible; }
            set { SetProperty(ref visible, value); }
        }

        public Command<AddressInfo> EditCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command AddAddressCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public AddressManageViewModel()
        {
            InitAddressList();

            EditCommand = new Command<AddressInfo>((address) =>
            {
                AddressInfo addressInfo = new AddressInfo();

                foreach (var item in AddressList)
                {
                    if (item.addressId == address.addressId)
                    {
                        addressInfo = item;
                        break;
                    }
                }

                EditAddressPage editAddressPage = new EditAddressPage(addressInfo);
                Application.Current.MainPage.Navigation.PushModalAsync(editAddressPage);
            }, (id) => { return true; });

            AddAddressCommand = new Command(() =>
            {
                EditAddressPage editAddressPage = new EditAddressPage();

                Application.Current.MainPage.Navigation.PushModalAsync(editAddressPage);
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            RefreshCommand = new Command(() =>
            {
                InitAddressList();
                IsRefreshing = false;
            }, () => { return true; });
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        private async void InitAddressList()
        {
            try
            {
                RestService _restService = new RestService();

                string memberId = GlobalVariables.LoggedUser.id.ToString();

                AddressRD addressRD = await _restService.GetAddressListById(memberId);

                if (addressRD.result.Count != 0)
                {
                    AddressList = addressRD.result;
                    Visible = false;
                }
                else
                {
                    Visible = true;
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