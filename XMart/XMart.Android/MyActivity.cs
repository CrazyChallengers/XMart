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
using Com.Alipay.Sdk.App;
using Newtonsoft.Json.Linq;

namespace XMart.Droid
{
    [Activity(Label = "MyActivity")]
    public class MyActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            string sign = Intent.GetStringExtra("sign");
            Pay(sign);
        }

        private string Pay(object sign)
        {
            try
            {
                PayTask payTask = new PayTask(this);
                var result = payTask.Pay(sign.ToString(), false);
                string status = GetPayResultStatus(result);

                //MessagingCenter.Send(new object(), "PaySuccess", status);

                return status;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPayResultStatus(string result)
        {
            string[] string_array = result.Split(';');
            JObject jObject = new JObject();

            for (int i = 0; i < string_array.Length; i++)
            {
                string resultPart = string_array[i];
                string key = resultPart.Split('=')[0];
                string _value = resultPart.Split('=')[1];

                int startIndex = _value.IndexOf("{");
                //int endIndex = content.LastIndexOf(endStr);

                string value = _value.Substring(startIndex + 1, _value.Length - startIndex - 1);
                value = value.Substring(0, value.LastIndexOf("}"));

                jObject.Add(key, value);
            }

            return jObject["resultStatus"].ToString();
        }
    }
}