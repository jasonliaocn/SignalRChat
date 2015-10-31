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
    public class ChatContentBO : SignalRChatApi.BusinessObjects.IChatContentBO
    {
        private readonly IChatContentRepository chatContentRepository;
        private readonly IMapper<ChatContent, ChatContentDto> chatContentDtoMapper;
        public ChatContentBO(IChatContentRepository chatContentRepository, IMapper<ChatContent, ChatContentDto> chatContentDtoMapper)
        {
            this.chatContentRepository = chatContentRepository;
            this.chatContentDtoMapper = chatContentDtoMapper;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userName">user Name</param>
        /// <returns>UserDto</returns>
        public IEnumerable<ChatContentDto> GetChatContents()
        {
            var chat = chatContentRepository.GetAllChats();
            return chatContentDtoMapper.MapOrEmptyList(chat);
        }

        public IEnumerable<ChatContentDto> GetSessionChatContents(int sessionId)
        {
            var chat = chatContentRepository.GetAllChatsInSession(sessionId);
            return chatContentDtoMapper.MapOrEmptyList(chat);
        }

        public ChatContentDto AddChatContent(ChatContentDto Dto)
        {
            ChatContent entity = chatContentDtoMapper.Map(Dto);
            chatContentRepository.Add(entity);
            return Dto;
        }
    }
}