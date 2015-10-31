using SignalRChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatApi.BusinessObjects
{
    public interface IChatSessionBO
    {
        IEnumerable<ChatSessionDto> LoadChatSessions();

        IEnumerable<ChatSessionDto> LoadUserChatSession(int userId);

        ChatSessionDto LoadChatSessionById(int sessionId);

        int CreateChatSession(ChatSessionDto chatsession);

        ChatSessionDto EndChatSession(ChatSessionDto chatsession);

        ChatSessionDto JoinChatSession(ChatSessionDto chatsession);
    }
}
