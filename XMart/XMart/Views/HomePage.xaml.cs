using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        int index = 0;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = homeViewModel;
        }

        /// <summary>
        /// 轮播图选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarouselItem_Tapped(object sender, EventArgs e)
        {
            int index = carousel.Position;
            //string url = homeViewModel.AdvertiseList[index].url;
            string url = "http://www.baidu.com/";
            WebPage webPage = new WebPage(url);
            Navigation.PushModalAsync(webPage);
        }

        private void ItemTapped_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = sender as StackLayout;
            //stackLayout.Children
            int index = HotProductStack.Children.IndexOf(stackLayout);

            long id = homeViewModel.HotProductList[index].productId;
            ProductDetailPage productDetailPage = new ProductDetailPage(id.ToString());
            Navigation.PushModalAsync(productDetailPage);
        }

        private new void Disappearing()
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
                return false; // runs again, or false to stop
            });
        }

        private new void Appearing()
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
    }
}