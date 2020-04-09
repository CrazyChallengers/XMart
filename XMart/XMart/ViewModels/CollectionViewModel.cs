using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XMart.Models;
using XMart.Services;
using XMart.ResponseData;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Util;
using XMart.Views;

namespace XMart.ViewModels
{
    public class CollectionViewModel : BaseViewModel
    {
        private ObservableCollection<ProductListItem> productList;   //产品列表
        public ObservableCollection<ProductListItem> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }

        private int  productNum;   //Comment
        public int  ProductNum
        {
            get { return productNum; }
            set { SetProperty(ref productNum, value); }
        }

        private bool visible;   //Comment
        public bool Visible
        {
            get { return visible; }
            set { SetProperty(ref visible, value); }
        }

        private bool indicatorIsRunning;   //Comment
        public bool IndicatorIsRunning
        {
            get { return indicatorIsRunning; }
            set { SetProperty(ref indicatorIsRunning, value); }
        }

        public Command BackCommand { get; set; }
        public Command<long> TappedCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public CollectionViewModel()
        {
            ProductList = new ObservableCollection<ProductListItem>();

            Init();

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            TappedCommand = new Command<long>((productId) =>
            {
                ProductDetailPage productDetailPage = new ProductDetailPage(productId.ToString());
                Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            }, (productId) => { return true; });

            RefreshCommand = new Command(() =>
            {
                Init();
            }, () => { return true; });
        }

        public async void Init()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }
                IndicatorIsRunning = true;
                ProductList.Clear();

                RestSharpService _restSharpService = new RestSharpService();
                ProductListRD productListRD = await _restSharpService.GetCollections();

                if (productListRD.result.total > 0)
                {
                    foreach (var item in productListRD.result.data)
                    {
                        ProductList.Add(item);
                    }
                    ProductNum = productListRD.result.total;
                    Visible = false;
                }
                else
                {
                    Visible = true;
                }
                IndicatorIsRunning = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
