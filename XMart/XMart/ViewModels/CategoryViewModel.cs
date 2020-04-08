using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Views;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace XMart.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private List<Category> parentCategoryList;   //一级目录
        public List<Category> ParentCategoryList
        {
            get { return parentCategoryList; }
            set { SetProperty(ref parentCategoryList, value); }
        }

        private List<Category> subCategoryList;   //二级目录
        public List<Category> SubCategoryList
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
        public Command ReloadCommand { get; set; }

        public CategoryViewModel()
        {
            ParentCategoryList = new List<Category>();

            SubCategoryList = new List<Category>();

            SearchString = "";

            SearchCommand = new Command(() =>
            {
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

            ReloadCommand = new Command(() =>
            {

            }, () => { return true; });

        }

    }
}
