using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ResponseData;
using XMart.ViewModels;
using XMart.Models;
using XMart.Services;
using XMart.Util;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPwdPage : ContentPage
    {
        RestService _restService = new RestService();
        ResetPwdViewModel resetPwdViewModel = new ResetPwdViewModel();

        public ResetPwdPage()
        {
            InitializeComponent();

            BindingContext = resetPwdViewModel;
        }

        /// <summary>
        /// 发送验证码按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ACButton_Clicked(object sender, EventArgs e)
        {
            OnACButtonClicked();

            resetPwdViewModel.myTimer = new MyTimer { EndDate = DateTime.Now.Add(new TimeSpan(900000000)) };
            resetPwdViewModel.LoadAsync();
        }

        /// <summary>
        /// 响应发送验证码
        /// </summary>
        private async void OnACButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(resetPwdViewModel.Tel) || !Tools.IsPhoneNumber(resetPwdViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("请检查手机号！", ToastLength.Long);
                return;
            }

            SimpleRD simpleRD = await _restService.SendAuthCode(resetPwdViewModel.Tel);

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
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(resetPwdViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (!Tools.IsPhoneNumber(resetPwdViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号格式错误，请重新输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(resetPwdViewModel.Pwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(resetPwdViewModel.SecondPwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("确认密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (resetPwdViewModel.Pwd != resetPwdViewModel.SecondPwd)
            {
                CrossToastPopUp.Current.ShowToastWarning("两次输入密码不一致，请检查！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(resetPwdViewModel.AuthCode))
            {
                CrossToastPopUp.Current.ShowToastWarning("验证码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (resetPwdViewModel.AuthCode.Length < 6)
            {
                CrossToastPopUp.Current.ShowToastWarning("验证码长度为6位，请检查！", ToastLength.Long);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 重置
        /// </summary>
        private async void OnReset()
        {
            ResetPwdPara resetPwdPara = new ResetPwdPara
            {
                authCode = resetPwdViewModel.AuthCode,
                tel = resetPwdViewModel.Tel,
                newPwd = resetPwdViewModel.Pwd
            };

            SimpleRD simpleRD = await _restService.ResetPwd(resetPwdPara);

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

        /// <summary>
        /// 重置按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                OnReset();
            }
        }
    }
}