using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private List<string> mainClassList;   //一级大类

        public List<string> MainClassList
        {
            get { return mainClassList; }
            set { SetProperty(ref mainClassList, value); }
        }

        private List<string> subClassList;   //二级小类

        public List<string> SubClassList
        {
            get { return subClassList; }
            set { SetProperty(ref subClassList, value); }
        }

        private List<string> itemList;   //商品列表

        public List<string> ItemList
        {
            get { return itemList; }
            set { SetProperty(ref itemList, value); }
        }
        
        public CategoryViewModel()
        {
            //Title = ""

            MainClassList = new List<string>
            {
                "A","B","C","D","E","F","G","H","I","J"
            };

            SubClassList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                SubClassList.Add(MainClassList[i] + i);
            }

        }
    }
}
