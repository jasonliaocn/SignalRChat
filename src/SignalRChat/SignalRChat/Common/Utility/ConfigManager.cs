using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Common.Utility
{
    /// <summary>
    /// Config Manager
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// WebApiUri
        /// </summary>
        public static string WebApiUri
        {
            get
            {
                return ConfigHelper.GetValue<string>("WebApiUri");
            }
        }

        public static TimeSpan ApiClientTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(ConfigHelper.GetValue<int>("ApiClientTimeout", 3));
            }
        }

        public static int IdentityTimeout
        {
            get
            {
                return ConfigHelper.GetValue<int>("IdentityTimeout", 60);
            }
        }

        public static int ScannerTimeout
        {
            get
            {
                return ConfigHelper.GetValue<int>("ScannerTimeout", 10);
            }
        }
    }
}