using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMart.Util;

namespace XMart.ViewModels
{ 
    public class ResetPwdViewModel : BaseViewModel
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

        public long Tick { get; set; }

        public MyTimer myTimer { get; set; }

        public ResetPwdViewModel()
        {
            AuthCodeButtonText = "发送验证码";
            IsEnable = true;
            ButtonColor = Color.FromHex("FFCC00");
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
    }
}
