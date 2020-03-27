using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XMart.Models;
using XMart.Services;
using XMart.ResponseData;
using Xamarin.Forms;

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

        public Command BackCommand { get; set; }

        RestSharpService _restSharpService = new RestSharpService();

        public CollectionViewModel()
        {
            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

            Init();
        }

        public async void Init()
        {
            try
            {
                ProductListRD productListRD = await _restSharpService.GetCollections();

                ProductList = new ObservableCollection<ProductListItem>();
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
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
