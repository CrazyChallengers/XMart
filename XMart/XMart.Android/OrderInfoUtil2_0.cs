using Android.Util;
using Com.Alipay.Sdk.Pay.Demo;
using Java.IO;
using Java.Net;
using Java.Text;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMart.Droid
{
	public class OrderInfoUtil2_0
	{

		/**
		 * 构造授权参数列表
		 *
		 * @param pid
		 * @param app_id
		 * @param target_id
		 * @return
		 */
		public static Dictionary<string, string> BuildAuthInfoMap(string pid, string app_id, string target_id, bool rsa2)
		{
			Dictionary<string, string> keyValues = new Dictionary<string, string>();

			// 商户签约拿到的app_id，如：2013081700024223
			keyValues.Add("app_id", app_id);

			// 商户签约拿到的pid，如：2088102123816631
			keyValues.Add("pid", pid);

			// 服务接口名称， 固定值
			keyValues.Add("apiname", "com.alipay.account.auth");

			// 服务接口名称， 固定值
			keyValues.Add("methodname", "alipay.open.auth.sdk.code.get");

			// 商户类型标识， 固定值
			keyValues.Add("app_name", "mc");

			// 业务类型， 固定值
			keyValues.Add("biz_type", "openservice");

			// 产品码， 固定值
			keyValues.Add("product_id", "APP_FAST_LOGIN");

			// 授权范围， 固定值
			keyValues.Add("scope", "kuaijie");

			// 商户唯一标识，如：kkkkk091125
			keyValues.Add("target_id", target_id);

			// 授权类型， 固定值
			keyValues.Add("auth_type", "AUTHACCOUNT");

			// 签名类型
			keyValues.Add("sign_type", rsa2 ? "RSA2" : "RSA");

			return keyValues;
		}

		/**
		 * 构造支付订单参数列表
		 */
		public static Dictionary<string, string> BuildOrderParamMap(string app_id, bool rsa2)
		{
			Dictionary<string, string> keyValues = new Dictionary<string, string>();

			keyValues.Add("app_id", app_id);

			keyValues.Add("biz_content", "{\"timeout_express\":\"30m\",\"product_code\":\"QUICK_MSECURITY_PAY\",\"total_amount\":\"0.01\",\"subject\":\"1\",\"body\":\"我是测试数据\",\"out_trade_no\":\"" + GetOutTradeNo() + "\"}");

			keyValues.Add("charset", "utf-8");

			keyValues.Add("method", "alipay.trade.app.pay");

			keyValues.Add("sign_type", rsa2 ? "RSA2" : "RSA");

			keyValues.Add("timestamp", "2016-07-29 16:55:53");

			keyValues.Add("version", "1.0");

			return keyValues;
		}

		/**
		 * 构造支付订单参数信息
		 *
		 * @param map
		 * 支付订单参数
		 * @return
		 */
		public static string BuildOrderParam(Dictionary<string, string> map)
		{
			List<string> keys = new List<string>(map.Keys);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < keys.Count - 1; i++)
			{
				string key = keys[i];
				string value = map[key];
				sb.Append(BuildKeyValue(key, value, true));
				sb.Append("&");
			}

			string tailKey = keys[keys.Count - 1];
			string tailValue = map[tailKey];
			sb.Append(BuildKeyValue(tailKey, tailValue, true));

			return sb.ToString();
		}

		/**
		 * 拼接键值对
		 *
		 * @param key
		 * @param value
		 * @param isEncode
		 * @return
		 */
		private static string BuildKeyValue(string key, string value, bool isEncode)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(key);
			sb.Append("=");
			if (isEncode)
			{
				try
				{
					sb.Append(URLEncoder.Encode(value, "UTF-8"));
				}
				catch (UnsupportedEncodingException e)
				{
					sb.Append(value);
				}
			}
			else
			{
				sb.Append(value);
			}
			return sb.ToString();
		}

		/**
		 * 对支付参数信息进行签名
		 *
		 * @param map
		 *            待签名授权信息
		 *
		 * @return
		 */
		public static string GetSign(Dictionary<string, string> map, string rsaKey, bool rsa2)
		{
			List<string> keys = new List<string>(map.Keys);
			// key排序
			Collections.Sort(keys);

			StringBuilder authInfo = new StringBuilder();
			for (int i = 0; i < keys.Count - 1; i++)
			{
				string key = keys[i];
				string value = map[key];
				authInfo.Append(BuildKeyValue(key, value, false));
				authInfo.Append("&");
			}

			string tailKey = keys[keys.Count - 1];
			string tailValue = map[tailKey];
			authInfo.Append(BuildKeyValue(tailKey, tailValue, false));

			string auth = authInfo.ToString();
			string oriSign = SignUtils.Sign(auth, rsaKey, rsa2);
			string encodedSign = "";

			try
			{
				encodedSign = URLEncoder.Encode(oriSign, "UTF-8");
			}
			catch (UnsupportedEncodingException e)
			{
				e.PrintStackTrace();
			}
			return "sign=" + encodedSign;
		}

		/**
		 * 要求外部订单号必须唯一。
		 * @return
		 */
		private static string GetOutTradeNo()
		{
			SimpleDateFormat format = new SimpleDateFormat("MMddHHmmss");
			//SimpleDateFormat format = new SimpleDateFormat("MMddHHmmss", Locale.GetDefault());
			Date date = new Date();
			string key = format.Format(date);

			Java.Util.Random r = new Java.Util.Random();
			key = key + r.NextInt();
			key = key.Substring(0, 15);
			return key;
		}

	}
}