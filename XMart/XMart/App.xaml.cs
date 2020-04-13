using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.Views;
using XMart.Util;
using XMart.Services;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XMart
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        private static LocalDatabaseHelper database;   //Comment
        public static LocalDatabaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XMartSQLite.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

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
