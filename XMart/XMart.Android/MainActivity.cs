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
using System.Drawing;
using Android.Runtime;
using System.Threading.Tasks;
using Java.Net;
using Com.Alipay.Sdk.Pay.Demo.Util;
using Com.Alipay.Sdk.App;

namespace XMart.Droid
{
    [Activity(Label = "美而好家具", Icon = "@mipmap/xmart", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //微信相关
        private readonly string appID = "wx6990f0f3818a8c7e";//申请的appid
        private IWXAPI wxApi;
        //支付宝相关
        public static string PARTNER = "合作商户ID";
        public static string SELLER = "商户收款的支付宝账号";
        private string RSA_PRIVATE = "商户私密";

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

            CarouselViewRenderer.Init();
            CachedImageRenderer.Init(true);

            //支付宝
            MessagingCenter.Subscribe<object>(this, "Pay", obj =>
            {
                var appid = "12345678";
                var rsa2 = true;
                var para = OrderInfoUtil2_0.BuildOrderParamMap(appid, rsa2);
                var orderParam = OrderInfoUtil2_0.BuildOrderParam(para);
                var privateKey = "987456321";
                var sign = OrderInfoUtil2_0.GetSign(para, privateKey, rsa2);
                var orderInfo = orderParam + "&" + sign;

                //var con = getOrderInfo("test", "testbody");
                //var sign = OrderInfoUtil2_0.Sign(con, RSA_PRIVATE);
                //sign = URLEncoder.Encode(sign, "utf-8");
                //con += "&sign=\"" + sign + "\"&" + MySignType;

                PayTask pay = new PayTask(this);
                var result = pay.Pay(orderInfo, false);
                //Logger_Info("支付宝result:" + result);
            });

            //微信相关
            //注册
            MessagingCenter.Subscribe<object>(this, "Register", d =>
            {
                var result = RegToWx();
                // MessagingCenter.Send(new object(), "Registered", result);//广播注册的结果  
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
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        private bool RegToWx()
        {
            wxApi = WXAPIFactory.CreateWXAPI(this, appID, true);
            return wxApi.RegisterApp(appID);
        }

        public string getOrderInfo(String subject, String body)
        {
            // 签约合作者身份ID
            String orderInfo = "partner=" + "\"" + PARTNER + "\"";
            // 签约卖家支付宝账号
            orderInfo += "&seller_id=" + "\"" + SELLER + "\"";
            // 商户网站唯一订单号
            orderInfo += "&out_trade_no=" + "\"DJ" + DateTime.Now.ToString("yyyyMMddhhmmss") + "\"";
            // 商品名称
            orderInfo += "&subject=" + "\"" + subject + "\"";
            // 商品详情
            orderInfo += "&body=" + "\"" + body + "\"";
            // 商品金额
            orderInfo += "&total_fee=" + "\"" + 1 + "\"";
            // 服务器异步通知页面路径
            orderInfo += "&notify_url=" + "\"" + "http://111.203.248.34:89/Order/AlipayNotify"
                    + "\"";
            // 服务接口名称， 固定值
            orderInfo += "&payment_type=\"1\"";

            // 参数编码， 固定值
            orderInfo += "&_input_charset=\"utf-8\"";

            // 设置未付款交易的超时时间
            // 默认30分钟，一旦超时，该笔交易就会自动被关闭。
            // 取值范围：1m～15d。
            // m-分钟，h-小时，d-天，1c-当天（无论交易何时创建，都在0点关闭）。
            // 该参数数值不接受小数点，如1.5h，可转换为90m。
            orderInfo += "&it_b_pay=\"30m\"";

            // extern_token为经过快登授权获取到的alipay_open_id,带上此参数用户将使用授权的账户进行支付
            // orderInfo += "&extern_token=" + "\"" + extern_token + "\"";
            // 支付宝处理完请求后，当前页面跳转到商户指定页面的路径，可空
            orderInfo += "&return_url=\"http://111.203.248.34:89/Order/AlipayNotify\"";

            // 调用银行卡支付，需配置此参数，参与签名， 固定值 （需要签约《无线银行卡快捷支付》才能使用）
            // orderInfo += "&paymethod=\"expressGateway\"";
            return orderInfo;

        }

        public string MySignType
        {
            get
            {
                return "sign_type=\"RSA\"";
            }
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}