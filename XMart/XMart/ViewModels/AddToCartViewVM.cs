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

		private int productNum;   //Comment
		public int ProductNum
		{
			get { return productNum; }
			set { SetProperty(ref productNum, value); }
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

		public AddToCartViewVM(ProductInfo productInfo)
		{
            Product = productInfo;

            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;

            ProductNum = 1;

            AddToCartCommand = new Command(() =>
			{
                if (GlobalVariables.IsLogged)
                {
                    AddToCartAsync();
                }
            }, () => { return true; });
		}

        private async void AddToCartAsync()
        {
            try
            {
                RestService _restService = new RestService();

                string memberId = GlobalVariables.LoggedUser.id.ToString();
                string productId = Product.productId.ToString();
                string num = ProductNum.ToString();

                SimpleRD simpleRD = await _restService.AddToCart(memberId, productId, num);

                if (simpleRD.message == "success")
                {
                    CrossToastPopUp.Current.ShowToastSuccess("已添加到购物车！", ToastLength.Short);
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastError("添加到购物车失败！", ToastLength.Long);
                }

                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
