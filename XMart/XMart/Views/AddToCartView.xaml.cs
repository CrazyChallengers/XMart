using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Models;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToCartView : PopupPage
    {
        //AddToCartViewVM addToCartViewVM = new AddToCartViewVM();

        public AddToCartView(ProductInfo productInfo)
        {
            InitializeComponent();

            BindingContext = new AddToCartViewVM(productInfo);
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// 返回键事件
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            return false;
        }
    }
}