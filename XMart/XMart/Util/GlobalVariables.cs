using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.Util
{
    public static class GlobalVariables
    {
        public static bool IsLogged { get; set; }

        public static UserInfo LoggedUser {get; set;}

        public static bool DarkMode { get; set; }
    }
}
