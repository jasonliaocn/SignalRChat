using SignalRChat.Dtos;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.DomainManager
{
    public static class UserViewModelExtension
    {
        public static UserViewModel ToViewModel(this UserDto userDto)
        {
            if (userDto == null) return null;
            UserViewModel model = new UserViewModel();
            model.UserId = userDto.UserId;
            model.UserName = userDto.UserName;
            model.Password = userDto.Password;
            model.UserFirstName = userDto.UserFirstName;
            model.UserLastName = userDto.UserLastName;
            model.UserGender = userDto.UserGender;
            model.UserIsActive = userDto.UserIsActive;
            model.UserIsOnLine = userDto.UserIsOnLine;
            model.UserType = userDto.UserType;
            model.UserDisplayName = userDto.UserDisplayName;
            return model;
        }

        public static UserDto ToDtoModel(this UserViewModel model)
        {
            if (model == null) return null;
            UserDto dto = new UserDto();
            dto.UserId = model.UserId;
            dto.UserName = model.UserName;
            dto.Password = model.Password;
            dto.UserFirstName = model.UserFirstName;
            dto.UserLastName = model.UserLastName;
            dto.UserGender = model.UserGender;
            dto.UserIsActive = model.UserIsActive;
            dto.UserIsOnLine = model.UserIsOnLine;
            dto.UserType = model.UserType;
            return dto;
        }
    }
}