using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ViewModels
{
    public class ProductListVM : BaseViewModel
    {
        private List<ProductListItem> productList;   //产品列表
        public List<ProductListItem> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }

        public ProductListVM()
        {

        }
    }
}
