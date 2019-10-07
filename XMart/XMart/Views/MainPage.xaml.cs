using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XMart.Views
{
    public partial class MainPage : TabbedPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            InitPages();
        }

        private void InitPages()
        {
            if (true)
            {
                Children.Add(new LoginPage());

                //Children.Add(new CartPage());
                //Children.Add(new MePage());
            }
        }
    }
}
