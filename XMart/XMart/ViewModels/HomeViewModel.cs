using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;

namespace XMart.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<CarouselItem> carouselItemsSource;
        public List<CarouselItem> CarouselItemsSource
        {
            get { return carouselItemsSource; }
            set { SetProperty(ref carouselItemsSource, value); }
        }

        public Command CarouselSelectedCommand { set; get; }

        public HomeViewModel()
        {
            Title = "首页";
            
        }
    }
}
