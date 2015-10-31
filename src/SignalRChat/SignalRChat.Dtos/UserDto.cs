using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SignalRChat.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [Required]
        [Display(Name = "User Name:")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "First Name:")]
        [MaxLength(35)]
        public string UserFirstName { get; set; }

        [Display(Name = "Last Name:")]
        [MaxLength(35)]
        public string UserLastName { get; set; }

        [Display(Name = "Gender:")]
        [MaxLength(1)]
        public string UserGender { get; set; }

        [Display(Name = "Status:")]
        public bool UserIsActive { get; set; }

        [Display(Name = "UserType:")]
        public int UserType { get; set; }

        public bool UserIsOnLine { get; set; }

        [Display(Name = "Display Name:")]
        public string UserDisplayName { get { return UserFirstName + " " + UserLastName; } }
    }
}
