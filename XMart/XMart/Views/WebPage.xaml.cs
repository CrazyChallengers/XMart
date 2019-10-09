using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage(string url)
        {
            InitializeComponent();

            Web.Source = url;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            defaultActivityIndicator.IsRunning = false;    //指示器关闭
            labelLoading.IsVisible = false;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            defaultActivityIndicator.IsRunning = true;    //指示器开启
            labelLoading.IsVisible = true;
        }

    }
}