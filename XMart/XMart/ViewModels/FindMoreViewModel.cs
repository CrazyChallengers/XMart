using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Views;
using XMart.Util;

namespace XMart.ViewModels
{
    public class FindMoreViewModel : BaseViewModel
    {
        private ObservableCollection<Category> subCategoryList;   //二级目录
        public ObservableCollection<Category> SubCategoryList
        {
            get { return subCategoryList; }
            set { SetProperty(ref subCategoryList, value); }
        }

        private string searchString;   //Comment
        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

        public Command SearchCommand { get; set; }
        public Command<int> ItemTappedCommand { get; set; }

        public FindMoreViewModel()
        {
            SubCategoryList = new ObservableCollection<Category>();

            InitCategories();

            SearchCommand = new Command(() =>
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                if (string.IsNullOrEmpty(SearchString))
                {
                    CrossToastPopUp.Current.ShowToastWarning("请输入关键词", ToastLength.Short);
                }
                else
                {
                    ProductListPage productListPage = new ProductListPage(SearchString);
                    SearchString = "";

                    Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
                }
            }, () => { return true; });

            ItemTappedCommand = new Command<int>((id) =>
            {
                foreach (var item in SubCategoryList)
                {
                    if (item.id == id)
                    {
                        ProductListPage productListPage = new ProductListPage(item);
                        Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
                    }
                }
            }, (id) => { return true; });

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async void InitCategories()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();
                CategoryRD categoryRD = await _restSharpService.GetCategories();

                List<Category> categoryList = categoryRD.result;

                foreach (var item in categoryList)
                {
                    if (!item.isParent)
                    {
                        SubCategoryList.Add(item);
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
