using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Models;
using XMart.ResponseData;
using XMart.Services;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindMorePage : ContentPage
    {
        FindMoreViewModel findMoreViewModel = new FindMoreViewModel();

        public FindMorePage()
        {
            InitializeComponent();

            BindingContext = findMoreViewModel;
        }
    }
}