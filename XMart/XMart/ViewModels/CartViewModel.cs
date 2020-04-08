using System.Collections.Generic;
using Xamarin.Forms;
using XMart.Models;
using XMart.Util;
using XMart.Views;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.ResponseData;
using XMart.Services;
using System.Linq;

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

        //private bool isAllChecked;   //是否全选
        //public bool IsAllChecked
        //{
        //    get { return isAllChecked; }
        //    set { SetProperty(ref isAllChecked, value); }
        //}

        private int checkedNumber;   //选择数量
        public int CheckedNumber
        {
            get { return checkedNumber; }
            set { SetProperty(ref checkedNumber, value); }
        }

        //private Color allCheckedButton_Color;   //comment
        //public Color AllCheckedButton_Color
        //{
        //    get { return allCheckedButton_Color; }
        //    set { SetProperty(ref allCheckedButton_Color, value); }
        //}

        public Command OrderCommand { get; set; }
        public Command AllCheckCommand { get; set; }
        public Command<long> TwoTappedCommand { get; set; }
        public Command<long> CheckedChangedCommand { get; set; }
        public Command RefreshCommand { get; set; }    //

        RestSharpService _restSharpService = new RestSharpService();

        public CartViewModel()
        {
            ItemList = new List<CartItemInfo>();
            TotalSelectedPrice = "0";
            ItemNumber = "0";
            CheckedNumber = 0;
            //IsAllChecked = false;
            //AllCheckedButton_Color = Color.LightGray;

            InitCart();

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

            /*
            AllCheckCommand = new Command(() =>
            {
                IsAllChecked = !IsAllChecked;
                AllCheckedButton_Color = IsAllChecked ? Color.Crimson : Color.LightGray;
                foreach (var item in ItemList)
                {
                    item.Checked = IsAllChecked;
                }

                OnCount();
            }, () => { return true; });*/

            TwoTappedCommand = new Command<long>((id) =>
            {
                TwoTapped_TappedAsync(id);
            }, (id) => { return true; });

            CheckedChangedCommand = new Command<long>((id) =>
            {
                foreach (var item in ItemList)
                {
                    if (item.productId == id)
                    {
                        item.Checked = !item.Checked;
                    }
                }

                OnCount();
            }, (id) => { return true; });

            RefreshCommand = new Command(() =>
            {
                InitCart();
            }, () => { return true; });

        }

        /// <summary>
        /// 计算已勾选商品的数量和总价
        /// </summary>
        private void OnCount()
        {
            double totalPrice = 0;
            int number = 0;
            //bool allChecked = true;
            foreach (var item in ItemList)
            {
                if (item.Checked)
                {
                    totalPrice += (item.memberPrice * item.productNum);
                    number += item.productNum;
                }

                //allChecked &= item.Checked;
            }

            //IsAllChecked = allChecked;
            //AllCheckedButton_Color = allChecked ? Color.Crimson : Color.LightGray;
            TotalSelectedPrice = totalPrice.ToString();
            CheckedNumber = number;
        }

        /// <summary>
        /// 初始化购物车
        /// </summary>
        public async void InitCart()
        {
            try
            {
                if (GlobalVariables.IsLogged)
                {
                    if (!Tools.IsNetConnective())
                    {
                        CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                        return;
                    }

                    string memberId = GlobalVariables.LoggedUser.id.ToString();
                    CartItemListRD cartItemListRD = await _restSharpService.GetCartItemList(memberId);

                    /*
                    List<CartItemInfo> tempList = cartItemListRD.result;
                    foreach (var tempItem in tempList)
                    {
                        foreach (var item in ItemList)
                        {
                            if (item.productId == tempItem.productId)
                            {
                                continue;
                            }
                            else
                            {

                            }
                        }
                    }*/
                    ItemList = cartItemListRD.result;
                    ItemNumber = cartItemListRD.result.Count().ToString();
                    OnCount();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private async void TwoTapped_TappedAsync(long id)
        {
            try
            {
                string action = await Application.Current.MainPage.DisplayActionSheet("选择操作", "取消", null, "删除", "修改");

                if (action == "删除")
                {
                    if (!Tools.IsNetConnective())
                    {
                        CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                        return;
                    }

                    CartItemInfo temp = new CartItemInfo();
                    foreach (var item in ItemList)
                    {
                        if (item.productId == id)
                        {
                            temp = item;
                            ItemList.Remove(item);
                        }
                    }

                    //Frame frame = sender as Frame;
                    //int index = ItemStack.Children.IndexOf(frame);

                    StupidRD stupidRD = await _restSharpService.DeleteItemInCart(temp);

                    if (stupidRD.success)
                    {
                        CrossToastPopUp.Current.ShowToastSuccess("删除成功！", ToastLength.Short);
                        InitCart();
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastError("删除失败！", ToastLength.Short);
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

