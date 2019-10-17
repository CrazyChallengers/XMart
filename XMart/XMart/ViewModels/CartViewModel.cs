using System.Collections.Generic;
using XMart.Models;

namespace XMart.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private List<CartItemInfo> itemList;   //商品列表

        public List<CartItemInfo> ItemList
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

        private bool isAllChecked;   //是否全选

        public bool IsAllChecked
        {
            get { return isAllChecked; }
            set { SetProperty(ref isAllChecked, value); }
        }

        private int checkedNumber;   //选择数量

        public int CheckedNumber
        {
            get { return checkedNumber; }
            set { SetProperty(ref checkedNumber, value); }
        }


        public CartViewModel()
        {
            Title = "购物车";

            CheckedNumber = 0;
        }
    }
}
