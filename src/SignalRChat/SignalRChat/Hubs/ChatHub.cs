using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Configuration;
using System.Diagnostics;
using SignalRChat.DomainManager.Interfaces;
using System.Threading.Tasks;
using SignalRChat.DomainManager;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignalRChat.Models;
using Autofac;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILifetimeScope _hubLifetimeScope;
        private readonly IUserManager userManager;
        private readonly IChatManager chatManager;
        private readonly static ConnectionMapping<int> _connections =
            new ConnectionMapping<int>();
        public ChatHub(ILifetimeScope lifetimeScope)
        {
            _hubLifetimeScope = lifetimeScope.BeginLifetimeScope();
            if (userManager == null)
                //this.userManager = (IUserManager)DependencyResolver.Current.GetService(typeof(IUserManager));
                this.userManager = _hubLifetimeScope.Resolve<IUserManager>();
            if (chatManager == null)
                //this.chatManager = (IChatManager)DependencyResolver.Current.GetService(typeof(IChatManager));
                this.chatManager = _hubLifetimeScope.Resolve<IChatManager>();
        }

        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            var user = Context.User;
            int userId = int.Parse(user.Identity.GetUserId());

            _connections.Add(userId, id);

            //userManager = (IUserManager)DependencyResolver.Current.GetService(typeof(IUserManager));
            Models.UserViewModel uservm = userManager.GetViewModel(userId);
            uservm.UserIsOnLine = true;
      
            uservm = userManager.UpdateUserOnLineStatus(uservm);

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            Clients.AllExcept(id).userConnected(userId, uservm.UserDisplayName);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            int userId = int.Parse(Context.User.Identity.GetUserId());

            _connections.Remove(userId, id);

            Clients.AllExcept(id).userDisconnected(userId);
            return base.OnDisconnected(stopCalled);
        }

        public void SendMessages(string Message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.updateMessages();
        }

        public void SendMessageToSession(bool isFstMessage, int sessionId, string message, int[] userIdlist)
        {
            var id = Context.ConnectionId;
            string userName = Context.User.Identity.GetUserName();
            int userId = int.Parse(Context.User.Identity.GetUserId());

            if (isFstMessage && userIdlist.Count() > 1)
            {
                if (!JoinSession(sessionId, userIdlist))
                    throw new Exception("Failed to join Session");
            }

            if (StoreMessage(sessionId, message))
            {
                Clients.Caller.updateActiveSessionMessages(sessionId, userName, message);
                foreach (var uid in userIdlist.Except(new int[] { userId }))
                {
                    foreach (var connectionId in _connections.GetConnections(uid))
                    {
                        Clients.Client(connectionId).updateSessionMessages(sessionId, userName, message, userIdlist);
                    }
                }
                //Clients.AllExcept(id).updateSessionMessages(sessionId, userName, message);
            }
            return;
        }

        public void SendMessageToNewSession(int sessionId, string message, int[] userIdlist)
        {
            var id = Context.ConnectionId;
            string userName = Context.User.Identity.GetUserName();
            int userId = int.Parse(Context.User.Identity.GetUserId());
            
            if(userIdlist.Length>0 && JoinSession(sessionId,userIdlist))
            {
                if (StoreMessage(sessionId, message))
                {
                    Clients.Caller.updateActiveSessionMessages(sessionId, userName, message);
                    foreach (var uid in userIdlist.Except(new int[]{userId}))
                    {
                        foreach (var connectionId in _connections.GetConnections(uid))
                        {
                            Clients.Client(connectionId).updateSessionMessages(sessionId, userName, message, userIdlist);
                        }
                    }
                    //Clients.AllExcept(id).updateSessionMessages(sessionId, userName, message);
                }
            }
            return;
        }

        public int StartNewSession(string topic, int startBy)
        {
            int sessionId = StoreSession(topic, startBy);
            return sessionId;
        }

        private bool JoinSession(int sessionId, int[] userIdlist)
        {
            try
            {
                //userManager = (IUserManager)DependencyResolver.Current.GetService(typeof(IUserManager));
                //var chatManager = (IChatManager)DependencyResolver.Current.GetService(typeof(IChatManager));
                ChatSessionViewModel chatsession = chatManager.GetChatSession(sessionId);
                foreach (int userid in userIdlist)
                {
                    UserViewModel user = userManager.GetViewModel(userid);
                    chatsession.Joinner.Add(user);
                }
                
                chatManager.JoinSession(chatsession);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private int StoreSession(string topic, int startBy)
        {
            try
            {
                ChatSessionViewModel chatsession = new ChatSessionViewModel
                {
                    Topic = topic,
                    StartBy = startBy,
                    StartOn = System.DateTime.Now
                };
                //var chatManager = (IChatManager)DependencyResolver.Current.GetService(typeof(IChatManager));
                int sessionId = chatManager.AddChatSession(chatsession);
                return sessionId;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private bool StoreMessage(int sessionId, string message)
        {
            try
            {
                int userId = int.Parse(Context.User.Identity.GetUserId());
                ChatContentViewModel chatcontent = new ChatContentViewModel
                {
                    SendBy = userId,
                    SendTo = -1,
                    SessionId = sessionId,
                    MessageContent = message,
                    SendOn = System.DateTime.Now
                };
                //var chatManager = (IChatManager)DependencyResolver.Current.GetService(typeof(IChatManager));
                chatManager.AddChatContent(chatcontent);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            // Dipose the hub lifetime scope when the hub is disposed.
            if (disposing && _hubLifetimeScope != null)
            {
                _hubLifetimeScope.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}