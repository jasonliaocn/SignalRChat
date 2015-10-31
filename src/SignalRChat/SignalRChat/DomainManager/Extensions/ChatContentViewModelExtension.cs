using SignalRChat.Dtos;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public static class ChatContentViewModelExtension
    {
        public static ChatContentDto ToDtoModel(this ChatContentViewModel model)
        {
            if (model == null) return null;
            ChatContentDto dto = new ChatContentDto();
            dto.SessionId = model.SessionId;
            dto.SendBy = model.SendBy;
            dto.SendTo = model.SendTo;
            dto.MessageContent = model.MessageContent;
            dto.SendOn = model.SendOn;
            return dto;
        }
    }
}