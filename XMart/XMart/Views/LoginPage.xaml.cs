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

        public LoginPage()
		{
			InitializeComponent ();

            BindingContext = loginViewModel;
        }

    }
}