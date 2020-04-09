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
    public partial class CollectionPage : ContentPage
    {
        CollectionViewModel collectionViewModel;

        public CollectionPage()
        {
            InitializeComponent();

            collectionViewModel = new CollectionViewModel();
            BindingContext = collectionViewModel;
        }

    }
}