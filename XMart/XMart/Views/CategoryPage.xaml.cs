using System;
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
        CategoryViewModel categoryViewModel;

		public CategoryPage ()
		{
			InitializeComponent ();

            categoryViewModel = new CategoryViewModel();

            BindingContext = categoryViewModel;

            InitCategories();
            //ParentStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
            //categoryViewModel.GetSubCategories(categoryViewModel.ParentCategoryList[0].id);
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

                categoryViewModel.categoryList = categoryRD.result;

                foreach (var item in categoryViewModel.categoryList)
                {
                    if (item.isParent)
                    {
                        categoryViewModel.ParentCategoryList.Add(item);
                    }
                }

                ParentStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
                categoryViewModel.GetSubCategories(categoryViewModel.ParentCategoryList[0].id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
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
        */
    }
}