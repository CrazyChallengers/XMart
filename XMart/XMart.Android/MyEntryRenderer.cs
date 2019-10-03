using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XMart.Controls;
using XMart.Droid;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace XMart.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Control.SetPadding(0, 0, 0, 0);
                Control.SetLines(1);
                //Control.SetShadowLayer(10, 5, 5, global::Android.Graphics.Color.LightGray);
            }
        }
    }
}