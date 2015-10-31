using System.Web.Mvc;
using SignalRChat.DomainManager.Interfaces;

namespace SignalRChat.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatManager chatManager;
        private readonly IUserManager userManager;

        public ChatController(IChatManager chatManager, IUserManager userManager)
        {
            this.chatManager = chatManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetUserChatSession()
        {
            return PartialView("_ChatSessionList", chatManager.GetUserChatSession(this.UserID));
        }

        [AuthorizationFilter]
        public ActionResult GetOnlineUser()
        {
            var result = userManager.GetOnlineUser();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Chat/
        public ActionResult GetChats()
        {
            return PartialView("_ChatList", chatManager.GetChatsViewModel());
        }

        public ActionResult JoinChat(int sessionId)
        {
            return PartialView("_ChatRoom", chatManager.GetChatContentsInSessions(sessionId));
        }


    }
}
