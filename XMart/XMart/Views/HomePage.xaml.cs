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

            homeViewModel.CarouselSelectedCommand = new Command<string>(
                async (url) =>
                {
                    WebPage webPage = new WebPage(url);
                    await Navigation.PushModalAsync(webPage);
                });

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

        private void Carousel_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void InitHomePage()
        {
            HomeContentRD homeContentRD = await _restService.GetHomeContent();

            homeViewModel.AdvertiseList = homeContentRD.data.advertiseList;
            homeViewModel.HotProductList = homeContentRD.data.hotProductList;
            homeViewModel.NewProductList = homeContentRD.data.newProductList;
            homeViewModel.SubjectList = homeContentRD.data.subjectList;
            homeViewModel.BrandList = homeContentRD.data.brandList;

            //Console.WriteLine(homeContentRD);
        }

        private void MessageButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void CarouselItem_Tapped(object sender, EventArgs e)
        {

        }
    }
}