using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;
using Com.Tencent.MM.Opensdk.Openapi;
using Com.Tencent.MM.Opensdk.Modelbase;
using Com.Tencent.MM.Opensdk.Modelmsg;
using System;
using Xamarin.Forms;

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

            //微信相关
            MessagingCenter.Subscribe<object>(this, "Register", d =>
            {
                var result = RegToWx();
                // MessagingCenter.Send(new object(), "Registered", result);//广播注册的结果  
            });
            MessagingCenter.Subscribe<object, string>(this, "ShareToFriend", (sender, arg) =>
            {
                WXTextObject textObj = new WXTextObject { Text = arg };//定义wx文本对象
                WXMediaMessage msg = new WXMediaMessage { MyMediaObject = textObj, Description = arg };//定义wxmsg对象
                SendMessageToWX.Req req = new SendMessageToWX.Req()
                {
                    Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
                    Message = msg,
                    Scene = SendMessageToWX.Req.WXSceneSession//分享到对话
                };
                wxApi.SendReq(req);
            });
            MessagingCenter.Subscribe<object, string>(this, "ShareToTimeline", (sender, arg) =>
            {
                WXTextObject textObj = new WXTextObject { Text = arg };//定义wx文本对象
                WXMediaMessage msg = new WXMediaMessage { MyMediaObject = textObj, Description = arg };//定义wxmsg对象
                SendMessageToWX.Req req = new SendMessageToWX.Req()
                {
                    Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
                    Message = msg,
                    Scene = SendMessageToWX.Req.WXSceneTimeline//分享到朋友圈
                };
                wxApi.SendReq(req);
            });

            LoadApplication(new App());
        }

        #region 微信相关
        private readonly string appID = "wx6990f0f3818a8c7e";//申请的appid
        private IWXAPI wxApi;
        private bool RegToWx()
        {
            wxApi = WXAPIFactory.CreateWXAPI(this, appID, true);
            return wxApi.RegisterApp(appID);
        }
        #endregion
    }
}