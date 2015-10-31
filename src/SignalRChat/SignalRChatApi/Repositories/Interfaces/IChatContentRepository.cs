using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatApi.Repositories
{
    public interface IChatContentRepository : IRepositoryBase<ChatContent> 
    {
        IEnumerable<ChatContent> GetAllChats();

        IEnumerable<ChatContent> GetAllChatsInSession(int sessionId);
    }
}
