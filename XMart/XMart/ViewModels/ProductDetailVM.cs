using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Util;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Rg.Plugins.Popup.Services;
using XMart.Views;

namespace XMart.ViewModels
{
    public class ProductDetailVM : BaseViewModel
    {
        private ProductInfo product;   //comment
        public ProductInfo Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
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

        public Command BackCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        public Command BuyCommand { get; set; }
        public Command ShareCommand { get; set; }
        RestService _restService = new RestService();

        public ProductDetailVM(string productId)
        {
            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            AddToCartCommand = new Command(() =>
            {
                var page = new AddToCartView(Product);

                PopupNavigation.Instance.PushAsync(page);
            }, () => { return true; });

            BuyCommand = new Command(() =>
            {
                if (GlobalVariables.IsLogged)
                {
                    List<CartItemInfo> productList = new List<CartItemInfo>();

                    CartItemInfo cartItemInfo = new CartItemInfo();
                    Tools.AutoMapping<ProductInfo, CartItemInfo>(Product, cartItemInfo);
                    cartItemInfo.productNum = 1;
                    cartItemInfo.productImg = Product.productImageBig;

                    productList.Add(cartItemInfo);

                    OrderingPage orderingPage = new OrderingPage(productList);
                    Application.Current.MainPage.Navigation.PushModalAsync(orderingPage);
                }
                else
                {
                    LoginPage loginPage = new LoginPage();
                    Application.Current.MainPage.Navigation.PushModalAsync(loginPage);
                }

            }, () => { return true; });

            ShareCommand = new Command(() =>
            {
                string para = "?productId=" + Product.productId + "&userId=" + GlobalVariables.LoggedUser.id;
                MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果。
                MessagingCenter.Send(new object(), "ShareToFriend", para);
            }, () => { return true; });

            InitProductDetailPageAsync(productId);
        }

        /// <summary>
        /// 获取商品详细信息，初始化页面
        /// </summary>
        /// <param name="productId"></param>
        private async void InitProductDetailPageAsync(string productId)
        {
            try
            {
                ProductDetailRD productDetailRD = await _restService.GetProductDetail(productId);

                if (productDetailRD.result != null)
                {
                    Product = productDetailRD.result;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
