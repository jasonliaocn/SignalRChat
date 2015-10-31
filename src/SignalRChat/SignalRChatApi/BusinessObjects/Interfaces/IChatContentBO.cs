using System;
namespace SignalRChatApi.BusinessObjects
{
    public interface IChatContentBO
    {
        System.Collections.Generic.IEnumerable<SignalRChat.Dtos.ChatContentDto> GetChatContents();

        System.Collections.Generic.IEnumerable<SignalRChat.Dtos.ChatContentDto> GetSessionChatContents(int sessionId);

        SignalRChat.Dtos.ChatContentDto AddChatContent(SignalRChat.Dtos.ChatContentDto Dto);
    }
}
