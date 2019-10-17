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

        public CartPage()
        {
            InitializeComponent();

            InitCart();

            BindingContext = cartViewModel;
        }

        /// <summary>
        /// 初始化购物车
        /// </summary>
        private async void InitCart()
        {
            CartItemListRD cartItemListRD = await _restService.GetCartItemList("1");

            cartViewModel.ItemList = cartItemListRD.data;
            cartViewModel.ItemNumber = cartItemListRD.data.Count().ToString();
        }

        /// <summary>
        /// 单选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            double totalPrice = 0;
            int number = 0;
            foreach (var item in cartViewModel.ItemList)
            {
                if (item.IsChecked)
                {
                    totalPrice += (item.price * item.quantity);
                    number += item.quantity;
                }
                else
                {
                    cartViewModel.IsAllChecked = false;
                }
            }

            cartViewModel.TotalSelectedPrice = totalPrice.ToString();
            cartViewModel.CheckedNumber = number;
        }

        /// <summary>
        /// 全选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckedButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            double totalPrice = 0;
            int number = 0;

            if (cartViewModel.IsAllChecked)
            {
                foreach (var item in cartViewModel.ItemList)
                {
                    item.IsChecked = true;
                    totalPrice += (item.price * item.quantity);
                    number += item.quantity;
                }
            }
            else
            {
                foreach (var item in cartViewModel.ItemList)
                {
                    item.IsChecked = false;
                }
            }

            cartViewModel.TotalSelectedPrice = totalPrice.ToString();
            cartViewModel.CheckedNumber = number;
        }

        /// <summary>
        /// 结算按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderButton_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}