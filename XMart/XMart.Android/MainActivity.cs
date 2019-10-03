using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;

namespace XMart.Droid
{
    [Activity(Label = "XMart", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                //透明状态栏                
                Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                //透明导航栏                
                Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            }

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CarouselViewRenderer.Init();
            //CachedImageRenderer.Init(true);
            
            LoadApplication(new App());
        }
    }
}