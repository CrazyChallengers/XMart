using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Util;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using XMart.Models;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryPage : ContentPage
	{
        RestService _restService = new RestService();
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        List<SubCategoryInfo> subCategoryList = new List<SubCategoryInfo>();

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
            CategoryRD categoryRD = await _restService.GetCategories();

            categoryViewModel.ParentCategoryList = categoryRD.data.parents;
            subCategoryList = categoryRD.data.categories;

            List<SubCategoryInfo> temp = new List<SubCategoryInfo>();
            
            foreach (var item in subCategoryList)
            {
                if (item.parentId == 1)
                {
                    temp.Add(item);
                }
            }
            /*
            for (int i = 0; i < 20; i++)
            {
                SubCategoryInfo sub = new SubCategoryInfo { id = i, name = "类别1-" + i, parentId = 1 };
                temp.Add(sub);
            }*/
            categoryViewModel.SubCategoryList = temp;

            ParentStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
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

            int selectedParentId = categoryViewModel.ParentCategoryList[index].id;

            List<SubCategoryInfo> temp = new List<SubCategoryInfo>();
           
            foreach (var item in subCategoryList)
            {
                if (item.parentId == selectedParentId)
                {
                    temp.Add(item);
                }
            } 
            /*
            for (int i = 0; i < 20; i++)
            {
                SubCategoryInfo sub = new SubCategoryInfo { id = i, name = "类别"+ selectedParentId + "-" + i, parentId = 1 };
                temp.Add(sub);
            }*/
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

            SubCategoryInfo subCategoryInfo = categoryViewModel.SubCategoryList[index];
            ProductListPage productListPage = new ProductListPage(subCategoryInfo);
            Navigation.PushModalAsync(productListPage);
        }
    }
}