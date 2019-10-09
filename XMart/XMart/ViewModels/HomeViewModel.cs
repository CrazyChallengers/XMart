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

        private List<string> featureList;   //专题列表

        public List<string> FeatureList
        {
            get { return featureList; }
            set { SetProperty(ref featureList, value); }
        }

        private List<string> discountList;   //折扣商品

        public List<string> DiscountList
        {
            get { return discountList; }
            set { SetProperty(ref discountList, value); }
        }
        
        public HomeViewModel()
        {
            Title = "首页";
            
        }
    }
}
