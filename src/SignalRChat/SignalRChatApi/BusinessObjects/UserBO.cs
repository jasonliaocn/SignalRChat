using SignalRChat.Dtos;
using SignalRChatApi.Mapper;
using SignalRChatApi.Models;
using SignalRChatApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatApi.BusinessObjects
{
    public class UserBO : SignalRChatApi.BusinessObjects.IUserBO
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper<User, UserDto> userDtoMapper;
        public UserBO(IUserRepository userRepository, IMapper<User, UserDto> userDtoMapper)
        {
            this.userRepository = userRepository;
            this.userDtoMapper = userDtoMapper;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userName">user Name</param>
        /// <returns>UserDto</returns>
        public UserDto GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) { throw new ArgumentNullException("userName"); }

            var user = userRepository.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            return userDtoMapper.Map(user);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns>UserDto</returns>
        public UserDto GetUser(int userID)
        {
            var user = userRepository.AsNoTracking().FirstOrDefault(x => x.UserId.Equals(userID));
            return userDtoMapper.Map(user);
        }


        public bool UserIsOnLine(int userId)
        {
            var user = userRepository.AsNoTracking().FirstOrDefault(x => x.UserId.Equals(userId));
            return user.UserIsOnLine;
        }


        public UserDto UpdateUser(UserDto user)
        {
            User entity = userDtoMapper.Map(user);
            userRepository.Update(entity, null);
            return user;
        }


        public UserDto UserOnLine(UserDto user)
        {
            User entity = userDtoMapper.Map(user);
            userRepository.Update(entity, x => new { x.UserIsOnLine});
            return user;
        }


        public IEnumerable<UserDto> GetOnLineUser()
        {
            var user = userRepository.AsNoTracking().Where(u=>u.UserIsOnLine == true).ToList();
            return userDtoMapper.MapOrEmptyList(user);
        }


        public UserDto AddUser(UserDto user)
        {
            User entity = userDtoMapper.Map(user);
            userRepository.Add(entity);
            user.UserId = entity.UserId;
            return user;
        }
    }
}