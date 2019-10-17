using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System.IO;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        LoginViewModel loginViewModel = new LoginViewModel();
        RestService _restService = new RestService();
        string fileName;

        public LoginPage ()
		{
			InitializeComponent ();

            BindingContext = loginViewModel;

            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");

            if (File.Exists(fileName))
            {
                string[] text = File.ReadAllLines(fileName);
                string check = text[0].Substring(6);
                string tel = text[1].Substring(8);
                string pwd = text[2].Substring(9);

                if (check == "Checked")
                {
                    loginViewModel.Tel = tel;
                    loginViewModel.Pwd = pwd;
                    RememberPwd.IsChecked = true;
                }
                else
                {
                    //input pwd
                }
            }
        }

        /// <summary>
        /// 跳转到注册页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();

            Navigation.PushModalAsync(registerPage);
        }

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                OnLogin();
            }
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(loginViewModel.Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(loginViewModel.Pwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        private async void OnLogin()
        {
            SimpleRD simpleRD = await _restService.Login(loginViewModel.Tel, loginViewModel.Pwd);

            if (simpleRD.code == 200)
            {
                CrossToastPopUp.Current.ShowToastSuccess(simpleRD.message, ToastLength.Long);

                MainPage mainPage = new MainPage();
                await Navigation.PushModalAsync(mainPage);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
            }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RememberPwd_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string text = "";

            if (!string.IsNullOrWhiteSpace(loginViewModel.Tel) && !string.IsNullOrWhiteSpace(loginViewModel.Pwd))
            {
                if (RememberPwd.IsChecked == true)
                {
                    text = "State:Checked\n" + "Account:" + account.Text + "\n" + "Password:" + password.Text;
                    File.WriteAllText(fileName, text);
                }
                else
                {
                    text = "State:Unchecked\nAccount:\nPassword:\n";
                    File.WriteAllText(fileName, text);
                }
            }
            else
            {
                //await DisplayAlert("错误", "请输入账号及密码！", "OK");
            }
        }

        /// <summary>
        /// 忘记密码tap事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FogotPwd_Tapped(object sender, EventArgs e)
        {
            ResetPwdPage resetPwdPage = new ResetPwdPage();

            Navigation.PushModalAsync(resetPwdPage);
        }
    }
}