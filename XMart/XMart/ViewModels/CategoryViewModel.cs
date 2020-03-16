﻿using System;
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

        private string index;   //Comment
        public string Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        public Command SearchCommand { get; set; }

        public CategoryViewModel()
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
