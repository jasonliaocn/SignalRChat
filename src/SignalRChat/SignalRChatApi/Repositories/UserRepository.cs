using SignalRChatApi.Models;
using System;
using System.Linq;

namespace SignalRChatApi.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        //private MyChatEntities db = new MyChatEntities();

        public UserRepository()
            : base()
        {

        }

        //public User GetByID(int id)
        //{
        //    return ActiveContext.Users.FirstOrDefault(u => u.UserId == id);
        //}

        //public User GetUserByUserName(string userName)
        //{
        //    return ActiveContext.Users.FirstOrDefault(u => u.UserName == userName);
        //}

        //protected void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (ActiveContext != null)
        //        {
        //            ActiveContext.Dispose();
        //            ActiveContext = null;
        //        }
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}