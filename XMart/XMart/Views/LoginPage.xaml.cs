using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Views;

namespace XMart.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();

            Navigation.PushModalAsync(registerPage);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {

        }
        
    }
}