using SignalRChat.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Common.ApiClient
{
    public static class CookieHelper
    {
        /// <summary>
        /// Remove the Cookie
        /// </summary>
        /// <param name="cookieName">cookieName</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// get Cookie value
        /// </summary>
        /// <param name="cookieName">cookieName</param>
        /// <returns>Cookie value</returns>
        public static T GetCookieValue<T>(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            T result = default(T);
            if (cookie != null)
            {
                var cookieValue = HttpUtility.UrlDecode(cookie.Value);
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cookieValue);
            }
            return result;
        }
        /// <summary>
        /// add Cookie（24h）
        /// </summary>
        /// <param name="cookieName">cookie Name</param>
        /// <param name="value">cookie value</param>
        public static void SetCookie<T>(string cookieName, T value)
        {
            SetCookie(cookieName, value, DateTime.Now.AddMinutes(ConfigManager.IdentityTimeout));
        }
        /// <summary>
        /// add Cookie
        /// </summary>
        /// <param name="cookieName">cookieName</param>
        /// <param name="value">value</param>
        /// <param name="expires"> the expiration date and time for the cookie.</param>
        public static void SetCookie<T>(string cookieName, T value, DateTime expires)
        {
            var cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Value = HttpUtility.UrlEncode(cookieValue),
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}