using SignalRChatApi.Models;
using System;
namespace SignalRChatApi.Repositories
{
    public interface IUserRepository:IRepositoryBase<User> 
    {
        //User GetByID(int id);
        //User GetUserByUserName(string userName);
    }
}
