using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindMorePage : ContentPage
    {
        FindMoreViewModel findMoreViewModel = new FindMoreViewModel();

        public FindMorePage()
        {
            InitializeComponent();

            InitCategories();

            BindingContext = findMoreViewModel;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async void InitCategories()
        {
            RestSharpService _restSharpService = new RestSharpService();
            CategoryRD categoryRD = await _restSharpService.GetCategories();

            List<Category> categoryList = new List<Category>();
            categoryList = categoryRD.result;

            List<Category> temp = new List<Category>();

            foreach (var item in categoryList)
            {
                if (!item.isParent)
                {
                    temp.Add(item);
                }
            }

            findMoreViewModel.SubCategoryList = temp;

        }

        private void CategorySelected_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;
            int index = SubStack.Children.IndexOf(frame);

            Category subCategoryInfo = findMoreViewModel.SubCategoryList[index];
            ProductListPage productListPage = new ProductListPage(subCategoryInfo);
            Navigation.PushModalAsync(productListPage);
        }
    }
}