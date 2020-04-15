using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;
using XMart.Models;
using System.Threading;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        //int index = 0;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = homeViewModel;

            /*
            if (homeViewModel.CarouselList.Count()>0)
            {
                Device.StartTimer(new TimeSpan(0, 0, 5), () =>
                {
                    // do something every 10 seconds
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // interact with UI elements
                        carousel.Position = index % homeViewModel.CarouselList.Count();
                        index += 1;
                        index %= homeViewModel.CarouselList.Count();
                    });
                    return true; // runs again, or false to stop
                });
            }*/
        }
        /*
        /// <summary>
        /// 轮播图选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarouselItem_Tapped(object sender, EventArgs e)
        {
            int index = carousel.Position;
            string url = homeViewModel.CarouselList[index].fullUrl;
            //string url = "http://www.baidu.com/";
            WebPage webPage = new WebPage(url);
            Navigation.PushModalAsync(webPage);
        }

        /// <summary>
        /// 热门商品之类的tap事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemTapped_Tapped(object sender, EventArgs e)
        {
            Grid grid = sender as Grid;
            //stackLayout.Children
            int index = HotProductStack.Children.IndexOf(grid);

            long id = homeViewModel.HotProductList[index].productId;
            ProductDetailPage productDetailPage = new ProductDetailPage(id.ToString());
            Navigation.PushModalAsync(productDetailPage);
        }

        /// <summary>
        /// 找单品点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = sender as StackLayout;
            //stackLayout.Children
            int index = CatStack.Children.IndexOf(stackLayout);

            Category category = homeViewModel.CatList[index];
            ProductListPage productListPage = new ProductListPage(category);
            Navigation.PushModalAsync(productListPage);
        }

        protected override void OnDisappearing()
        {

        }
        
        protected override void OnAppearing()
        {
            if (!homeViewModel.NetErrorVisible)
            {
                Device.StartTimer(new TimeSpan(0, 0, 5), () =>
                {
                    // do something every 10 seconds
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // interact with UI elements
                        carousel.Position = index % homeViewModel.CarouselList.Count();
                        index += 1;
                        index %= homeViewModel.CarouselList.Count();
                    });
                    return true; // runs again, or false to stop
                });
            }
        }*/

    }
}