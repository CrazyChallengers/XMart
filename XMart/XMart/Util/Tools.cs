using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace XMart.Util
{
    public static class Tools
    {
        /// <summary>
        /// 检测是否手机号码
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string tel)
        {
            string CellPhoneReg = @"^[1]+[3,4,5,7,8,9]+\d{9}$";   //手机

            return Regex.IsMatch(tel, CellPhoneReg);
        }

        /// <summary>
        /// 不同类型对象同名属性赋值
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="s">源对象</param>
        /// <param name="t">目标对象</param>
        public static void AutoMapping<S, T>(S s, T t)
        {
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type target = t.GetType();

            foreach (var pp in pps)
            {
                try
                {
                    PropertyInfo targetPP = target.GetProperty(pp.Name);
                    object value = pp.GetValue(s, null);

                    if (targetPP != null && value != null)
                    {
                        targetPP.SetValue(t, value, null);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

    }
}
