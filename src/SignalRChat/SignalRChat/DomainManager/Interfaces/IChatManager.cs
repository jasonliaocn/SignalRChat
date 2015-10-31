using System;
namespace SignalRChat.DomainManager.Interfaces
{
    public interface IChatManager
    {
        System.Collections.Generic.List<SignalRChat.Models.ChatContentViewModel> GetChatsViewModel();

        System.Collections.Generic.List<Models.ChatSessionViewModel> GetUserChatSession(int userId);

        System.Collections.Generic.List<Models.ChatContentViewModel> GetChatContentsInSessions(int sessionId);

        void AddChatContent(Models.ChatContentViewModel chatcontent);

        int AddChatSession(Models.ChatSessionViewModel chatsession);

        void JoinSession(Models.ChatSessionViewModel chatsession);

        Models.ChatSessionViewModel GetChatSession(int sessionId);
    }
}
