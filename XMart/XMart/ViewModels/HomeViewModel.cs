using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.Views;
using XMart.Util;
using System.Threading;

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

        private List<Category> catList;   //Comment
        public List<Category> CatList
        {
            get { return catList; }
            set { SetProperty(ref catList, value); }
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

        private string searchString;   //Comment
        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

        public Command<long> ItemTapCommand { set; get; }
        public Command<int> FindMoreCommand { get; set; }
        public Command<int> CarouselTappedCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command<string> NavigateCommand { get; set; }
        public Command MoreCatCommand { get; set; }

        public HomeViewModel()
        {
            InitHomePage();

            //TimerCallback timerDelegate = new TimerCallback(Tick);
            //timer = new Timer(timerDelegate, null, 0, 5000); //5秒执行一次Tick方法

            CarouselTappedCommand = new Command<int>((position) =>
            {
                string url = CarouselList[position].fullUrl;
                WebPage webPage = new WebPage(url);
                Application.Current.MainPage.Navigation.PushModalAsync(webPage);
            }, (position) => { return true; });

            FindMoreCommand = new Command<int>((id) =>
            {
                foreach (var item in CatList)
                {
                    if (item.id == id)
                    {
                        ProductListPage productListPage = new ProductListPage(item);
                        Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
                    }
                }
            }, (id) => { return true; });

            SearchCommand = new Command(() =>
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                if (string.IsNullOrEmpty(SearchString))
                {
                    CrossToastPopUp.Current.ShowToastWarning("请输入关键词", ToastLength.Short);
                }
                else
                {
                    ProductListPage productListPage = new ProductListPage(SearchString);
                    SearchString = "";
                    Application.Current.MainPage.Navigation.PushModalAsync(productListPage);
                }
            }, () => { return true; });

            NavigateCommand = new Command<string>((pageName) =>
            {
                Type type = Type.GetType(pageName);
                Page page = (Page)Activator.CreateInstance(type);
                Application.Current.MainPage.Navigation.PushModalAsync(page);
            }, (pageName) => { return true; });

            ItemTapCommand = new Command<long>((id) =>
            {
                ProductDetailPage productDetailPage = new ProductDetailPage(id.ToString());
                Application.Current.MainPage.Navigation.PushModalAsync(productDetailPage);
            }, (id) => { return true; });

            MoreCatCommand = new Command(() =>
            {
                FindMorePage findMorePage = new FindMorePage();
                Application.Current.MainPage.Navigation.PushModalAsync(findMorePage);
            }, () => { return true; });

        }

        private async void InitHomePage()
        {
            try
            {
                if (!Tools.IsNetConnective())
                {
                    CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
                    return;
                }

                RestSharpService _restSharpService = new RestSharpService();
                HomeContentRD homeContentRD = await _restSharpService.GetHomeContent();
                CategoryRD categoryRD = await _restSharpService.GetCategories();

                //CarouselList = homeContentRD.result[0].panelContents.ToList<HomePanelContent>();
                HotProductList = homeContentRD.result[1].panelContents.ToList<HomePanelContent>();
                //OfficialChoiceList = homeContentRD.result[2].panelContents.ToList<HomePanelContent>();
                //GoodBrandList = homeContentRD.result[3].panelContents.ToList<HomePanelContent>();
                //BrandChoiceList = homeContentRD.result[4].panelContents.ToList<HomePanelContent>();

                CarouselList = new List<HomePanelContent>
                {
                    new HomePanelContent
                    {
                        picUrl = "sanye.png",
                        fullUrl = "http://www.sanecn.com/"
                    },
                    new HomePanelContent
                    {
                        picUrl = "bianselong.jpg",
                        fullUrl = "http://www.cdbsljs.com/"
                    },
                    new HomePanelContent
                    {
                        picUrl = "jiabei.jpg",
                        fullUrl = "http://www.cdbsljs.com/"
                    }
                };

                List<Category> temp = new List<Category>();
                foreach (var item in categoryRD.result)
                {
                    if (!item.isParent)
                    {
                        temp.Add(item);
                    }
                }
                CatList = temp.GetRange(0, 10);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
