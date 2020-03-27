using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using XMart.Util;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        RestSharpService _restSharpService = new RestSharpService();
        CartViewModel cartViewModel = new CartViewModel();

        public CartPage()
        {
            InitializeComponent();

            BindingContext = cartViewModel;
        }

        /// <summary>
        /// 初始化购物车
        /// </summary>
        private async void InitCart()
        {
            if (GlobalVariables.IsLogged)
            {
                string memberId = GlobalVariables.LoggedUser.id.ToString();
                CartItemListRD cartItemListRD = await _restSharpService.GetCartItemList(memberId);

                cartViewModel.ItemList = cartItemListRD.result;
                cartViewModel.ItemNumber = cartItemListRD.result.Count().ToString();
                OnCount();
            }
        }

        /// <summary>
        /// 单选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            OnCount();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnCount()
        {
            double totalPrice = 0;
            int number = 0;
            bool allChecked = true;
            foreach (var item in cartViewModel.ItemList)
            {
                if (item.Checked)
                {
                    totalPrice += (item.memberPrice * item.productNum);
                    number += item.productNum;
                }

                allChecked &= item.Checked;
            }

            cartViewModel.IsAllChecked = allChecked;
            cartViewModel.AllCheckedButton_Color = allChecked ? Color.Crimson : Color.LightGray;
            cartViewModel.TotalSelectedPrice = totalPrice.ToString();
            cartViewModel.CheckedNumber = number;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            InitCart();
        }

        private async void TwoTapped_TappedAsync(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("选择操作", "取消", null, "删除", "修改");

            if (action == "删除")
            {
                Frame frame = sender as Frame;
                int index = ItemStack.Children.IndexOf(frame);

                StupidRD stupidRD = await _restSharpService.DeleteItemInCart(cartViewModel.ItemList[index]);

                if (stupidRD.success)
                {
                    CrossToastPopUp.Current.ShowToastSuccess("删除成功！", ToastLength.Short);

                    InitCart();
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("删除失败！", ToastLength.Short);
                }
            }
        }
    }
}