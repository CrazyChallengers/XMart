using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ViewModels
{
    public class ProductDetailVM : BaseViewModel
    {
        private ProductInfo product;   //comment

        public ProductInfo Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }

        public ProductDetailVM()
        {

        }
    }
}
