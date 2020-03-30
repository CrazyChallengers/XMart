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
    public class MonitorValueChange
    {
        private string myValue;
        public string MyValue
        {
            get { return myValue; }
            set
            {
                if (value != myValue)
                {
                    WhenMyValueChange();
                }
                myValue = value;
            }
        }
        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;
        //事件触发函数
        private void WhenMyValueChange()
        {
            if (OnMyValueChanged != null)
            {
                OnMyValueChanged(this, null);
            }
        }

    }
}