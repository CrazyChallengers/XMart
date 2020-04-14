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
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;

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

        private string starSource;   //Comment
        public string StarSource
        {
            get { return starSource; }
            set { SetProperty(ref starSource, value); }
        }

        private bool isCollected;   //Comment
        public bool IsCollected
        {
            get { return isCollected; }
            set { SetProperty(ref isCollected, value); }
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
        public Command CallServiceCommand { get; set; }
        public Command StarCommand { get; set; }
        public Command SpeakCommand { get; set; }

        RestSharpService _restSharpService = new RestSharpService();

        public ProductDetailVM(string productId)
        {
            //CusPriceVisible = GlobalVariables.LoggedUser.userType == "0";
            MemberPriceVisible = GlobalVariables.IsLogged;

            Product = new ProductInfo();
            StarSource = "star_gray.png";
            IsCollected = false;
            CusPriceVisible = false;

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            AddToCartCommand = new Command(() =>
            {
                if (GlobalVariables.IsLogged)
                {
                    var page = new AddToCartView(Product);
                    PopupNavigation.Instance.PushAsync(page);
                }
                else
                {
                    LoginPage loginPage = new LoginPage();
                    Application.Current.MainPage.Navigation.PushModalAsync(loginPage);
                }
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
                if (GlobalVariables.IsLogged)
                {
                    string para = "?productId=" + Product.productId + "&userId=" + GlobalVariables.LoggedUser.id;
                    MessagingCenter.Send(new object(), "Register");//首先进行注册，然后订阅注册的结果。
                    MessagingCenter.Send(new object(), "ShareToFriend", para);
                }
                else
                {
                    LoginPage loginPage = new LoginPage();
                    Application.Current.MainPage.Navigation.PushModalAsync(loginPage);
                }
            }, () => { return true; });

            CallServiceCommand = new Command(() =>
            {
                try
                {
                    PhoneDialer.Open("18080961008");
                }
                catch (ArgumentNullException anEx)
                {
                    // Number was null or white space
                    CrossToastPopUp.Current.ShowToastError("无联系方式", ToastLength.Short);
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Phone Dialer is not supported on this device.
                    CrossToastPopUp.Current.ShowToastError("该设备不支持拨号", ToastLength.Short);
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                    CrossToastPopUp.Current.ShowToastError("出现其他错误", ToastLength.Short);
                }
            }, () => { return true; });

            StarCommand = new Command(() =>
            {
                Collect();
            }, () => { return true; });

            SpeakCommand = new Command(() =>
            {
                if (GlobalVariables.IsLogged)
                {
                    DependencyService.Get<ITextToSpeech>().Speak(Product.productName + " 市场价" + Product.mallPrice + "元 会员价" + Product.memberPrice + "元");
                }
                else
                {
                    DependencyService.Get<ITextToSpeech>().Speak(Product.productName + " 市场价" + Product.mallPrice + "元");
                }
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
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                ProductDetailRD productDetailRD = await _restSharpService.GetProductDetail(productId);

                if (GlobalVariables.IsLogged)
                {
                    string judgeRD = await _restSharpService.JudgeCollection(productId);

                    var json = JObject.Parse(judgeRD);
                    isCollected = (bool)json["success"];
                }
                else
                {
                    isCollected = false;
                }

                if (productDetailRD.result != null)
                {
                    Product = productDetailRD.result;
                    StarSource = isCollected ? "star_yellow.png" : "star_gray.png";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 收藏
        /// </summary>
        private async void Collect()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                if (!GlobalVariables.IsLogged)
                {
                    LoginPage loginPage = new LoginPage();
                    await Application.Current.MainPage.Navigation.PushModalAsync(loginPage);
                    return;
                }

                if (IsCollected)
                {
                    //取消收藏
                    StupidRD stupidRD = await _restSharpService.DeleteCollection(Product.productId.ToString());

                    if (stupidRD.success)
                    {
                        CrossToastPopUp.Current.ShowToastSuccess("取消收藏成功", ToastLength.Short);
                        StarSource = "star_gray.png";
                        IsCollected = false;
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastWarning("取消收藏失败", ToastLength.Short);
                    }
                }
                else
                {
                    //收藏
                    StupidRD stupidRD = await _restSharpService.AddToCollection(Product.productId.ToString());

                    if (stupidRD.success)
                    {
                        CrossToastPopUp.Current.ShowToastSuccess("收藏成功", ToastLength.Short);
                        StarSource = "star_yellow.png";
                        IsCollected = true;
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastWarning("收藏失败", ToastLength.Short);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
