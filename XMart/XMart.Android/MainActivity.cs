﻿using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;
using Com.Tencent.MM.Opensdk.Openapi;
using Com.Tencent.MM.Opensdk.Modelmsg;
using Com.Alipay.Sdk.App;
using Com.Alipay.Android.App;
using System;
using Xamarin.Forms;
using Com.Alipay.Sdk.Pay.Demo.Util;
using System.Threading;
using Java.Net;
using XMart.Services;
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
                //Window.SetStatusBarColor(Android.Graphics.Color.DeepSkyBlue);
                //不遮挡导航栏                
                Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            }

            //Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);   //弹出框

            CarouselViewRenderer.Init();    //轮播图
            CachedImageRenderer.Init(true);

            //支付宝
            MessagingCenter.Subscribe<object, string>(this, "Pay", (sender, obj) =>
            {
                try
                {
                    Thread the = new Thread(new ParameterizedThreadStart(Pay));
                    the.Start(obj);
                }
                catch (Exception ex)
                {
                }

                //var appid = "2021001146672151";
                //var rsa2 = true;
                //var para = OrderInfoUtil2_0.BuildOrderParamMap(appid, rsa2);
                //var orderParam = OrderInfoUtil2_0.BuildOrderParam(para);
                //var privateKey = RSA_PRIVATE;
                //var sign = OrderInfoUtil2_0.GetSign(para, privateKey, rsa2);
                //var orderInfo = orderParam + "&" + sign;
                //
                //PayTask pay = new PayTask(this);
                //var result = pay.PayV2(orderInfo, false);
                //Console.WriteLine("支付宝result:" + result);

                /*
                try
                {
                    var con = getOrderInfo("test", "testbody");
                    var sign = OrderInfoUtil2_0.GetSign(con, RSA_PRIVATE, false);
                    sign = URLEncoder.Encode(sign, "utf-8");
                    con += "&sign=\"" + sign + "\"&" + MySignType;
                    Com.Alipay.Sdk.App.PayTask pa = new Com.Alipay.Sdk.App.PayTask(this);
                    var result = pa.Pay(con, false);
                    Logger_Info("支付宝result:" + result);
                }
                catch (Exception ex)
                {

                    Logger_Info("2" + ex.Message + ex.StackTrace);
                }

                
                string orderInfo = info;   // 订单信息

                PayTask alipay = new PayTask(this);
                JavaDictionary<string, string> result = alipay.PayV2(orderInfo, true);

                Message msg = new Message();
                msg.What = SDK_PAY_FLAG;
                msg.Obj = result;
                mHandler.sendMessage(msg);
            
                // 必须异步调用
                //Thread payThread = new Thread(payRunnable);
                //payThread.start();*/
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
            /*
            //分享小程序给朋友
            MessagingCenter.Subscribe<object, string>(this, "ShareToFriend", (sender, arg) =>
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
            });*/

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
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        private bool RegToWx()
        {
            wxApi = WXAPIFactory.CreateWXAPI(this, appID, true);
            return wxApi.RegisterApp(appID);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region 支付宝
        private void Pay(object con)
        {
            try
            {
                PayTask pa = new PayTask(this);
                var result = pa.Pay(con.ToString(), false);
                //return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}

