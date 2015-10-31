using SignalRChat.Dtos;
using SignalRChatApi.BusinessObjects;
using SignalRChatApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignalRChatApi.Controllers
{
    public class ChatController : ApiController
    {
        private readonly IChatContentBO chatBO;

        public ChatController(IChatContentBO chatBO)
        {
            this.chatBO = chatBO;
        }

        public IEnumerable<ChatContentDto> GetChats()
        {
            return chatBO.GetChatContents();
        }

        [HttpGet]
        [Route("api/Chat/ChatInSession/{sessionId}")]
        public IEnumerable<ChatContentDto> GetChatInSession(int sessionId)
        {
            return chatBO.GetSessionChatContents(sessionId);
        }

        [HttpPost]
        public void Add([FromBody]ChatContentDto dto)
        {
            if (ModelState.IsValid)
            {
                chatBO.AddChatContent(dto);
            }
            else
            {
                string message = ModelState.ToErrorMessage();
                throw new Exception(message);
            }
        }
    }
}
