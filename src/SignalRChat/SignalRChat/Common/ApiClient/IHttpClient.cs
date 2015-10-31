using SignalRChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Common.ApiClient
{
    public interface IHttpClient : IDisposable
    {
        T GetAsync<T>(string url, bool enableMaxTimeout = false);

        void DeleteAsync(string url, bool enableMaxTimeout = false);

        void PostAsJsonAsync<T>(string url, T entity, bool enableMaxTimeout = false) where T : class;

        T PostAsJsonAsyncWithReturnValue<T>(string url, T entity, bool enableMaxTimeout = false) where T : class;

        void PutAsJsonAsync<T>(string url, T entity, bool enableMaxTimeout = false) where T : class;

        string GetAccessToken(string userID, string password, out UserDto userDto, string clientId);
    }
}