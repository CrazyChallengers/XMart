using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;

namespace XMart.Droid
{
    [Activity(Label = "XMart", Icon = "@mipmap/xmart", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Resources.DisplayMetrics.ScaledDensity = 2;//告诉android不要把自己大小单位缩放
            //Resources.DisplayMetrics.Density = 1;
            App.ScreenWidth = Resources.DisplayMetrics.WidthPixels;
            App.ScreenHeight = Resources.DisplayMetrics.HeightPixels;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                //透明状态栏                
                Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                //透明导航栏                
                Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            }

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CarouselViewRenderer.Init();
            CachedImageRenderer.Init(true);
            
            LoadApplication(new App());
        }
    }
}