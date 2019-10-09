using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private List<string> itemList;   //商品列表

        public List<string> ItemList
        {
            get { return itemList; }
            set { SetProperty(ref itemList, value); }
        }

        private string totalSelectedPrice;   //所有已选产品总价

        public string TotalSelectedPrice
        {
            get { return totalSelectedPrice; }
            set { SetProperty(ref totalSelectedPrice, value); }
        }

        private string itemNumber;   //购物车商品数量

        public string ItemNumber
        {
            get { return itemNumber; }
            set { SetProperty(ref itemNumber, value); }
        }


        public CartViewModel()
        {
            Title = "购物车";
        }
    }
}
