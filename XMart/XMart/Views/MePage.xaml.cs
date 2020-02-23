using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Util;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MePage : ContentPage
	{
        MePageViewModel mePageViewModel = new MePageViewModel();

		public MePage ()
		{
			InitializeComponent ();

            BindingContext = mePageViewModel;

		}

    }
}