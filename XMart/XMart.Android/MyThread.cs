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

namespace XMart.Droid
{
    public class MyThread
    {
        public string Result { get; set; }

        public MyThread(string sign)
        {
            
        }

        //真正方法
        public string Pay(string sign)
        {
            //Intent intent = new Intent(this, typeof(MyActivity));
            //intent.PutExtra("sign", sign);
            //StartActivity(intent);
            return "";
        }

        //加壳方法
        public void Alipay(string sign)
        {
            Result = Pay(sign);
        }
    }
}