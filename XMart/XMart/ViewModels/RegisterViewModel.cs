using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Util;

namespace XMart.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string tel;   //手机号
        public string Tel
        {
            get { return tel; }
            set { SetProperty(ref tel, value); }
        }

        private string authCode;   //验证码
        public string AuthCode
        {
            get { return authCode; }
            set { SetProperty(ref authCode, value); }
        }

        private string userName;   //用户名
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string pwd;   //密码
        public string Pwd
        {
            get { return pwd; }
            set { SetProperty(ref pwd, value); }
        }

        private string secondPwd;   //确认密码
        public string SecondPwd
        {
            get { return secondPwd; }
            set { SetProperty(ref secondPwd, value); }
        }

        private string authCodeButtonText;   //comment
        public string AuthCodeButtonText
        {
            get { return authCodeButtonText; }
            set { SetProperty(ref authCodeButtonText, value); }
        }

        private string invitePhone;   //邀请手机号
        public string InvitePhone
        {
            get { return invitePhone; }
            set { SetProperty(ref invitePhone, value); }
        }

        private bool isEnable;   //可否点击
        public bool IsEnable
        {
            get { return isEnable; }
            set { SetProperty(ref isEnable, value); }
        }

        private Color buttonColor;   //发送验证码按钮颜色
        public Color ButtonColor
        {
            get { return buttonColor; }
            set { SetProperty(ref buttonColor, value); }
        }

        private bool IsDesignerChecked { get; set; }

        public long Tick { get; set; }
        public MyTimer myTimer { get; set; }
        RestService _restService = new RestService();

        public Command SendAuthCodeCommand { get; private set; }   //发送验证码
        public Command RegisterCommand { get; private set; }   //注册
        public Command<bool> SelectedIdentityCommand { get; private set; }   //选择注册身份
        public Command BackCommand { get; private set; }   //返回上一页

        public RegisterViewModel()
        {
            AuthCodeButtonText = "发送验证码";
            IsEnable = true;
            ButtonColor = Color.FromHex("FFCC00");

            SendAuthCodeCommand = new Command(() =>
            {
                OnACButtonClicked();

                myTimer = new MyTimer { EndDate = DateTime.Now.Add(new TimeSpan(900000000)) };
                LoadAsync();
            }, () => { return true; });

            RegisterCommand = new Command(() =>
            {
                if (CheckInput())
                {
                    OnRegister();
                }
            }, () => { return true; });

            SelectedIdentityCommand = new Command<bool>((bool isChecked) =>
            {
                IsDesignerChecked = isChecked;
                if (IsDesignerChecked)
                {
                    CrossToastPopUp.Current.ShowToastWarning("您将要注册成为设计师！", ToastLength.Long);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastWarning("您将要注册成为客户！", ToastLength.Long);
                }
            }, (bool fuck) => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        public void LoadAsync()
        {
            IsEnable = false;
            ButtonColor = Color.LightGray;
            myTimer.Start();
            myTimer.Ticked += OnCountdownTicked;
            myTimer.Completed += OnCountdownCompleted;
            //return Task.CompletedTask;
        }

        public Task UnloadAsync()
        {
            myTimer.Ticked -= OnCountdownTicked;
            myTimer.Completed -= OnCountdownCompleted;
            return Task.CompletedTask;
        }

        void OnCountdownTicked()
        {
            AuthCodeButtonText = string.Format("{0}秒后再试", myTimer.RemainTime.TotalSeconds.ToString().Split('.')[0]);
        }

        void OnCountdownCompleted()
        {
            AuthCodeButtonText = "发送验证码";
            IsEnable = true;
            ButtonColor = Color.FromHex("FFCC00");
            UnloadAsync();
        }

        /// <summary>
        /// 响应发送验证码
        /// </summary>
        private async void OnACButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(Tel) || !Tools.IsPhoneNumber(Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("请检查手机号！", ToastLength.Long);
                return;
            }

            SimpleRD simpleRD = await _restService.SendAuthCode(Tel);

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
            if (string.IsNullOrWhiteSpace(Tel))
            {
                CrossToastPopUp.Current.ShowToastWarning("手机号不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (!Tools.IsPhoneNumber(Tel))
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

            if (string.IsNullOrWhiteSpace(Pwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(SecondPwd))
            {
                CrossToastPopUp.Current.ShowToastWarning("确认密码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (Pwd != SecondPwd)
            {
                CrossToastPopUp.Current.ShowToastWarning("两次输入密码不一致，请检查！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(InvitePhone))
            {
                CrossToastPopUp.Current.ShowToastWarning("邀请码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (string.IsNullOrWhiteSpace(AuthCode))
            {
                CrossToastPopUp.Current.ShowToastWarning("验证码不能为空，请输入！", ToastLength.Long);
                return false;
            }

            if (AuthCode.Length < 6)
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
                authCode = AuthCode,
                tel = Tel,
                userPwd = Pwd,
                //userName = registerViewModel.UserName,
                invitePhone = InvitePhone
            };

            registerPara.userType = IsDesignerChecked ? "1" : "0";

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
    }
}
