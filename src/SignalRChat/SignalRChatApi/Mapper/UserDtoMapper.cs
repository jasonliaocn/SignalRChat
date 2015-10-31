using SignalRChat.Dtos;
using SignalRChatApi.Models;

namespace SignalRChatApi.Mapper
{
    public class UserDtoMapper : Mapper<User, UserDto>
    {
        public override UserDto Map(User source)
        {
            if (source == null) return null;
            var dto = new UserDto();
            dto.UserId = source.UserId;
            dto.UserName = source.UserName;
            dto.Password = source.UserPassword;
            dto.UserFirstName = source.UserFirstName;
            dto.UserLastName = source.UserLastName;
            dto.UserGender = source.UserGender;
            dto.UserIsActive = source.UserIsActive;
            dto.UserType = source.UserType;
            dto.UserIsOnLine = source.UserIsOnLine;            
            //List<UserRoleDto> userRole = new List<UserRoleDto>();
            //foreach (var item in source.BnCUserAccess)
            //{
            //    UserRoleDto role = new UserRoleDto();
            //    var store = item.BnCStore;
            //    role.RoleID = item.RoleID;
            //    role.RoleName = item.BnCUserRole.RoleDesc;
            //    role.StoreNumber = item.StoreNumber;
            //    if (store != null)
            //    {
            //        role.StoreName = store.StoreName;
            //        role.IsBulkReceipt = store.IsBulkReceipt.HasValue ? item.BnCStore.IsBulkReceipt.Value : false;
            //    }
            //    var featureIds = new List<short>();
            //    foreach (var feature in item.BnCUserRole.BnCRoleFeature)
            //    {
            //        featureIds.Add(feature.FeatureID);
            //    }
            //    role.FeatureId = featureIds;
            //    userRole.Add(role);
            //}
            //dto.Role = userRole;
            //dto.RowVersion = source.ModifiedDate.ToNullableLong();
            return dto;
        }

        public override User Map(UserDto source)
        {
            var user = new User();
            if (source == null) return null;
            user.UserId = source.UserId;
            user.UserName = source.UserName;
            user.UserPassword = source.Password;
            user.UserFirstName = source.UserFirstName;
            user.UserLastName = source.UserLastName;
            user.UserGender = source.UserGender;
            user.UserIsActive = source.UserIsActive;
            user.UserType = source.UserType;
            user.UserIsOnLine = source.UserIsOnLine;
            user.CreatedOn = System.DateTime.Now;
            user.LastLogOnDate = System.DateTime.Now;
            return user;
        }
    }
}