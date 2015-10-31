using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.DomainManager.Interfaces
{
    public interface IUserManager
    {
        UserViewModel GetViewModel(string userName);

        Models.UserViewModel GetViewModel(int userId);

        Models.UserViewModel AddUser(UserViewModel user);

        string GetAccessToken(string userId, string password, out UserViewModel user);

        UserViewModel UpdateUserOnLineStatus(UserViewModel user);

        System.Collections.Generic.List<UserViewModel> GetOnlineUser();
    }
}
