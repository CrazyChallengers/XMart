using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Views;

namespace XMart.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        /*
        private List<AdvertiseInfo> advertiseList;   //广告

        public List<AdvertiseInfo> AdvertiseList
        {
            get { return advertiseList; }
            set { SetProperty(ref advertiseList, value); }
        }

        private List<BrandInfo> brandList;   //品牌

        public List<BrandInfo> BrandList
        {
            get { return brandList; }
            set { SetProperty(ref brandList, value); }
        }

        private List<ProductInfo> hotProductList;   //热卖产品列表

        public List<ProductInfo> HotProductList
        {
            get { return hotProductList; }
            set { SetProperty(ref hotProductList, value); }
        }

        private List<ProductInfo> newProductList;   //新品列表

        public List<ProductInfo> NewProductList
        {
            get { return newProductList; }
            set { SetProperty(ref newProductList, value); }
        }

        private List<SubjectInfo> subjectList;   //专题列表

        public List<SubjectInfo> SubjectList
        {
            get { return subjectList; }
            set { SetProperty(ref subjectList, value); }
        }
        */

        private List<HomePanelContent> carouselList;   //comment
        public List<HomePanelContent> CarouselList
        {
            get { return carouselList; }
            set { SetProperty(ref carouselList, value); }
        }

        private List<HomePanelContent> hotProductList;   //comment
        public List<HomePanelContent> HotProductList
        {
            get { return hotProductList; }
            set { SetProperty(ref hotProductList, value); }
        }

        private List<HomePanelContent> officialChoiceList;   //comment
        public List<HomePanelContent> OfficialChoiceList
        {
            get { return officialChoiceList; }
            set { SetProperty(ref officialChoiceList, value); }
        }

        private List<HomePanelContent> goodBrandList;   //comment
        public List<HomePanelContent> GoodBrandList
        {
            get { return goodBrandList; }
            set { SetProperty(ref goodBrandList, value); }
        }

        private List<HomePanelContent> brandChoiceList;   //comment
        public List<HomePanelContent> BrandChoiceList
        {
            get { return brandChoiceList; }
            set { SetProperty(ref brandChoiceList, value); }
        }

        private string index;   //Comment
        public string Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }
        
        public ICommand ItemTapCommand { set; get; }
        public Command SearchCommand { get; set; }
        public Command<string> NavigateCommand { get; set; }

        public HomeViewModel()
        {
            InitHomePage();

            SearchCommand = new Command(() =>
            {
                ProductListPage productListPage = new ProductListPage(Index);
                Index = "";

                Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
            }, () => { return true; });

            NavigateCommand = new Command<string>((pageName) =>
            {
                Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(type);
                Application.Current.MainPage.Navigation.PushModalAsync(page);
            }, (pageName) => { return true; });


            ItemTapCommand = new Command<string>(
                execute: (string productId) =>
                {
                    ProductDetailPage productDetailPage = new ProductDetailPage(productId);
                    Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
                }
                );
        }

        private async void InitHomePage()
        {
            RestSharpService _restSharpService = new RestSharpService();
            HomeContentRD homeContentRD = await _restSharpService.GetHomeContent();

            CarouselList = homeContentRD.result[0].panelContents.ToList<HomePanelContent>();
            HotProductList = homeContentRD.result[1].panelContents.ToList<HomePanelContent>();
            OfficialChoiceList = homeContentRD.result[2].panelContents.ToList<HomePanelContent>();
            GoodBrandList = homeContentRD.result[3].panelContents.ToList<HomePanelContent>();
            BrandChoiceList = homeContentRD.result[4].panelContents.ToList<HomePanelContent>();

        }
    }
}
