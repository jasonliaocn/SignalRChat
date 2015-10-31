using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SignalRChat.Common.Utility
{
    /// <summary>
    /// Configuration Manager Helper
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Get value from configuration
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="key">key</param>
        /// <returns>configuration value</returns>
        public static T GetValue<T>(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new ConfigurationErrorsException(string.Format("App Settings Configuration value missing for key '{0}'.", key));
            }
            T result;
            if (typeof(T) == typeof(bool))
            {
                result = (T)((object)ConfigurationManager.AppSettings[key].Equals("true", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                result = (T)((object)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T)));
            }
            return result;
        }
        /// <summary>
        /// Get value from configuration
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="key">key</param>
        /// <param name="defaultValueIfNotExists">default value</param>
        /// <returns>configuration value or default value</returns>
        public static T GetValue<T>(string key, T defaultValueIfNotExists)
        {
            T result;
            if (ConfigurationManager.AppSettings[key] == null)
            {
                result = defaultValueIfNotExists;
            }
            else
            {
                result = (T)((object)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T)));
            }
            return result;
        }
    }
}