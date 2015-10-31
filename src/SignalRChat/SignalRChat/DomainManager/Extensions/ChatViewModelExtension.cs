using SignalRChat.Dtos;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public static class ChatViewModelExtension
    {
        public static ChatContentViewModel ToViewModel(this ChatContentDto chatDto)
        {
            if (chatDto == null) return null;
            ChatContentViewModel model = new ChatContentViewModel();

            model.Id = chatDto.Id;
            model.SendBy = chatDto.SendBy;
            model.SendTo = chatDto.SendTo;
            model.SendOn = chatDto.SendOn;
            model.MessageContent = chatDto.MessageContent; 
            return model;
        }
    }
}