using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XMart.Models;

namespace XMart.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command CarouselSelectedCommand { set; get; }
        
        //--------------------------------------
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
        
        //--------------------------------------

        public HomeViewModel()
        {
            Title = "首页";
            
        }
    }
}
