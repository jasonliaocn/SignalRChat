using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// GET Login UserID
        /// </summary>
        protected int UserID
        {
            get
            {
                if (this.User.Identity != null)
                {
                    return int.Parse(this.User.Identity.GetUserId());
                }
                else
                {
                    return -1;
                }
            }
        }

        protected string UserName
        {
            get
            {
                if (this.User.Identity != null)
                {
                    return this.User.Identity.GetUserName();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
	}
}