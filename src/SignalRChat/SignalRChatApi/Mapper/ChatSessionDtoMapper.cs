using SignalRChat.Dtos;
using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatApi.Mapper
{
    public class ChatSessionDtoMapper : Mapper<ChatSession, ChatSessionDto>
    {
        public override ChatSessionDto Map(ChatSession source)
        {
            if (source == null) return null;
            var dto = new ChatSessionDto();
            dto.Id = source.Id;
            dto.Topic = source.Topic;
            if(source.User !=null)
            {
                dto.Starter.UserId = source.User.UserId;
                dto.Starter.UserName = source.User.UserName;
                dto.Starter.UserFirstName = source.User.UserFirstName;
                dto.Starter.UserLastName = source.User.UserLastName;
            }
            if(source.ChatSessionJoinners!=null)
            {
                foreach (var joinner in source.ChatSessionJoinners)
                {
                    UserDto user = new UserDto
                    {
                        UserId = joinner.User.UserId,
                        UserName = joinner.User.UserName,
                        UserFirstName = joinner.User.UserFirstName,
                        UserLastName = joinner.User.UserLastName,
                    };
                    dto.Joinner.Add(user);
                }
            }
            if(source.ChatContents!=null)
            {
                foreach(var content in source.ChatContents)
                {
                    ChatContentDto cdto = new ChatContentDto
                    {
                        Id = content.Id,
                        MessageContent = content.MessageContent,
                        SendBy = content.SendBy,
                        SendOn = content.SendOn,
                        SendTo = content.SendTo
                    };
                    dto.ChatContents.Add(cdto);
                }
            }

            dto.StartOn = source.StartOn;
            dto.IsFinished = source.IsFinished;
            dto.FinishedOn = source.FinishedOn;
            return dto;
        }

        public override ChatSession Map(ChatSessionDto source)
        {
            if (source == null) return null;
            var entity = new ChatSession();
            entity.Id = source.Id;
            entity.Topic = source.Topic;
            if (source.Joinner != null)
            {
                foreach (var joiner in source.Joinner)
                {
                    ChatSessionJoinner csj = new ChatSessionJoinner
                    {
                        SessionId = source.Id,
                        UserId = joiner.UserId,
                        JoinedOn = source.StartOn
                    };
                    entity.ChatSessionJoinners.Add(csj);
                }
            }

            entity.StartOn = source.StartOn;
            entity.IsFinished = source.IsFinished;
            entity.FinishedOn = source.FinishedOn;
            return entity;
        }
    }
}