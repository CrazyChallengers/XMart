using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.Util;

namespace XMart.ViewModels
{
    public class EditUserInfoViewModel : BaseViewModel
    {
        private UserInfo user;   //Comment
        public UserInfo User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        public Command AddOrUpdateAddressCommand { get; set; }
        public Command BackCommand { get; set; }

        public EditUserInfoViewModel()
        {
            User = GlobalVariables.LoggedUser;

            AddOrUpdateAddressCommand = new Command(() =>
            {
                
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }
    }
}
