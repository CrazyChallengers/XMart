using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Views;
using XMart.Util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        private bool authVisible;   //Comment
        public bool AuthVisible
        {
            get { return authVisible; }
            set { SetProperty(ref authVisible, value); }
        }

        private bool passwordVisible;   //Comment
        public bool PasswordVisible
        {
            get { return passwordVisible; }
            set { SetProperty(ref passwordVisible, value); }
        }

        private string authLoginButtonColor;   //Comment
        public string AuthLoginButtonColor
        {
            get { return authLoginButtonColor; }
            set { SetProperty(ref authLoginButtonColor, value); }
        }

        private bool authCodeButtonEnable;   //Comment
        public bool AuthCodeButtonEnable
        {
            get { return authCodeButtonEnable; }
            set { SetProperty(ref authCodeButtonEnable, value); }
        }

        private RestSharpService _restSharpService = new RestSharpService();
        string fileName;

        public Command ToRegisterPageCommand { get; private set; }   //跳转到注册页面
        public Command LoginCommand { get; private set; }   //登录按钮
        public Command FindPwdCommand { get; private set; }   //跳转到找回密码页面
        public Command OpenEyeCommand { get; private set; }
        public Command CheckPhoneCommand { get; set; }
        public Command ToAuthPageCommand { get; set; }
        public Command PasswordLoginPartCommand { get; set; }
        public Command AuthLoginPartCommand { get; set; }
        public Command WechatLoginCommand { get; set; }

        public LoginViewModel()
        {
            IsPassword = true;
            EyeSource = "Resource/drawable/closed_eye.png";
            AuthLoginButtonColor = "#83d7f9";
            AuthCodeButtonEnable = false;
            AuthVisible = true;
            PasswordVisible = false;

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
                    EyeSource = "Resource/drawable/closed_eye.png";
                }
            }, () => { return true; });

            CheckPhoneCommand = new Command(() =>
            {
                if (Tools.IsPhoneNumber(Tel))
                {
                    AuthLoginButtonColor = "#01acf2";
                    AuthCodeButtonEnable = true;
                }
                else
                {
                    AuthLoginButtonColor = "#83d7f9";
                    AuthCodeButtonEnable = false;
                }
            }, () => { return true; });

            ToAuthPageCommand = new Command(() =>
            {
                AuthCodePage authCodePage = new AuthCodePage(Tel);

                Application.Current.MainPage.Navigation.PushModalAsync(authCodePage);
            }, () => { return true; });

            PasswordLoginPartCommand = new Command(() =>
            {
                AuthVisible = false;
                PasswordVisible = true;
            }, () => { return true; });

            AuthLoginPartCommand = new Command(() =>
            {
                AuthVisible = true;
                PasswordVisible = false;
            }, () => { return true; });

            WechatLoginCommand = new Command(() =>
            {
                MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果。
                MessagingCenter.Send(new object(), "Login");
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
                userPwd = Pwd,
                authCode = "",
                tel = Tel
            };

            LoginRD loginRD = await _restSharpService.Login(loginPara);

            if (loginRD.result.message == null)
            {
                CrossToastPopUp.Current.ShowToastSuccess("欢迎您登录美而好家具！", ToastLength.Long);

                GlobalVariables.LoggedUser = loginRD.result;   //将登录用户的信息保存成全局静态变量
                GlobalVariables.IsLogged = true;

                JObject log = new JObject();
                log.Add("LoginTime", DateTime.UtcNow);
                log.Add("UserInfo", JsonConvert.SerializeObject(loginRD.result));
                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");
                File.WriteAllText(fileName, log.ToString());

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
