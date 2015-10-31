using SignalRChat.Common.ApiClient;
using SignalRChat.DomainManager.Interfaces;
using SignalRChat.Dtos;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public class UserManager : IUserManager
    {
        private readonly IHttpClient httpClient;
        public UserManager(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Models.UserViewModel GetViewModel(string userName)
        {
            var userDto = httpClient.GetAsync<UserDto>(string.Format("Api/User/Name/{0}", userName));
            
            return userDto.ToViewModel();
        }

        public Models.UserViewModel GetViewModel(int userId)
        {
            var userDto = httpClient.GetAsync<UserDto>(string.Format("Api/User/{0}", userId));

            return userDto.ToViewModel();
        }

        public Models.UserViewModel AddUser(Models.UserViewModel user)
        {
            if (user == null) { throw new ArgumentNullException("user"); }

            var userDto = httpClient.PostAsJsonAsyncWithReturnValue("api/User", user.ToDtoModel());

            return userDto.ToViewModel();
        }

        public string GetAccessToken(string userID, string password, out UserViewModel user)
        {
            var token = GetAccessToken(userID, password, out user, "");
            return token;
        }

        private string GetAccessToken(string userID, string password, out UserViewModel user, string clientId = "")
        {
            UserDto userDto;
            var token = httpClient.GetAccessToken(userID, password, out userDto, clientId);
            user = userDto.ToViewModel();
            return token;
        }

        public Models.UserViewModel UpdateUserOnLineStatus(Models.UserViewModel user)
        {
            var userDto = user.ToDtoModel();
            httpClient.PutAsJsonAsync("api/User/OnLine", user, true);
            return user;
        }


        public List<Models.UserViewModel> GetOnlineUser()
        {
            var userList = httpClient.GetAsync<List<UserDto>>(string.Format("Api/User/GetOnLineUser"));
            List<Models.UserViewModel> result = new List<Models.UserViewModel>();
            userList.ForEach(a => result.Add(a.ToViewModel()));
            return result;
        }
    }
}