using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XMart.Util
{
    public class ScreenHelper
    {
        public class PIXEL : Dictionary<string, double>
        {

            public new double this[string key]
            {
                get
                {
                    return Convert.ToDouble(key) * ScreenHelper.flag;
                }
                set
                {

                }
            }
        }
        public class MARGIN : Dictionary<string, Thickness>
        {
            public new Thickness this[string key]
            {
                get
                {
                    string[] p = key.Split('-');
                    if (p.Length == 1)
                        return new Thickness(Convert.ToDouble(p[0]) * ScreenHelper.flag);
                    return new Thickness(Convert.ToDouble(p[0]) * ScreenHelper.flag,
                        Convert.ToDouble(p[1]) * ScreenHelper.flag,
                        Convert.ToDouble(p[2]) * ScreenHelper.flag,
                        Convert.ToDouble(p[3]) * ScreenHelper.flag);
                }
                set
                {

                }
            }
        }

        public static double flag;
        public PIXEL px
        {
            get;
        }
        public MARGIN m
        {
            get;
        }
        public ScreenHelper()
        {
            //计算出屏幕缩放比例，640是你的UI原始设计图的高度
            flag = App.ScreenWidth / 640.0;
            px = new PIXEL();
            m = new MARGIN();
        }
    }
}
