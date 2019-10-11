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
            
            BindingContext = homeViewModel;
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
    }
}