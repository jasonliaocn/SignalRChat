using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SignalRChat.Common
{
    public static class HashHelper
    {
        /// <summary>
        /// Get MD5 Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5EncryptToString<T>(this T source)
        {
            using (HashAlgorithm algorithm = MD5.Create())
            {
                string strEncrypt = source.ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(strEncrypt);
                byte[] inArray = algorithm.ComputeHash(bytes);
                return Convert.ToBase64String(inArray);
            }

        }
    }
}