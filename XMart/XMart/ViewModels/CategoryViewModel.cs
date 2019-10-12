using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private List<ParentCategoryInfo> parentCategoryList;   //一级目录

        public List<ParentCategoryInfo> ParentCategoryList
        {
            get { return parentCategoryList; }
            set { SetProperty(ref parentCategoryList, value); }
        }

        private List<SubCategoryInfo> subCategoryList;   //二级目录

        public List<SubCategoryInfo> SubCategoryList
        {
            get { return subCategoryList; }
            set { SetProperty(ref subCategoryList, value); }
        }

        public CategoryViewModel()
        {
            

        }
    }
}
