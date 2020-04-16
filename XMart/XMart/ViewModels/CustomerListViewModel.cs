using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Util;
using Xamarin.Forms;

namespace XMart.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        private List<UserInfo> customerList;   //Comment
        public List<UserInfo> CustomerList
        {
            get { return customerList; }
            set { SetProperty(ref customerList, value); }
        }

        private bool visible;   //Comment
        public bool Visible
        {
            get { return visible; }
            set { SetProperty(ref visible, value); }
        }

        public Command BackCommand { get; set; }

        public CustomerListViewModel()
        {
            CustomerList = new List<UserInfo>();

            InitCustomerListAsync();

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

        }

        private async void InitCustomerListAsync()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();
                CustomerListRD customerListRD = await _restSharpService.GetCustomers(GlobalVariables.LoggedUser.phone.ToString());

                if (customerListRD.result.Count > 0)
                {
                    List<UserInfo> temp = new List<UserInfo>();
                    foreach (var item in customerListRD.result)
                    {
                        temp.Add(item);
                    }
                    CustomerList = temp;

                    Visible = false;
                }
                else
                {
                    Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
