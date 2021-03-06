﻿using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMart.Util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using XMart.Models;
using Xamarin.Essentials;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Forms.Xaml;

namespace XMart.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            InitAsync();

        }

        private void InitAsync()
        {
            if (!Tools.IsNetConnective())
            {
                CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                return;
            }

            //var status = CheckAndRequestPermissionAsync();
            //if (status.Result != PermissionStatus.Granted)
            //{
            //    GlobalVariables.IsLogged = false;
            //}

            //NetErrorPage.IsVisible = false;
            Children.Remove(NetErrorPage);
            Children.Add(new HomePage());
            Children.Add(new CategoryPage());

            //初始化，检查是否存在已记住的密码
            /*
            UserInfo userInfo = await App.Database.GetUserInfo();
            if (userInfo != null)
            {
                GlobalVariables.LoggedUser = userInfo;
                GlobalVariables.IsLogged = true;
            }
            else
            {
                GlobalVariables.IsLogged = false;
            }*/

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                JObject log = (JObject)JsonConvert.DeserializeObject(text);

                string loginTime = log["LoginTime"].ToString();
                DateTime lastLoginTime = DateTime.Parse(loginTime);
                DateTime nowTime = DateTime.UtcNow;
                TimeSpan span = nowTime.Subtract(lastLoginTime);
                int dayDiff = span.Days + 1;

                if (dayDiff <= 30)
                {
                    GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<UserInfo>(log["UserInfo"].ToString());
                    GlobalVariables.IsLogged = true;
                }
                else
                {
                    GlobalVariables.IsLogged = false;
                }
            }
            else
            {
                GlobalVariables.IsLogged = false;
            }

            if (GlobalVariables.IsLogged)
            {
                Children.Add(new CartPage());
                Children.Add(new MePage());
            }
            else
            {
                Children.Add(new CartPage());
                Children.Add(new LoginPage());
            }
        }

        /// <summary>
        /// 返回键事件，禁止返回
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async Task<PermissionStatus> CheckAndRequestPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            // Additionally could prompt the user to turn on in settings

            return status;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            InitAsync();
        }
    }
}
