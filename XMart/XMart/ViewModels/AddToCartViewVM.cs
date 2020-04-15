using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;
using XMart.ResponseData;
using XMart.Util;
using XMart.Services;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XMart.Views;

namespace XMart.ViewModels
{
    public class AddToCartViewVM : BaseViewModel
    {
		private ProductInfo product;   //Comment
		public ProductInfo Product
		{
			get { return product; }
			set { SetProperty(ref product, value); }
		}

        private string mallPrice;   //Comment
        public string MallPrice
        {
            get { return mallPrice; }
            set { SetProperty(ref mallPrice, value); }
        }

        private string memberPrice;   //Comment
        public string MemberPrice
        {
            get { return memberPrice; }
            set { SetProperty(ref memberPrice, value); }
        }

        private int productNum;   //Comment
		public int ProductNum
		{
			get { return productNum; }
			set { SetProperty(ref productNum, value); }
		}

        private int index;   //Comment
        public int Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        private bool memberPriceVisible;   //Comment
        public bool MemberPriceVisible
        {
            get { return memberPriceVisible; }
            set { SetProperty(ref memberPriceVisible, value); }
        }

        private bool cusPriceVisile;   //Comment
        public bool CusPriceVisible
        {
            get { return cusPriceVisile; }
            set { SetProperty(ref cusPriceVisile, value); }
        }

        public Command AddToCartCommand { get; set; }
        public Command SelectAttributeCommand { get; set; }
        public Command ToOrderCommand { get; set; }

        public AddToCartViewVM(ProductInfo productInfo)
		{
            Product = productInfo;
            MallPrice = Product.itemAttributeValues[0].mallPrice.ToString();
            MemberPrice = Product.itemAttributeValues[0].memberPrice.ToString();
            Index = 0;

            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;

            ProductNum = 1;

            AddToCartCommand = new Command(async () =>
			{
                if (GlobalVariables.IsLogged)
                {
                    AddToCartAsync();
                }
                await PopupNavigation.Instance.PopAsync();
            }, () => { return true; });

            ToOrderCommand = new Command(async () =>
            {
                if (GlobalVariables.IsLogged)
                {
                    List<CartItemInfo> productList = new List<CartItemInfo>();

                    CartItemInfo cartItemInfo = new CartItemInfo();
                    Tools.AutoMapping<ProductInfo, CartItemInfo>(Product, cartItemInfo);
                    cartItemInfo.productNum = 1;
                    cartItemInfo.productImg = Product.productImageBig;
                    cartItemInfo.attributesValues = Product.itemAttributeValues[Index].attributeValue;

                    productList.Add(cartItemInfo);

                    OrderingPage orderingPage = new OrderingPage(productList);
                    await PopupNavigation.Instance.PopAsync();
                    await Application.Current.MainPage.Navigation.PushModalAsync(orderingPage);
                }
                else
                {
                    LoginPage loginPage = new LoginPage();
                    await PopupNavigation.Instance.PopAsync();
                    await Application.Current.MainPage.Navigation.PushModalAsync(loginPage);
                }

            }, () => { return true; });

            SelectAttributeCommand = new Command(() =>
            {
                MallPrice = Product.itemAttributeValues[Index].mallPrice.ToString();
                MemberPrice = Product.itemAttributeValues[Index].memberPrice.ToString();
            }, () => { return true; });
        }

        private async void AddToCartAsync()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();

                string memberId = GlobalVariables.LoggedUser.id.ToString();
                string productId = Product.productId.ToString();
                string num = ProductNum.ToString();
                string attributeValue = Product.itemAttributeValues[Index].attributeValue;

                SimpleRD simpleRD = await _restSharpService.AddToCart(memberId, productId, num, attributeValue);

                if (simpleRD.message == "success")
                {
                    CrossToastPopUp.Current.ShowToastSuccess("已添加到购物车！", ToastLength.Short);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("添加到购物车失败！", ToastLength.Long);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
