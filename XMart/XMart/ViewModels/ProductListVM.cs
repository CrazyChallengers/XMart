using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ViewModels
{
    public class ProductListVM : BaseViewModel
    {
        private List<ProductInfo> productList;   //产品列表

        public List<ProductInfo> ProductList
        {
            get { return productList; }
            set { SetProperty(ref productList, value); }
        }


        public ProductListVM()
        {

        }
    }
}
