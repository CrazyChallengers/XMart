using System;
using System.ComponentModel;
using Android.Content;
using Android.Webkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(XMart.Droid.WebRenderer))]
namespace XMart.Droid
{
    public class WebRenderer : ViewRenderer<Xamarin.Forms.WebView, AWebView>
    {
        Context _context;

        public WebRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = new AWebView(_context);
                webView.SetWebViewClient(new WebViewClient(_context));
                webView.Settings.JavaScriptEnabled = true;
                SetNativeControl(webView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null && e.PropertyName == "Source")
            {
                var src = Element.Source as UrlWebViewSource;
                Control.LoadUrl(src.Url);
            }
        }
    }

    public class WebViewClient : Android.Webkit.WebViewClient
    {
        private Context _context;
        public WebViewClient(Context context)
        {
            _context = context;
        }
        public override bool ShouldOverrideUrlLoading(AWebView view, IWebResourceRequest request)
        {
            if (request.Url.Scheme == "weixin")
            {
                Intent intent = new Intent() { };
                intent.SetAction(Intent.ActionView);
                intent.SetData(request.Url);
                this._context.StartActivity(intent);
                return true;
            }
            return base.ShouldOverrideUrlLoading(view, request);
        }

        [Obsolete]
        public override bool ShouldOverrideUrlLoading(AWebView view, string url)
        {
            if (url.StartsWith("weixin"))
            {
                Intent intent = new Intent() { };
                intent.SetAction(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse(url));
                this._context.StartActivity(intent);
                return true;
            }
            return base.ShouldOverrideUrlLoading(view, url);
        }
    }

}