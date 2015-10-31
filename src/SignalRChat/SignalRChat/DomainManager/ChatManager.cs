using SignalRChat.Common.ApiClient;
using SignalRChat.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public class ChatManager : SignalRChat.DomainManager.Interfaces.IChatManager
    {
        private readonly IHttpClient httpClient;
        public ChatManager(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public List<Models.ChatContentViewModel> GetChatsViewModel()
        {
            var chatList = httpClient.GetAsync<List<ChatContentDto>>(string.Format("Api/Chat/GetChats"));
            List<Models.ChatContentViewModel> result = new List<Models.ChatContentViewModel>();
            chatList.ForEach(a => result.Add(a.ToViewModel()));
            return result;
        }

        public List<Models.ChatSessionViewModel> GetUserChatSession(int userId)
        {
            var chatSessionList = httpClient.GetAsync<List<ChatSessionDto>>(string.Format("api/ChatSession/{0}/Sessions", userId));
            List<Models.ChatSessionViewModel> result = new List<Models.ChatSessionViewModel>();
            chatSessionList.ForEach(a => result.Add(a.ToViewModel()));
            return result;
        }

        public List<Models.ChatSessionViewModel> GetAllSessionsViewModel()
        {
            var chatSessionList = httpClient.GetAsync<List<ChatSessionDto>>(string.Format("api/ChatSession"));
            List<Models.ChatSessionViewModel> result = new List<Models.ChatSessionViewModel>();
            chatSessionList.ForEach(a => result.Add(a.ToViewModel()));
            return result;
        }


        public List<Models.ChatContentViewModel> GetChatContentsInSessions(int sessionId)
        {
            var chatContentList = httpClient.GetAsync<List<ChatContentDto>>(string.Format("Api/Chat/ChatInSession/{0}", sessionId.ToString()));
            List<Models.ChatContentViewModel> result = new List<Models.ChatContentViewModel>();
            chatContentList.ForEach(a => result.Add(a.ToViewModel()));
            return result;
        }


        public void AddChatContent(Models.ChatContentViewModel chatcontent)
        {
            var chatcontentDto = chatcontent.ToDtoModel();
            httpClient.PostAsJsonAsync("api/Chat", chatcontent, true);            
        }


        public int AddChatSession(Models.ChatSessionViewModel chatsession)
        {
            var chatSessionDto = chatsession.ToDtoModel();
            var chatSessionId = httpClient.GetAsync<int>(string.Format("api/ChatSession/CreateChatSession?Topic={0}&StartBy={1}&StartOn={2}&IsFinished={3}", chatSessionDto.Topic, chatSessionDto.StartBy, chatSessionDto.StartOn, chatSessionDto.IsFinished));
            return chatSessionId;
        }


        public void JoinSession(Models.ChatSessionViewModel chatsession)
        {
            var chatSessionDto = chatsession.ToDtoModel();
            httpClient.PostAsJsonAsync("api/ChatSession/JoinToSession", chatSessionDto, true);  
        }


        public Models.ChatSessionViewModel GetChatSession(int sessionId)
        {
            var chatSession = httpClient.GetAsync<ChatSessionDto>(string.Format("api/ChatSession/Session/{0}", sessionId));

            return chatSession.ToViewModel();
        }
    }
}