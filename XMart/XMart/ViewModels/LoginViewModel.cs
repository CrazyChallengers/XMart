using System;
using System.Collections.Generic;
using System.Text;
using XMart.Views;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using System.IO;

namespace XMart.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string tel;   //手机号
        public string Tel
        {
            get { return tel; }
            set { SetProperty(ref tel, value); }
        }

        private string pwd;   //密码
        public string Pwd
        {
            get { return pwd; }
            set { SetProperty(ref pwd, value); }
        }

        private bool isRememberPwd;   //是否记住密码
        public bool IsRememberPwd
        {
            get { return isRememberPwd; }
            set { SetProperty(ref isRememberPwd, value); }
        }

        private string eyeSource;   //Comment
        public string EyeSource
        {
            get { return eyeSource; }
            set { SetProperty(ref eyeSource, value); }
        }

        private bool isPassword;   //Comment
        public bool IsPassword
        {
            get { return isPassword; }
            set { SetProperty(ref isPassword, value); }
        }



        private RestService _restService = new RestService();
        string fileName;

        public Command ToRegisterPageCommand { get; private set; }   //跳转到注册页面
        public Command LoginCommand { get; private set; }   //登录按钮
        public Command RememberPwdCommand { get; private set; }   //记住密码
        public Command FindPwdCommand { get; private set; }   //跳转到找回密码页面
        public Command OpenEyeCommand { get; private set; }

        public LoginViewModel()
        {
            IsPassword = true;
            EyeSource = "Resource/drawable/close_eye.png";
            //初始化，检查是否存在已记住的密码
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");

            if (File.Exists(fileName))
            {
                string[] text = File.ReadAllLines(fileName);
                string check = text[0].Substring(6);
                string tel = text[1].Substring(8);
                string pwd = text[2].Substring(9);

                if (check == "Checked")
                {
                    Tel = tel;
                    Pwd = pwd;
                    IsRememberPwd = true;
                }
                else
                {
                    //input pwd
                }
            }

            ToRegisterPageCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
            }, () => { return true; });

            LoginCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    OnLogin();
                }
            }, () => { return true; });

            RememberPwdCommand = new Command(() =>
            {
                string text = "";

                if (!string.IsNullOrWhiteSpace(Tel) && !string.IsNullOrWhiteSpace(Pwd))
                {
                    if (IsRememberPwd)
                    {
                        text = "State:Checked\n" + "Account:" + Tel + "\n" + "Password:" + Pwd;
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
            }, () => { return true; });

            FindPwdCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushModalAsync(new ResetPwdPage());
            }, () => { return true; });

            OpenEyeCommand = new Command(() =>
            {
                if (IsPassword)
                {
                    IsPassword = false;
                    EyeSource = "Resource/drawable/open_eye.png";
                }
                else
                {
                    IsPassword = true;
                    EyeSource = "Resource/drawable/close_eye.png";
                }
            }, () => { return true; });

        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Pwd))
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
            LoginPara loginPara = new LoginPara
            {
                //userName = loginViewModel.Tel,
                userPwd = Pwd,
                authCode = "",
                tel = Tel
            };

            LoginRD loginRD = await _restService.Login(loginPara);

            /*
            if (loginRD.code == 200)
            {
                CrossToastPopUp.Current.ShowToastSuccess(loginRD.message, ToastLength.Long);

                MainPage mainPage = new MainPage();
                await Navigation.PushModalAsync(mainPage);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(loginRD.message, ToastLength.Long);
            }*/
            if (loginRD.result.message == null)
            {
                CrossToastPopUp.Current.ShowToastSuccess(loginRD.message, ToastLength.Long);

                MainPage mainPage = new MainPage();
                await Application.Current.MainPage.Navigation.PushModalAsync(mainPage);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(loginRD.result.message, ToastLength.Long);
            }
        }

    }
}
