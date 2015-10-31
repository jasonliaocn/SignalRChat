using System;
namespace SignalRChatApi.BusinessObjects
{
    public interface IUserBO
    {
        SignalRChat.Dtos.UserDto GetUser(int userID);
        SignalRChat.Dtos.UserDto GetUser(string userName);

        bool UserIsOnLine(int userId);

        SignalRChat.Dtos.UserDto UpdateUser(SignalRChat.Dtos.UserDto user);

        SignalRChat.Dtos.UserDto UserOnLine(SignalRChat.Dtos.UserDto user);

        System.Collections.Generic.IEnumerable<SignalRChat.Dtos.UserDto> GetOnLineUser();

        SignalRChat.Dtos.UserDto AddUser(SignalRChat.Dtos.UserDto user);
    }
}
