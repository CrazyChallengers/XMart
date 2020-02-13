using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XMart.Controls
{
    public class RememberPwdAction : TriggerAction<CheckBox>
    {
        protected override void Invoke(CheckBox checkBox)
        {
            if (checkBox.IsChecked)
            {
                CrossToastPopUp.Current.ShowToastSuccess("Checked！", ToastLength.Short);
            }
            else
            {
                CrossToastPopUp.Current.ShowToastError("Unchecked！", ToastLength.Short);
            }

        }
    }
}
