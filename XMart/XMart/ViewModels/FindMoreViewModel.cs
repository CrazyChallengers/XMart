using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.Views;

namespace XMart.ViewModels
{
    public class FindMoreViewModel : BaseViewModel
    {
        private List<Category> subCategoryList;   //二级目录
        public List<Category> SubCategoryList
        {
            get { return subCategoryList; }
            set { SetProperty(ref subCategoryList, value); }
        }

        private string index;   //Comment
        public string Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        public Command SearchCommand { get; set; }

        public FindMoreViewModel()
        {
            SearchCommand = new Command(() =>
            {
                if (string.IsNullOrEmpty(Index))
                {
                    CrossToastPopUp.Current.ShowToastWarning("请输入关键词", ToastLength.Short);
                }
                else
                {
                    ProductListPage productListPage = new ProductListPage(Index);
                    Index = "";

                    Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
                }
            }, () => { return true; });
        }
    }
}
