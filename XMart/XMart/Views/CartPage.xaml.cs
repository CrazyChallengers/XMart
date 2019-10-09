using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;

namespace XMart.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{
        CartViewModel cartViewModel = new CartViewModel();

		public CartPage ()
		{
			InitializeComponent ();

            List<string> list = new List<string>
            {
                "A","B","C","D","E","F","G","H","I","J","K"
            };

            cartViewModel.ItemList = list;
            cartViewModel.ItemNumber = list.Count().ToString();

            BindingContext = cartViewModel;
		}
	}
}