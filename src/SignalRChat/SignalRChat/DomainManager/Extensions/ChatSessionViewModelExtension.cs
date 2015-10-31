using SignalRChat.Dtos;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public static class ChatSessionViewModelExtension
    {
        public static ChatSessionViewModel ToViewModel(this ChatSessionDto chatsessionDto)
        {
            if (chatsessionDto == null) return null;
            ChatSessionViewModel model = new ChatSessionViewModel();
            model.SessionId = chatsessionDto.Id;
            model.Topic = chatsessionDto.Topic;
            model.Starter = chatsessionDto.Starter.ToViewModel();
            if (chatsessionDto.Joinner !=null)
            {
                chatsessionDto.Joinner.ForEach(j => model.Joinner.Add(j.ToViewModel()));
            }
            if (chatsessionDto.ChatContents!=null)
            {
                chatsessionDto.ChatContents.ForEach(c => model.ChatContents.Add(c.ToViewModel()));
            }
            model.StartOn = chatsessionDto.StartOn;
            return model;
        }

        public static ChatSessionDto ToDtoModel(this ChatSessionViewModel model)
        {
            if (model == null) return null;
            ChatSessionDto dto = new ChatSessionDto();
            dto.Id = model.SessionId;
            dto.Topic = model.Topic;
            dto.StartBy = model.StartBy;
            dto.StartOn = model.StartOn;
            dto.IsFinished = model.IsFinished;
            if (model.Joinner.Count > 0)
            {
                model.Joinner.ForEach(a =>
                {
                    dto.Joinner.Add(a.ToDtoModel());
                });
            }
            return dto;
        }
    }
}