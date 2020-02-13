using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System.IO;
using XMart.Models;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        LoginViewModel loginViewModel = new LoginViewModel();
        //RestService _restService = new RestService();
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
                    loginViewModel.IsRememberPwd = true;
                }
                else
                {
                    //input pwd
                }
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

    }
}