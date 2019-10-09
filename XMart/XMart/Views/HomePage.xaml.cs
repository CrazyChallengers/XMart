using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel homeViewModel = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();

            homeViewModel.CarouselSelectedCommand = new Command<string>(
                async (url) =>
                {
                    WebPage webPage = new WebPage(url);
                    await Navigation.PushModalAsync(webPage);
                });

            homeViewModel.CarouselItemsSource = new List<CarouselItem>()
            {
                new CarouselItem { ImgSource = "http://phone.68fc.cn/indexpic/1.jpg", ImgUrl = "http://phone.68fc.cn/indexpic/1.html", CarouselCommand = homeViewModel.CarouselSelectedCommand },
                new CarouselItem { ImgSource = "http://phone.68fc.cn/indexpic/2.jpg", ImgUrl = "http://phone.68fc.cn/indexpic/2.html", CarouselCommand = homeViewModel.CarouselSelectedCommand },
                new CarouselItem { ImgSource = "http://phone.68fc.cn/indexpic/3.jpg", ImgUrl = "http://phone.68fc.cn/indexpic/3.html", CarouselCommand = homeViewModel.CarouselSelectedCommand }
            };

            homeViewModel.DiscountList = new List<string>
            {
                "折扣商品1","折扣商品2","折扣商品3","折扣商品4","折扣商品5","折扣商品6"
            };

            homeViewModel.FeatureList = new List<string>
            {
                "专题1","专题2","专题3","专题4","专题5","专题6"
            };

            BindingContext = homeViewModel;
        }
    }
}