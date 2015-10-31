using SignalRChat.Common.ApiClient;
using SignalRChat.DomainManager.Interfaces;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using SignalRChat.Common.Utility;
using System.Web.Security;
using SignalRChat.Common;

namespace SignalRChat.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserManager userManager;

        public AccountController(IUserManager userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");

            this.userManager = userManager;
        }

        //
        // GET: /Account/Login
        //[AuthorizationFilter(Enumeration.Feature.Login)]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // Post: /Account/Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserViewModel user;
                    var token = userManager.GetAccessToken(model.UserName, model.Password, out user);
                    CookieHelper.SetCookie<string>(ApplicationConstants.COOKIE_TOKEN, token);
                    //UserViewModel user = userManager.GetViewModel(model.UserName);
                    if (user != null)
                    {
                        if (!user.UserIsActive)
                        {
                            ModelState.AddModelError(string.Empty, "ERROR_LOGIN_INACTIVATED");
                        }
                        else
                        {
                            //this.HttpContext.GetOwinContext().Authentication.OwinSignIn(user);
                            if (!model.Password.MD5EncryptToString().Equals(user.Password))
                                ModelState.AddModelError(string.Empty, "ERROR_LOGIN_Wrong Password");
                            else
                            {
                                this.HttpContext.GetOwinContext().Authentication.OwinSignIn(user);
                                //FormsAuthentication.SetAuthCookie(user.UserName,false);                                
                                return RedirectToLocal(returnUrl);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Login Incorrect");
                    }
                }
                catch (AuthenticationException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Logout()
        {
            UserViewModel user = userManager.GetViewModel(this.UserID);
            user.UserIsOnLine = false;
            user = userManager.UpdateUserOnLineStatus(user);
            this.HttpContext.GetOwinContext().Authentication.OwinSignOut();
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(UserViewModel user, string returnUrl)
        {
            user.Password = user.Password.MD5EncryptToString();
            user = userManager.AddUser(user);
            this.HttpContext.GetOwinContext().Authentication.OwinSignIn(user);
            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}