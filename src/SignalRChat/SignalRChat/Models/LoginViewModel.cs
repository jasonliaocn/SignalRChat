using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        //[RegularExpression(ApplicationConstants.REGULAR_PASSWORD, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ERROR_PASSWORD")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}