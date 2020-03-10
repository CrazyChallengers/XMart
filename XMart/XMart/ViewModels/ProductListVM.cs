using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using Xamarin.Forms;

namespace XMart.ViewModels
{
    public class ProductListVM : BaseViewModel
    {
        private List<ProductListItem> productList;   //产品列表
        public List<ProductListItem> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }

        private string index;   //Comment
        public string Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        public Command SearchCommand { get; set; }
        public Command BackCommand { get; set; }

        public ProductListVM(Category subCategoryInfo)
        {
            GetProductList(subCategoryInfo);

            SearchCommand = new Command(() =>
            {
                GetProductList(Index);
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

        }

        public ProductListVM(string _index)
        {
            Index = _index;
            GetProductList(Index);

            SearchCommand = new Command(() =>
            {
                GetProductList(Index);
            }, () => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        private async void GetProductList(string index)
        {
            int page = 1;
            int size = 20;
            string sort = "1";
            int sequence = 1;
            int priceGt = -1;
            int priceLte = -1;

            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.FuzzySearch(index, sequence, page, size, sort, priceGt, priceLte);

            ProductList = productListRD.result.data;
        }

        private async void GetProductList(Category subCategoryInfo)
        {
            int page = 1;
            int size = 20;
            string sort = "1";
            long cid = subCategoryInfo.id;
            int priceGt = -1;
            int priceLte = -1;
            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.GetProductList(page, size, sort, cid, priceGt, priceLte);

            ProductList = productListRD.result.data;
        }

    }
}
