using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{
        RestService _restService = new RestService();
        CartViewModel cartViewModel = new CartViewModel();

		public CartPage ()
		{
			InitializeComponent ();

            InitCart();

            BindingContext = cartViewModel;
		}

        private async void InitCart()
        {
            CartItemListRD cartItemListRD = await _restService.GetCartItemList("1");

            cartViewModel.ItemList = cartItemListRD.data;
            cartViewModel.ItemNumber = cartItemListRD.data.Count().ToString();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}