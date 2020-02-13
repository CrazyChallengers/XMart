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
        RegisterViewModel registerViewModel = new RegisterViewModel();

        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = registerViewModel;
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