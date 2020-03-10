using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.ResponseData;
using XMart.Util;
using XMart.Services;
using System.Threading.Tasks;
using XMart.Models;
using XMart.Views;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace XMart.ViewModels
{
    public class AuthCodeViewModel : BaseViewModel
    {
        private string authCode;   //Comment
        public string AuthCode
        {
            get { return authCode; }
            set { SetProperty(ref authCode, value); }
        }

        private string secretTel;   //Comment
        public string SecretTel
        {
            get { return secretTel; }
            set { SetProperty(ref secretTel, value); }
        }

        private string remainingTime;   //Comment
        public string RemainingTime
        {
            get { return remainingTime; }
            set { SetProperty(ref remainingTime, value); }
        }

        private bool authCodeButtonEnable;   //Comment
        public bool AuthCodeButtonEnable
        {
            get { return authCodeButtonEnable; }
            set { SetProperty(ref authCodeButtonEnable, value); }
        }

        private string authLoginButtonColor;   //Comment
        public string AuthLoginButtonColor
        {
            get { return authLoginButtonColor; }
            set { SetProperty(ref authLoginButtonColor, value); }
        }

        private string Tel { get; set; }
        public long Tick { get; set; }
        public MyTimer myTimer { get; set; }
        RestSharpService _restSharpService = new RestSharpService();

        public Command LoginCommand { get; set; }
        public Command CheckInputCommand { get; set; }
        public Command BackCommand { get; set; }

        public AuthCodeViewModel(string tel)
        {
            Tel = tel;
            SecretTel = Tel.Substring(0, 3) + "****" + Tel.Substring(7, 4);
            AuthCodeButtonEnable = false;
            AuthLoginButtonColor = "#83d7f9";

            SendAuthCode();

            LoginCommand = new Command(() =>
            {
                OnLogin();

            }, () => { return true; });

            CheckInputCommand = new Command(() =>
            {
                if (AuthCode.Length == 6)
                {
                    AuthCodeButtonEnable = true;
                    AuthLoginButtonColor = "#01acf2";
                }
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        /// <summary>
        /// 响应发送验证码
        /// </summary>
        private async void SendAuthCode()
        {
            SimpleRD simpleRD = await _restSharpService.SendAuthCode(Tel);

            if (simpleRD.code == 200)
            {
                myTimer = new MyTimer { EndDate = DateTime.Now.Add(new TimeSpan(900000000)) };
                LoadAsync();
                CrossToastPopUp.Current.ShowToastSuccess(simpleRD.message + "，请注意查收！", ToastLength.Long);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        private async void OnLogin()
        {
            LoginPara loginPara = new LoginPara
            {
                authCode = AuthCode,
                tel = Tel
            };

            LoginRD loginRD = await _restSharpService.LoginByAuthCode(loginPara);

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

                GlobalVariables.LoggedUser = loginRD.result;   //将登录用户的信息保存成全局静态变量
                GlobalVariables.IsLogged = true;

                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");
                JObject log = new JObject();
                log.Add("LoginTime", DateTime.UtcNow);
                log.Add("UserInfo", JsonConvert.SerializeObject(loginRD.result));
                //string text = "State:Checked\n" + "Account:" + Tel + "\nPassword:" + loginRD.result + "\nLoginTime:" + DateTime.UtcNow;
                File.WriteAllText(fileName, log.ToString());

                MainPage mainPage = new MainPage();
                await Application.Current.MainPage.Navigation.PushModalAsync(mainPage);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError(loginRD.result.message, ToastLength.Long);
            }
        }

        public void LoadAsync()
        {
            //IsEnable = false;
            //ButtonColor = Color.LightGray;
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
            RemainingTime = string.Format("{0}", myTimer.RemainTime.TotalSeconds.ToString().Split('.')[0]);
        }

        void OnCountdownCompleted()
        {
            //AuthCodeButtonEnable = true;
            //ButtonColor = Color.FromHex("FFCC00");
            UnloadAsync();
        }
    }
}
