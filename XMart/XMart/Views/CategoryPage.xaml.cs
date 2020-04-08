﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Util;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using XMart.Models;
using XMart.Behaviors;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryPage : ContentPage
	{
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        List<Category> categoryList = new List<Category>();

		public CategoryPage ()
		{
			InitializeComponent ();

            InitCategories();

            BindingContext = categoryViewModel;
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

                categoryList = categoryRD.result;

                List<Category> temp = new List<Category>();

                foreach (var item in categoryList)
                {
                    if (item.isParent)
                    {
                        temp.Add(item);
                    }
                }

                categoryViewModel.ParentCategoryList = temp;

                ParentStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
                GetSubCategories(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 一级目录点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentGR_Tapped(object sender, EventArgs e)
        {
            Label label = sender as Label;

            int index = ParentStack.Children.IndexOf(label);

            GetSubCategories(index);
        }

        /// <summary>
        /// 获取二级目录
        /// </summary>
        /// <param name="index"></param>
        private void GetSubCategories(int index)
        {
            int selectedParentId = categoryViewModel.ParentCategoryList[index].id;

            List<Category> temp = new List<Category>();

            foreach (var item in categoryList)
            {
                if (item.parentId == selectedParentId)
                {
                    temp.Add(item);
                }
            }
            categoryViewModel.SubCategoryList = temp;
        }

        /// <summary>
        /// 二级目录点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubTGR_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = sender as StackLayout;
            int index = SubStack.Children.IndexOf(stackLayout);

            Category subCategoryInfo = categoryViewModel.SubCategoryList[index];
            ProductListPage productListPage = new ProductListPage(subCategoryInfo);
            Navigation.PushModalAsync(productListPage);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            InitCategories();
        }

    }
}