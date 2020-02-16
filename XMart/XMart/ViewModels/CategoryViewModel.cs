using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;

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

        //public Command<int> ParentTappedCommand { get; private set; }
        //public Command SubTappedCommand { get; private set; }
        //
        //RestService _restService = new RestService();
        //List<Category> categoryList = new List<Category>();

        public CategoryViewModel()
        {
            /*
            ParentTappedCommand = new Command<int>((int id) =>
            {
                List<Category> temp = new List<Category>();

                foreach (var item in categoryList)
                {
                    if (item.parentId == id)
                    {
                        temp.Add(item);
                    }
                }

                SubCategoryList = temp;
            }, (int fuck) => { return true; });

            SubTappedCommand = new Command(() =>
            {

            }, () => { return true; });
            */
        }
        /*
        /// <summary>
        /// 初始化
        /// </summary>
        private async void InitCategories()
        {
            CategoryRD categoryRD = await _restService.GetCategories();

            //categoryViewModel.ParentCategoryList = categoryRD.result.parents;
            //subCategoryList = categoryRD.data.categories;

            categoryList = categoryRD.result;

            List<Category> temp = new List<Category>();

            foreach (var item in categoryList)
            {
                if (item.isParent)
                {
                    temp.Add(item);
                }
            }
            ParentCategoryList = temp;

            
        }*/
    }
}
