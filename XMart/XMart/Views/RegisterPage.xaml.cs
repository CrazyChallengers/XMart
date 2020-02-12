using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Util;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RestService _restService = new RestService();
        RegisterViewModel registerViewModel = new RegisterViewModel();

        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = registerViewModel;
        }

        /// <summary>
        /// 发送验证码按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ACButton_Clicked(object sender, EventArgs e)
        {
            OnACButtonClicked();

            registerViewModel.myTimer =  new MyTimer { EndDate = DateTime.Now.Add(new TimeSpan(900000000)) };
            registerViewModel.LoadAsync();
        }

        /// <summary>
        /// 响应发送验证码
        /// </summary>
        private async void OnACButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(registerViewModel.Tel) || !Tools.IsPhoneNumber(registerViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("请检查手机号！", ToastLength.Long);
                return;
            }

            SimpleRD simpleRD = await _restService.SendAuthCode(registerViewModel.Tel);

            if (simpleRD.code == 200)
            {
                CrossToastPopUp.Current.ShowToastSuccess(simpleRD.message + "，请注意查收！", ToastLength.Long);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
            }
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                OnRegister();
            }
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(registerViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (!Tools.IsPhoneNumber(registerViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号格式错误，请重新输入！", ToastLength.Long);
                return false;
            }
            /*
            if (string.IsNullOrWhiteSpace(registerViewModel.UserName))
            {
                CrossToastPopUp.Current.ShowToastWarning("用户名不能为空，请输入！", ToastLength.Long);
                return false;
            }*/

            if (string.IsNullOrWhiteSpace(registerViewModel.Pwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(registerViewModel.SecondPwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("确认密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (registerViewModel.Pwd != registerViewModel.SecondPwd)
            {
                CrossToastPopUp.Current.ShowToastWarning("两次输入密码不一致，请检查！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(registerViewModel.InvitePhone))
            {
                CrossToastPopUp.Current.ShowToastWarning("邀请码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(registerViewModel.AuthCode))
            {
                CrossToastPopUp.Current.ShowToastWarning("验证码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (registerViewModel.AuthCode.Length < 6)
            {
                CrossToastPopUp.Current.ShowToastWarning("验证码长度为6位，请检查！", ToastLength.Long);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 注册
        /// </summary>
        private async void OnRegister()
        {
            RegisterPara registerPara = new RegisterPara
            {
                authCode = registerViewModel.AuthCode,
                tel = registerViewModel.Tel,
                userPwd = registerViewModel.Pwd,
                //userName = registerViewModel.UserName,
                invitePhone = registerViewModel.InvitePhone
            };

            registerPara.userType = CustomerRadio.IsChecked ? "0" : "1";

            SimpleRD simpleRD = await _restService.Register(registerPara);

            if (simpleRD.code == 200)
            {
                CrossToastPopUp.Current.ShowToastSuccess(simpleRD.message, ToastLength.Long);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
            }
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}