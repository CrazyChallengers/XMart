using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ViewModels
{
    public class ProductDetailVM : BaseViewModel
    {
        public ProductInfo product { get; set; }   //商品信息


        public ProductDetailVM()
        {

        }
    }
}
