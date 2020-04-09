using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;
using Com.Tencent.MM.Opensdk.Openapi;
using Com.Tencent.MM.Opensdk.Modelmsg;
using Com.Alipay.Sdk.App;
using System;
using Xamarin.Forms;
using System.Threading;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace XMart.Droid
{
    [Activity(MainLauncher = false, Label = "美而好", Icon = "@mipmap/xmart", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //微信相关
        private readonly string appID = "wx6990f0f3818a8c7e";//申请的appid
        private IWXAPI wxApi;

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                WhenValueSet();
                status = value;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Resources.DisplayMetrics.ScaledDensity = 2;//告诉android不要把自己大小单位缩放
            //double systemDensity = DeviceDisplay.MainDisplayInfo.Density;
            //Resources.DisplayMetrics.Density = (float)systemDensity / 2.55F;
            //var temp = Resources.DisplayMetrics;
            //var device = DeviceDisplay.MainDisplayInfo;
            App.ScreenWidth = Resources.DisplayMetrics.WidthPixels;
            App.ScreenHeight = Resources.DisplayMetrics.HeightPixels;
            
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                //透明状态栏                
                //Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                Window.SetStatusBarColor(Android.Graphics.Color.LightGray);
                //不遮挡导航栏                
                Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            }

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);   //弹出框
            CarouselViewRenderer.Init();    //轮播图
            CachedImageRenderer.Init(true);

            //支付宝
            MessagingCenter.Subscribe<object, string>(this, "Pay", (sender, sign) =>
            {
                try
                {
                    //Func<string, string> test = Pay;
                    //IAsyncResult asyncResult = test.BeginInvoke(sign, null, null);
                    //string result = test.EndInvoke(asyncResult);
                    //
                    //Console.WriteLine(result);
                    //Status = "";
                    Thread the = new Thread(new ParameterizedThreadStart(Pay));
                    the.Start(sign);
                    //the.Join();
                    //Console.WriteLine(Pay(sign));

                    //Task<string> task = new Task<string>(async () => await Pay(sign));
                    //var result = await Pay(sign);
                    //task.Wait();
                    //task.RunSynchronously();
                    //Console.WriteLine(result);

                    //PayDelegate payDelegate = Pay;
                    //Task<string> task = Task<string>.Factory.FromAsync(payDelegate.BeginInvoke(sign, Callback, "a delegate asynchronous call"), payDelegate.EndInvoke);
                    //Task<string> task = Task<string>.Factory.FromAsync(payDelegate.BeginInvoke, payDelegate.EndInvoke, sign, "a delegate asynchronous call");
                    //task.ContinueWith(t => MessagingCenter.Send(new object(), "PaySuccess", t.Result));
                }
                catch (ThreadAbortException)
                {
                    MessagingCenter.Send(new object(), "PaySuccess", Status);
                }

            });

            //微信相关
            //注册
            MessagingCenter.Subscribe<object>(this, "Register", d =>
            {
                var result = RegToWx();
                // MessagingCenter.Send(new object(), "Registered", result);//广播注册的结果  
            });

            MessagingCenter.Subscribe<object>(this, "Login", d =>
            {
                // send oauth request
                SendAuth.Req req = new SendAuth.Req();
                req.Scope = "snsapi_userinfo";
                req.State = "wechat_sdk_demo_test";
                bool result = wxApi.SendReq(req);

            });
            
            //分享小程序给朋友
            MessagingCenter.Subscribe<object, string>(this, "ShareMiniProgramToFriend", (sender, arg) =>
            {
                WXMiniProgramObject miniProgramObj = new WXMiniProgramObject();
                miniProgramObj.WebpageUrl = "http://www.qq.com"; // 兼容低版本的网页链接
                miniProgramObj.MiniprogramType = WXMiniProgramObject.MiniptogramTypeRelease;// 正式版:0，测试版:1，体验版:2
                miniProgramObj.UserName = "gh_d43f693ca31f";     // 小程序原始id
                miniProgramObj.Path = "/pages/media";            //小程序页面路径；对于小游戏，可以只传入 query 部分，来实现传参效果，如：传入 "?foo=bar"

                WXMediaMessage msg = new WXMediaMessage(miniProgramObj);
                msg.Title = "小程序消息Title";                    // 小程序消息title
                msg.Description = "小程序消息Desc";               // 小程序消息desc
                //msg.ThumbData = getThumb();                      // 小程序消息封面图片，小于128k

                SendMessageToWX.Req req = new SendMessageToWX.Req
                {
                    //Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
                    Message = msg,
                    Scene = SendMessageToWX.Req.WXSceneSession  // 目前只支持会话
                };

                wxApi.SendReq(req);
            });

            //分享文字给朋友
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
            //分享文字到朋友圈
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
            /*
            //分享网页给朋友
            MessagingCenter.Subscribe<object, string>(this, "ShareToFriend", (sender, arg) =>
            {
                //初始化一个WXWebpageObject，填写url
                WXWebpageObject webpage = new WXWebpageObject();
                webpage.WebpageUrl = "网页url";

                //用 WXWebpageObject 对象初始化一个 WXMediaMessage 对象
                WXMediaMessage msg = new WXMediaMessage(webpage);
                msg.Title = "网页标题 ";
                msg.Description = "网页描述";
                Bitmap thumbBmp = BitmapFactory.decodeResource(getResources(), R.drawable.send_music_thumb);
                msg.ThumbData = Util.bmpToByteArray(thumbBmp, true);

                //构造一个Req
                SendMessageToWX.Req req = new SendMessageToWX.Req();
                req.Transaction = buildTransaction("webpage");
                req.Message = msg;
                req.Scene = mTargetScene;
                req.UserOpenId = getOpenId();

                //调用api接口，发送数据到微信
                wxApi.sendReq(req);
            });*/

            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        private bool RegToWx()
        {
            wxApi = WXAPIFactory.CreateWXAPI(this, appID, true);
            return wxApi.RegisterApp(appID);
        }
        /*
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }*/

        #region 支付宝
        private void Pay(object sign)
        {
            try
            {
                PayTask payTask = new PayTask(this);
                var result = payTask.PayV2(sign.ToString(), true);
                //Status = result["resultStatus"];

                
                Looper.Prepare();
                //switch (status)
                //{
                //    case "9000": Android.Widget.Toast.MakeText(this, "订单支付成功！", Android.Widget.ToastLength.Long).Show(); break;
                //    case "8000": Android.Widget.Toast.MakeText(this, "正在处理中！", Android.Widget.ToastLength.Long).Show(); break;
                //    case "4000": Android.Widget.Toast.MakeText(this, "订单支付失败！", Android.Widget.ToastLength.Long).Show(); break;
                //    case "6001": Android.Widget.Toast.MakeText(this, "用户中途取消！", Android.Widget.ToastLength.Long).Show(); break;
                //    case "6002": Android.Widget.Toast.MakeText(this, "网络连接出错！", Android.Widget.ToastLength.Long).Show(); break;
                //    default: break;
                //}
                switch (result["resultStatus"])
                {
                    case "9000": CrossToastPopUp.Current.ShowToastSuccess("订单支付成功！", ToastLength.Long); break;
                    case "8000": CrossToastPopUp.Current.ShowToastWarning("正在处理中！", ToastLength.Long); break;
                    case "4000": CrossToastPopUp.Current.ShowToastError("订单支付失败！", ToastLength.Long); break;
                    case "6001": CrossToastPopUp.Current.ShowToastWarning("用户中途取消！", ToastLength.Long); break;
                    case "6002": CrossToastPopUp.Current.ShowToastError("网络连接出错！", ToastLength.Long); break;
                    default: break;
                }
                Looper.Loop();

                //return result["resultStatus"];
                //Thread.CurrentThread.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public void WhenValueSet()
        {
            MessagingCenter.Send(new object(), "PaySuccess", Status);
        }

    }
}

