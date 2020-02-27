using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XMart.ResponseData;
using Newtonsoft.Json;
using XMart.Models;
using XMart.Util;

namespace XMart.Services
{
    public class RestService
    {
        HttpClient _client = new HttpClient();
        string rootUrl2 = "http://118.24.96.42:8083";    //新
        string rootUrl3 = "http://120.26.3.153:7777";    //更新
        
        public RestService()
        {

        }

        #region 会员注册登录
        /// <summary>
        /// 发送验证码 ok
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <returns></returns>
        public async Task<SimpleRD> SendAuthCode(string tel)
        {
            string url = rootUrl3 + "/member/getAuthCode?tel=" + tel;

            string responseBody = await GetStringDataAsync(url);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);

            return simpleRD;
        }

        /// <summary>
        /// 注册 ok
        /// </summary>
        /// <param name="registerPara"></param>
        /// <returns></returns>
        public async Task<SimpleRD> Register(RegisterPara registerPara)
        {
            string url = rootUrl3 + "/member/registerByInvite";

            string httpContent = JsonConvert.SerializeObject(registerPara);  //序列化

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);   //反序列化

            return simpleRD;
        }

        /// <summary>
        /// 忘记密码、重置密码
        /// </summary>
        /// <param name="resetPwdPara"></param>
        /// <returns></returns>
        public async Task<SimpleRD> ResetPwd(ResetPwdPara resetPwdPara)
        {
            string url = rootUrl2 + "/User/resetPwd";

            string httpContent = JsonConvert.SerializeObject(resetPwdPara);

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);

            return simpleRD;
        }

        /// <summary>
        /// 登录 ok
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public async Task<LoginRD> Login(LoginPara loginPara)
        {
            string url = rootUrl3 + "/member/loginByTel";

            string httpContent = JsonConvert.SerializeObject(loginPara);

            string responseBody = await PostAsync(url, httpContent);

            LoginRD loginRD = JsonConvert.DeserializeObject<LoginRD>(responseBody);

            return loginRD;
        }

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="loginPara"></param>
        /// <returns></returns>
        public async Task<LoginRD> LoginByAuthCode(LoginPara loginPara)
        {
            string url = rootUrl3 + "/member/loginByAuthCode";

            string httpContent = JsonConvert.SerializeObject(loginPara);

            string responseBody = await PostAsync(url, httpContent);

            LoginRD loginRD = JsonConvert.DeserializeObject<LoginRD>(responseBody);

            return loginRD;
        }
        #endregion

        #region 购物车
        /// <summary>
        /// 获取购物车商品列表
        /// </summary>
        /// <param name="memberId">用户Id</param>
        /// <returns></returns>
        public async Task<CartItemListRD> GetCartItemList(string memberId)
        {
            string url = rootUrl3 + "/member/cartList";
            string httpContent = "{\"userId\":" + memberId + "}";

            string responseBody = await PostAsync(url, httpContent);

            CartItemListRD cartItemListRD = JsonConvert.DeserializeObject<CartItemListRD>(responseBody);

            return cartItemListRD;
        }

        public async Task<SimpleRD> AddToCart(string memberId, string productId, string num)
        {
            string url = rootUrl3 + "/member/addCart";
            string httpContent = "{\"userId\":" + memberId 
                + ", \"productId\":" + productId + ", \"productNum\":" + num + "}";

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);

            return simpleRD;
        }

        #endregion

        #region 收货地址
        /// <summary>
        /// 获取收货地址列表
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<AddressRD> GetAddressListById(string memberId)
        {
            string url = rootUrl3 + "/member/addressList";

            string httpContent = "{\"userId\":" + memberId + "}";

            string responseBody = await PostAsync(url, httpContent);

            AddressRD addressRD = JsonConvert.DeserializeObject<AddressRD>(responseBody);   //反序列化

            return addressRD;
        }

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="addressInfo"></param>
        /// <returns></returns>
        public async Task<SimpleRD> AddAddress(AddressInfo addressInfo)
        {
            string url = rootUrl3 + "/member/addAddress";

            string httpContent = JsonConvert.SerializeObject(addressInfo);

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);   //反序列化

            return simpleRD;
        }

        /// <summary>
        /// 修改收获地址
        /// </summary>
        /// <param name="addressInfo"></param>
        /// <returns></returns>
        public async Task<SimpleRD> UpdateAddress(AddressInfo addressInfo)
        {
            string url = rootUrl3 + "/member/updateAddress";

            string httpContent = JsonConvert.SerializeObject(addressInfo);

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);   //反序列化

            return simpleRD;
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="addressInfo"></param>
        /// <returns></returns>
        public async Task<SimpleRD> DeleteAddressById(long addressId)
        {
            string url = rootUrl3 + "/member/delAddress";

            string httpContent = "{\"addressId\":" + addressId + "}";

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);   //反序列化

            return simpleRD;
        }
        #endregion

        #region 订单

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="orderPara"></param>
        /// <returns></returns>
        public async Task<StupidRD> Order(OrderPara orderPara)
        {
            string url = rootUrl3 + "/member/addOrder";

            string httpContent = JsonConvert.SerializeObject(orderPara);  //序列化

            string responseBody = await PostAsync(url, httpContent);

            StupidRD stupidRD = JsonConvert.DeserializeObject<StupidRD>(responseBody);   //反序列化

            return stupidRD;
        }

        /// <summary>
        /// 根据订单编号获取订单详情信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderDetailRD> GetOrderDetailByOrderId(long orderId)
        {
            string url = rootUrl3 + "/member/orderDetail?orderId=" + orderId.ToString();

            string responseBody = await GetStringDataAsync(url);

            OrderDetailRD orderDetailRD = JsonConvert.DeserializeObject<OrderDetailRD>(responseBody);

            return orderDetailRD;
        }

        /// <summary>
        /// 根据用户id获取订单列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderListRD> GetOrderListById(int userId, int page, int size)
        {
            string url = rootUrl3 + "/member/orderList?userId=" + userId.ToString() 
                + "&page=" + page.ToString() 
                + "&size=" + size.ToString();

            string responseBody = await GetStringDataAsync(url);

            OrderListRD orderListRD = JsonConvert.DeserializeObject<OrderListRD>(responseBody);

            return orderListRD;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public async Task<SimpleRD> CancelOrder(OrderDetail orderDetail)
        {
            string url = rootUrl3 + "/member/cancelOrder";

            string httpContent = JsonConvert.SerializeObject(orderDetail);  //序列化

            string responseBody = await PostAsync(url, httpContent);

            SimpleRD simpleRD = JsonConvert.DeserializeObject<SimpleRD>(responseBody);   //反序列化

            return simpleRD;
        }

        #endregion

        #region 商品
        /// <summary>
        /// 获取首页相关内容信息
        /// </summary>
        /// <returns></returns>
        public async Task<HomeContentRD> GetHomeContent()
        {
            string url = rootUrl3 + "/goods/home";

            string responseBody = await GetStringDataAsync(url);

            HomeContentRD homeContentRD = JsonConvert.DeserializeObject<HomeContentRD>(responseBody);

            return homeContentRD;
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        public async Task<CategoryRD> GetCategories()
        {
            string url = rootUrl3 + "/goods/SearchAllItemCat";

            string responseBody = await GetStringDataAsync(url);

            CategoryRD categoryRD = JsonConvert.DeserializeObject<CategoryRD>(responseBody);

            return categoryRD;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="cid"></param>
        /// <param name="priceGt"></param>
        /// <param name="priceLte"></param>
        /// <returns></returns>
        public async Task<ProductListRD> GetProductList(int page, int size, string sort, long cid, int priceGt, int priceLte)
        {
            string url = rootUrl3 + "/goods/allGoods?page=" + page.ToString()
                + "&size=" + size.ToString()
                + "&sort=" + sort
                + "&cid=" + cid.ToString()
                + "&priceGt=" + priceGt.ToString()
                + "&priceLte=" + priceLte.ToString();

            string responseBody = await GetStringDataAsync(url);

            ProductListRD productListRD = JsonConvert.DeserializeObject<ProductListRD>(responseBody);

            return productListRD;
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductDetailRD> GetProductDetail(string productId)
        {
            string url = rootUrl3 + "/goods/productDet?productId=" + productId;
            string responseBody = await GetStringDataAsync(url);
            ProductDetailRD productDetailRD = JsonConvert.DeserializeObject<ProductDetailRD>(responseBody);
            return productDetailRD;
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sequence"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="priceGt"></param>
        /// <param name="priceLte"></param>
        /// <returns></returns>
        public async Task<ProductListRD> FuzzySearch(string index, int sequence, int page, int size, string sort, int priceGt, int priceLte)
        {
            string url = rootUrl3 + "/goods/SearchLike?page=" + page.ToString()
                + "&index=" + index
                + "&size=" + size.ToString()
                + "&sort=" + sort
                + "&sequence=" + sequence.ToString()
                + "&priceGt=" + priceGt.ToString()
                + "&priceLte=" + priceLte.ToString();

            string responseBody = await GetStringDataAsync(url);

            ProductListRD productListRD = JsonConvert.DeserializeObject<ProductListRD>(responseBody);

            return productListRD;
        }

        #endregion

        #region 
        /// <summary>
        /// 截取字符串，处理网站返回值
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        private string GetSubString(string content, string startStr, string endStr)
        {
            int startIndex = content.IndexOf(startStr);
            //int endIndex = content.LastIndexOf(endStr);
            string str = "";

            str = content.Substring(startIndex, content.Length - startIndex);
            str = str.Substring(0, str.LastIndexOf(endStr) + 1);

            return str;
        }

        /// <summary>
        /// Get方法
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<string> GetDataAsync(string uri)
        {
            string responseData = "";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                    //responseData = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Get string方法
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<string> GetStringDataAsync(string uri)
        {
            string responseData = "";
            try
            {
                string response = await _client.GetStringAsync(uri);
                responseData = GetSubString(response, "{", "}");
                //responseData = JsonConvert.DeserializeObject<string>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private async Task<string> PostAsync(string uri, string httpContent)
        {
            string responseData = "";
            var _httpContent = new StringContent(httpContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                    //responseData = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// Post Form表单的方法
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private async Task<string> PostFormAsync(string uri, string httpContent)
        {
            string responseData = "";
            var _httpContent = new StringContent(httpContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, _httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    responseData = GetSubString(content, "{", "}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {0}", ex.Message);
            }

            return responseData;
        }
        #endregion
    }
}
