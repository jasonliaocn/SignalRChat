using Newtonsoft.Json.Linq;
using SignalRChat.Common.Utility;
using SignalRChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Web;

namespace SignalRChat.Common.ApiClient
{
    public class ApiClient:IHttpClient
    {
        private HttpClient httpClient;
        private HttpClientHandler handler;

        private string token;
        public virtual string Token
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = CookieHelper.GetCookieValue<string>(ApplicationConstants.COOKIE_TOKEN);
                }
                return token;
            }
            internal set { token = value; }
        }

        public T GetAsync<T>(string uriString, bool enableMaxTimeout = false)
        {
            CreateHttpClient(enableMaxTimeout, Token);
            HttpResponseMessage response = httpClient.GetAsync(uriString).Result;
            var result = default(T);
            DoApi(response, () =>
            {
                result = response.Content.ReadAsAsync<T>().Result;
            });
            return result;
        }

        /// <summary>
        /// Do Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriString"></param>
        /// <param name="entity"></param>
        public void PostAsJsonAsync<T>(string uriString, T entity, bool enableMaxTimeout = false) where T : class
        {
            CreateHttpClient(enableMaxTimeout, Token);
            HttpResponseMessage response = httpClient.PostAsJsonAsync(uriString, entity).Result;
            DoApi(response);
        }

        public T PostAsJsonAsyncWithReturnValue<T>(string uriString, T entity, bool enableMaxTimeout = false) where T : class
        {
            CreateHttpClient(enableMaxTimeout, Token);
            HttpResponseMessage response = httpClient.PostAsJsonAsync(uriString, entity).Result;
            var result = default(T);
            DoApi(response, () =>
                {
                    result = response.Content.ReadAsAsync<T>().Result;
                });
            return result;
        }

        /// <summary>
        /// Do Put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriString"></param>
        /// <param name="entity"></param>
        public void PutAsJsonAsync<T>(string uriString, T entity, bool enableMaxTimeout = false) where T : class
        {
            CreateHttpClient(enableMaxTimeout, Token);
            HttpResponseMessage response = httpClient.PutAsJsonAsync(uriString, entity).Result;
            DoApi(response);
        }

        /// <summary>
        /// Do delete
        /// </summary>
        /// <param name="uriString"></param>
        public void DeleteAsync(string uriString, bool enableMaxTimeout = false)
        {
            CreateHttpClient(enableMaxTimeout, Token);
            HttpResponseMessage response = httpClient.DeleteAsync(uriString).Result;
            DoApi(response);
        }

        private void DoApi(HttpResponseMessage response, Action success = null)
        {
            if (response.IsSuccessStatusCode)
            {
                if (success != null)
                    success();
            }
            else
            {
                string errorMessage = response.Content.ReadAsStringAsync().Result;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException(errorMessage);
                    default:
                        throw new Exception(errorMessage);
                }
                throw new Exception(errorMessage);
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }
                if (handler != null)
                {
                    handler.Dispose();
                    handler = null;
                }
            }
        }

        protected virtual void CreateHttpClient(bool enableMaxTimeout, string apiToken)
        {
            string webApiUri = ConfigManager.WebApiUri;
            handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip;
            httpClient = HttpClientFactory.Create(handler);

            httpClient.BaseAddress = new Uri(webApiUri);

            if (enableMaxTimeout)
                httpClient.Timeout = TimeSpan.FromMinutes(ApplicationConstants.APICLIENTTIMEOUT_DEFAULT);
            else
                httpClient.Timeout = ConfigManager.ApiClientTimeout;

            if (!string.IsNullOrEmpty(apiToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // Add an Accept header for JSON format.
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string GetAccessToken(string userID, string password, out UserDto userDto, string clientId = "")
        {
            try
            {
                CreateHttpClient(false, "");
                CookieHelper.ClearCookie(ApplicationConstants.COOKIE_TOKEN);
                string token = string.Empty;
                var parameters = new Dictionary<string, string>();
                parameters.Add("grant_type", "password");
                parameters.Add("username", userID);
                parameters.Add("password", password);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + "")));

                using (var fromParameters = new FormUrlEncodedContent(parameters))
                {
                    using (var response = httpClient.PostAsync("token", fromParameters).Result)
                    {
                        var responseValue = response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            token = JObject.Parse(responseValue.Result)["access_token"].Value<string>();
                            string userJson = JObject.Parse(responseValue.Result)["user"].Value<string>();
                            userDto = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(userJson);
                        }
                        else
                        {
                            string error = JObject.Parse(responseValue.Result)["error_description"].Value<string>();
                            throw new AuthenticationException(error);
                        }
                        return token;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}