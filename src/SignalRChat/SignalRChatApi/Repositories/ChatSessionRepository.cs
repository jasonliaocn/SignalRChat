using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatApi.Repositories
{
    public class ChatSessionRepository : RepositoryBase<ChatSession>, IChatSessionRepository
    {
        public ChatSessionRepository()
            : base()
        {

        }
    }
}