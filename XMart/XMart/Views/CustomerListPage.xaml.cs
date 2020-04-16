using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerListPage : ContentPage
    {
        CustomerListViewModel customerListViewModel;

        public CustomerListPage()
        {
            InitializeComponent();

            customerListViewModel = new CustomerListViewModel();

            BindingContext = customerListViewModel;
        }
    }
}