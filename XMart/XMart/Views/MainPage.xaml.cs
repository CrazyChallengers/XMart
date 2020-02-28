using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMart.Views;
using XMart.Util;

namespace XMart.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (GlobalVariables.IsLogged)
            {
                Children.Add(new MePage());
            }
            else
            {
                Children.Add(new LoginPage());
            }
        }

        /// <summary>
        /// 返回键事件，禁止返回
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
