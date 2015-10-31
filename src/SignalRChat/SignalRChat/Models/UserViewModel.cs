using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        [Required]
        [Display(Name = "User Name:")]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        [System.Web.Mvc.Compare("Password")]
        public string ConfirmPassWord { get; set; }

        [Display(Name="First Name:")]
        public string UserFirstName { get; set; }

        [Display(Name = "Last Name:")]
        public string UserLastName { get; set; }

        [Display(Name = "Gender:")]
        public string UserGender { get; set; }

        [Display(Name = "Company:")]
        public string UserCompany { get; set; }

        [Display(Name = "Designation:")]
        public string UserDesignation { get; set; }

        [Display(Name = "Status:")]
        public bool UserIsActive { get; set; }

        [Display(Name = "UserType:")]
        public int UserType { get; set; }

        public bool UserIsOnLine { get; set; }

        public string UserDisplayName { get; set; }
    }
}