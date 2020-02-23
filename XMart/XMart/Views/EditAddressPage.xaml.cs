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
    public partial class EditAddressPage : ContentPage
    {
        public EditAddressPage()
        {
            InitializeComponent();

            BindingContext = new EditAddressViewModel();
        }

        public EditAddressPage(AddressInfo addressInfo)
        {
            InitializeComponent();

            BindingContext = new EditAddressViewModel(addressInfo);
        }
    }
}