using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Views;
using System.Collections.ObjectModel;
using XMart.Util;

namespace XMart.ViewModels
{
    public class ProductListVM : BaseViewModel
    {
        private ObservableCollection<ProductListItem> productList;   //产品列表
        public ObservableCollection<ProductListItem> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }

        private int productNum;   //Comment
        public int ProductNum
        {
            get { return productNum; }
            set { SetProperty(ref productNum, value); }
        }

        private int totalProductNum;   //Comment
        public int TotalProductNum
        {
            get { return totalProductNum; }
            set { SetProperty(ref totalProductNum, value); }
        }

        private string searchString;   //Comment
        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

        private string loadMoreButtonText;   //Comment
        public string LoadMoreButtonText
        {
            get { return loadMoreButtonText; }
            set { SetProperty(ref loadMoreButtonText, value); }
        }

        private bool buttonIsEnable;   //Comment
        public bool ButtonIsEnable
        {
            get { return buttonIsEnable; }
            set { SetProperty(ref buttonIsEnable, value); }
        }

        private bool indicatorIsRunning;   //Comment
        public bool IndicatorIsRunning
        {
            get { return indicatorIsRunning; }
            set { SetProperty(ref indicatorIsRunning, value); }
        }

        private int page;   //Comment
        public int Page
        {
            get { return page; }
            set { SetProperty(ref page, value); }
        }

        private int size;   //Comment
        public int _Size
        {
            get { return size; }
            set { SetProperty(ref size, value); }
        }

        private string sort;   //Comment
        public string Sort
        {
            get { return sort; }
            set { SetProperty(ref sort, value); }
        }

        private int sequence;   //Comment
        public int Sequence
        {
            get { return sequence; }
            set { SetProperty(ref sequence, value); }
        }

        private string priceGt;   //Comment
        public string PriceGt
        {
            get { return priceGt; }
            set { SetProperty(ref priceGt, value); }
        }

        private string priceLte;   //Comment
        public string PriceLte
        {
            get { return priceLte; }
            set { SetProperty(ref priceLte, value); }
        }

        public Command SearchCommand { get; set; }
        public Command BackCommand { get; set; }
        //public Command<ProductListItem> TappedCommand { get; set; }
        public Command LoadMoreCommand { get; set; }
        public Command<string> SortCommand { get; set; }
        public Command<string> PriceRangeCommand { get; set; }

        /// <summary>
        /// 点击分类进入商品列表
        /// </summary>
        /// <param name="subCategoryInfo"></param>
        public ProductListVM(Category subCategoryInfo)
        {
            ProductList = new ObservableCollection<ProductListItem>();
            ProductNum = 0;
            TotalProductNum = 0;
            Page = 1;
            _Size = 20;
            Sort = "1";
            Sequence = 1;
            PriceGt = "";
            PriceLte = "";
            GetProductList(subCategoryInfo);

            SearchCommand = new Command(() =>
            {
                if (string.IsNullOrEmpty(SearchString))
                {
                    CrossToastPopUp.Current.ShowToastWarning("请输入关键词", ToastLength.Short);
                }
                else
                {
                    Search(SearchString);
                }
            }, () => { return true; });

            //TappedCommand = new Command<ProductListItem>((productListItem) =>
            //{
            //    //ProductListItem productListItem = ProductList[id];
            //    ProductDetailPage productDetailPage = new ProductDetailPage(productListItem.productId.ToString());
            //    Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            //}, (productListItem) => { return true; });

            LoadMoreCommand = new Command(() =>
            {
                //CrossToastPopUp.Current.ShowToastWarning(str + "/" + ProductNum, ToastLength.Short);
                Page++;
                GetProductList(subCategoryInfo);
            }, () => { return true; });

            SortCommand = new Command<string>((str) =>
            {
                switch (str)
                {
                    //综合排序，不排序
                    case "0": Sort = "0"; break;
                    //价格升序
                    case "1": Sort = GlobalVariables.IsLogged ? "2" : "1"; break;
                    //价格降序
                    case "2": Sort = GlobalVariables.IsLogged ? "-2" : "-1"; break;
                    //销量
                    case "3": Sort = "3"; break;
                    //不排序
                    default: Sort = "0"; break;
                }

                ProductList.Clear();
                ProductNum = 0;
                GetProductList(subCategoryInfo);
            }, (str) => { return true; });

            PriceRangeCommand = new Command<string>((str) =>
            {
                switch (str)
                {
                    //重置
                    case "0":
                        {
                            ProductList.Clear();
                            ProductNum = 0;
                            TotalProductNum = 0;
                            Page = 1;
                            _Size = 20;
                            Sort = "1";
                            Sequence = 1;
                            PriceGt = "";
                            PriceLte = "";

                            GetProductList(subCategoryInfo);
                        }
                        break;
                    //按价格区间查询
                    case "1":
                        {
                            if (string.IsNullOrWhiteSpace(PriceGt) || string.IsNullOrWhiteSpace(PriceLte))
                            {
                                CrossToastPopUp.Current.ShowToastWarning("最低价与最高价不可为空", ToastLength.Long);
                            }
                            else if (int.Parse(PriceGt) > int.Parse(PriceLte))
                            {
                                CrossToastPopUp.Current.ShowToastWarning("最低价>最高价", ToastLength.Long);
                            }
                            else
                            {
                                ProductList.Clear();
                                ProductNum = 0;
                                GetProductList(subCategoryInfo);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }, (str) => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });

        }

        /// <summary>
        /// 搜索进入商品列表
        /// </summary>
        /// <param name="_index"></param>
        public ProductListVM(string _index)
        {
            ProductList = new ObservableCollection<ProductListItem>();
            ProductNum = 0;
            TotalProductNum = 0;
            Page = 1;
            _Size = 20;
            Sort = "1";
            Sequence = 1;

            SearchString = _index;
            Search(SearchString);

            SearchCommand = new Command(() =>
            {
                if (string.IsNullOrEmpty(SearchString))
                {
                    CrossToastPopUp.Current.ShowToastWarning("请输入关键词", ToastLength.Short);
                }
                else
                {
                    Search(SearchString);
                }
            }, () => { return true; });

            //TappedCommand = new Command<ProductListItem>((productListItem) =>
            //{
            //    //ProductListItem productListItem = ProductList[id];
            //    ProductDetailPage productDetailPage = new ProductDetailPage(productListItem.productId.ToString());
            //    Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            //}, (productListItem) => { return true; });

            LoadMoreCommand = new Command(() =>
            {
                //CrossToastPopUp.Current.ShowToastWarning(str + "/" + ProductNum, ToastLength.Short);
                Search(_index);
            }, () => { return true; });

            SortCommand = new Command<string>((str) =>
            {
                switch (str)
                {
                    //综合排序，不排序
                    case "0": Sort = "0"; break;
                    //价格升序
                    case "1": Sort = GlobalVariables.IsLogged ? "2" : "1"; break;
                    //价格降序
                    case "2": Sort = GlobalVariables.IsLogged ? "-2" : "-1"; break;
                    //销量
                    case "3": Sort = "3"; break;
                    //不排序
                    default: Sort = "0"; break;
                }

                ProductList.Clear();
                ProductNum = 0;
                Search(_index);
            }, (str) => { return true; });

            PriceRangeCommand = new Command<string>((str) =>
            {
                switch (str)
                {
                    //重置
                    case "0":
                        {
                            ProductList.Clear();
                            ProductNum = 0;
                            TotalProductNum = 0;
                            Page = 1;
                            _Size = 20;
                            Sort = "1";
                            Sequence = 1;
                            PriceGt = "";
                            PriceLte = "";

                            Search(_index);
                        }
                        break;
                    //按价格区间查询
                    case "1":
                        {
                            if (string.IsNullOrWhiteSpace(PriceGt) || string.IsNullOrWhiteSpace(PriceLte))
                            {
                                CrossToastPopUp.Current.ShowToastWarning("最低价或最高价不可为空", ToastLength.Long);
                            }
                            else if (int.Parse(PriceGt) > int.Parse(PriceLte))
                            {
                                CrossToastPopUp.Current.ShowToastWarning("最低价>最高价", ToastLength.Long);
                            }
                            else
                            {
                                ProductList.Clear();
                                ProductNum = 0;
                                Search(_index);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }, (str) => { return true; });

            BackCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        private async void Search(string searchString)
        {
            IndicatorIsRunning = true;
            //int page = 1;
            //int size = 20;
            //string sort = "1";
            //int sequence = 1;
            //int priceGt = -1;
            //int priceLte = -1;
            int gt = string.IsNullOrEmpty(PriceGt) ? -1 : int.Parse(PriceGt);
            int lte = string.IsNullOrEmpty(PriceLte) ? -1 : int.Parse(PriceLte);

            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.FuzzySearch(searchString, sequence, page, size, sort, gt, lte);

            TotalProductNum = productListRD.result.total;
            ProductNum += productListRD.result.data.Count;

            List<ProductListItem> tempList = new List<ProductListItem>();
            foreach (var item in productListRD.result.data)
            {
                ProductList.Add(item);
            }

            ChangeButtonText();
            IndicatorIsRunning = false;
        }

        private async void Search(string searchString, int page, int size, string sort, int sequence, string priceGt, string priceLte)
        {
            IndicatorIsRunning = true;
            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.FuzzySearch(searchString, sequence, page, size, sort, int.Parse(priceGt), int.Parse(priceLte));

            TotalProductNum = productListRD.result.total;
            ProductNum += productListRD.result.data.Count;

            List<ProductListItem> tempList = new List<ProductListItem>();
            foreach (var item in productListRD.result.data)
            {
                ProductList.Add(item);
            }

            ChangeButtonText();
            IndicatorIsRunning = false;
        }

        private async void GetProductList(Category subCategoryInfo)
        {
            IndicatorIsRunning = true;
            //int page = 1;
            //int size = 20;
            //string sort = "1";
            long cid = subCategoryInfo.id;
            //int priceGt = -1;
            //int priceLte = -1;
            int gt = string.IsNullOrEmpty(PriceGt) ? -1 : int.Parse(PriceGt);
            int lte = string.IsNullOrEmpty(PriceLte) ? -1 : int.Parse(PriceLte);

            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.GetProductList(page, size, sort, cid, gt, lte);

            TotalProductNum = productListRD.result.total;
            ProductNum += productListRD.result.data.Count;

            foreach (var item in productListRD.result.data)
            {
                ProductList.Add(item);
            }

            ChangeButtonText();
            IndicatorIsRunning = false;
        }

        private async void GetProductList(Category subCategoryInfo, int page, int size, string sort, string priceGt, string priceLte)
        {
            IndicatorIsRunning = true;
            long cid = subCategoryInfo.id;

            RestSharpService _restSharpService = new RestSharpService();
            ProductListRD productListRD = await _restSharpService.GetProductList(page, size, sort, cid, int.Parse(priceGt), int.Parse(priceLte));

            TotalProductNum = productListRD.result.total;
            ProductNum += productListRD.result.data.Count;

            List<ProductListItem> tempList = new List<ProductListItem>();
            foreach (var item in productListRD.result.data)
            {
                ProductList.Add(item);
            }

            ChangeButtonText();
            IndicatorIsRunning = false;
        }

        private void ChangeButtonText()
        {
            if (ProductNum == TotalProductNum)
            {
                LoadMoreButtonText = ProductNum.ToString() + "/" + TotalProductNum.ToString() + "，" + "已全部加载";
                ButtonIsEnable = false;
            }
            else
            {
                LoadMoreButtonText = ProductNum.ToString() + "/" + TotalProductNum.ToString() + "，" + "点击加载更多";
                ButtonIsEnable = true;
            }
        }

    }
}
