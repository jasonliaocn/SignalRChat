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
    public class ChatSessionBO : IChatSessionBO
    {
        private readonly IChatSessionRepository chatSessionRepository;
        private readonly IChatSessionJoinnerRepository chatSessionJoinnerRepository;
        private readonly IMapper<ChatSession, ChatSessionDto> chatSessionDtoMapper;

        public ChatSessionBO(IChatSessionRepository chatSessionRepository, IChatSessionJoinnerRepository chatSessionJoinnerRepository, IMapper<ChatSession, ChatSessionDto> chatSessionDtoMapper)
        {
            this.chatSessionRepository = chatSessionRepository;
            this.chatSessionJoinnerRepository = chatSessionJoinnerRepository;
            this.chatSessionDtoMapper = chatSessionDtoMapper;
        }

        public IEnumerable<ChatSessionDto> LoadUserChatSession(int userId)
        {
            List<ChatSessionDto> listSession = new List<ChatSessionDto>();
            chatSessionRepository.ToList().
                ForEach(cs => 
                    {
                        if (cs.ChatSessionJoinners.Count > 0)
                        {
                            cs.ChatSessionJoinners.ToList().ForEach(csj =>
                                {
                                    if (csj.User.UserId.Equals(userId))
                                        listSession.Add(chatSessionDtoMapper.Map(cs));
                                });
                        }
                            
                    }
                );
            return listSession;
        }

        public ChatSessionDto LoadChatSessionById(int sessionId)
        {
            return chatSessionDtoMapper.Map(chatSessionRepository.FirstOrDefault(c => c.Id == sessionId));
        }

        public ChatSessionDto EndChatSession(ChatSessionDto chatsession)
        {
            ChatSession entity = chatSessionDtoMapper.Map(chatsession);
            chatSessionRepository.Update(entity, null);
            return chatsession;
        }

        public IEnumerable<ChatSessionDto> LoadChatSessions()
        {
            List<ChatSessionDto> listSession = new List<ChatSessionDto>();
            chatSessionRepository.ToList().ForEach(cs => listSession.Add(chatSessionDtoMapper.Map(cs)));
            return listSession;
        }


        public ChatSessionDto JoinChatSession(ChatSessionDto chatsession)
        {
            ChatSession entity = chatSessionDtoMapper.Map(chatsession);
            foreach(var joinner in entity.ChatSessionJoinners)
            {
                chatSessionJoinnerRepository.Add(joinner);
            }
            
            return chatsession;
        }


        public int CreateChatSession(ChatSessionDto chatsession)
        {
            ChatSession entity = chatSessionDtoMapper.Map(chatsession);
            chatSessionRepository.Add(entity);
            return entity.Id;
        }
    }
}