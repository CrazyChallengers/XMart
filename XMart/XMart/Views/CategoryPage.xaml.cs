using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Util;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryPage : ContentPage
	{
        CategoryViewModel categoryViewModel = new CategoryViewModel();

		public CategoryPage ()
		{
			InitializeComponent ();

            BindingContext = categoryViewModel;

            MainClassStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
            SubClassStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
		}

        private void Init()
        {
            MainClassStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
            SubClassStack.Children[0].Behaviors[0].SetValue(RadioBehavior.IsCheckedProperty, true);
            
        }

        private void MainTGR_Tapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int index = MainClassStack.Children.IndexOf(label);

            string selectedClass = categoryViewModel.MainClassList[index];

            List<string> newSubClassList = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                newSubClassList.Add(selectedClass + i);
            }

            categoryViewModel.SubClassList = newSubClassList;
        }

        private void SubTGR_Tapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int index = SubClassStack.Children.IndexOf(label);

            string selectedClass = categoryViewModel.SubClassList[index];

            List<string> newItemList = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                newItemList.Add(selectedClass + i);
            }

            categoryViewModel.ItemList = newItemList;
        }
    }
}