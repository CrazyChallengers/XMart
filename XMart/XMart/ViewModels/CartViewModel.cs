﻿using System.Collections.Generic;
using Xamarin.Forms;
using XMart.Models;
using XMart.Util;
using XMart.Views;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

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

        private Color allCheckedButton_Color;   //comment
        public Color AllCheckedButton_Color
        {
            get { return allCheckedButton_Color; }
            set { SetProperty(ref allCheckedButton_Color, value); }
        }

        public Command OrderCommand { get; set; }
        public Command AllCheckCommand { get; set; }

        public CartViewModel()
        {
            Title = "购物车";

            TotalSelectedPrice = "0";
            CheckedNumber = 0;
            IsAllChecked = false;
            allCheckedButton_Color = Color.LightGray;

            OrderCommand = new Command(() =>
            {
                List<CartItemInfo> productList = new List<CartItemInfo>();

                foreach (var item in ItemList)
                {
                    if (item.Checked)
                    {
                        productList.Add(item);
                    }
                }

                if (productList.Count == 0)
                {
                    CrossToastPopUp.Current.ShowToastError("没有选中商品", ToastLength.Long);
                }
                else
                {
                    OrderingPage orderingPage = new OrderingPage(productList);
                    Application.Current.MainPage.Navigation.PushModalAsync(orderingPage);
                }
            }, () => { return true; });

            AllCheckCommand = new Command(() =>
            {
                IsAllChecked = !IsAllChecked;
                AllCheckedButton_Color = IsAllChecked ? Color.Crimson : Color.LightGray;
                foreach (var item in ItemList)
                {
                    item.Checked = IsAllChecked;
                }

                OnCount();
            }, () => { return true; });

        }

        /// <summary>
        /// 
        /// </summary>
        private void OnCount()
        {
            double totalPrice = 0;
            int number = 0;
            foreach (var item in ItemList)
            {
                if (item.Checked)
                {
                    totalPrice += (item.memberPrice * item.productNum);
                    number += item.productNum;
                }
            }

            TotalSelectedPrice = totalPrice.ToString();
            CheckedNumber = number;
        }
    }
}
