using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;
using XMart.Services;
using XMart.ResponseData;
using CarouselView.FormsPlugin.Abstractions;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        RestService _restService = new RestService();
        int index = 0;

        public HomePage()
        {
            InitializeComponent();

            InitHomePage();

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                // do something every 10 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    carousel.Position = index % homeViewModel.AdvertiseList.Count();
                    index += 1;
                    index %= homeViewModel.AdvertiseList.Count();
                });
                return true; // runs again, or false to stop
            });

            BindingContext = homeViewModel;
        }

        /// <summary>
        /// 初始化首页
        /// </summary>
        private async void InitHomePage()
        {
            HomeContentRD homeContentRD = await _restService.GetHomeContent();

            homeViewModel.AdvertiseList = homeContentRD.result[0].panelContents.ToList<HomePanelContent>();
            //homeViewModel.HotProductList = homeContentRD.data.hotProductList;
            //homeViewModel.NewProductList = homeContentRD.data.newProductList;
            //homeViewModel.SubjectList = homeContentRD.data.subjectList;
            //homeViewModel.BrandList = homeContentRD.data.brandList;
        }

        /// <summary>
        /// 消息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            
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
    }
}