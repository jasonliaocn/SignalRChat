using SignalRChat.Dtos;
using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatApi.Mapper
{
    public class ChatContentDtoMapper : Mapper<ChatContent, ChatContentDto>
    {
        public override ChatContentDto Map(ChatContent source)
        {
            if (source == null) return null;
            var dto = new ChatContentDto();
            dto.Id = source.Id;
            dto.SendBy = source.SendBy;
            dto.SendTo = source.SendTo;
            dto.SendOn = source.SendOn;
            dto.SessionId = source.SessionId;
            dto.MessageContent = source.MessageContent;
            return dto;
        }

        public override ChatContent Map(ChatContentDto source)
        {
            if (source == null) return null;
            var chat = new ChatContent();
            chat.SessionId = source.SessionId;
            chat.SendBy = source.SendBy;
            chat.SendTo = source.SendTo;
            chat.MessageContent = source.MessageContent;
            chat.SendOn = source.SendOn;
            return chat;
        }
    }
}