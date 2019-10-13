using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XMart.Util
{
    public static class Tools
    {
        public static bool IsPhoneNumber(string tel)
        {
            string CellPhoneReg = @"^[1]+[3,4,5,7,8,9]+\d{9}$";   //手机

            return Regex.IsMatch(tel, CellPhoneReg);
        }
    }
}
