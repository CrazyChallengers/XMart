using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Views;
using XMart.Util;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XMart
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {
            InitializeComponent();
            /*
            #if DEBUG
            HotReloader.Current.Run(this);
            #endif
            */

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
