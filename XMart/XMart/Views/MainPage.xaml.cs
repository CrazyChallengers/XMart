using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMart.Views;
using XMart.Util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using XMart.Models;

namespace XMart.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Init();

            if (GlobalVariables.IsLogged)
            {
                Children.Add(new MePage());
            }
            else
            {
                Children.Add(new LoginPage());
            }
        }

        private void Init()
        {
            //初始化，检查是否存在已记住的密码
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
                    //Tel = tel;
                    //Pwd = pwd;
                    //IsRememberPwd = true;

                    //OnLogin();

                    GlobalVariables.LoggedUser = JsonConvert.DeserializeObject<UserInfo>(log["UserInfo"].ToString());
                    GlobalVariables.IsLogged = true;
                }
                else
                {
                    //input pwd
                    GlobalVariables.IsLogged = false;
                }
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
    }
}
