using SignalRChat.Dtos;
using SignalRChatApi.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SignalRChatApi.Extensions;

namespace SignalRChatApi.Controllers
{
    public class ChatSessionController : ApiController
    {
        private readonly IChatSessionBO chatSessionBO;
        public ChatSessionController(IChatSessionBO chatSessionBO)
        {
            this.chatSessionBO = chatSessionBO;
        }

        [HttpGet]
        public IEnumerable<ChatSessionDto> Get()
        {
            var response = chatSessionBO.LoadChatSessions();
            return response;
        }

        [HttpGet]
        [Route("api/ChatSession/{userid}/Sessions")]
        public IEnumerable<ChatSessionDto> GetSessionByUser(int userid)
        {
            var response = chatSessionBO.LoadUserChatSession(userid);
            return response;
        }

        [HttpGet]
        [Route("api/ChatSession/Session/{sessionid}")]
        public ChatSessionDto GetSessionById(int sessionid)
        {
            var response = chatSessionBO.LoadChatSessionById(sessionid);
            return response;
        }

        [HttpPut]
        public ChatSessionDto Update([FromBody]ChatSessionDto dto)
        {
            if (ModelState.IsValid)
            {
                return chatSessionBO.EndChatSession(dto);
            }
            else
            {
                string message = ModelState.ToErrorMessage();
                throw new Exception(message);
            }
        }

        [HttpPut]
        [Route("api/ChatSession/JoinSession/{sessionid}")]
        public ChatSessionDto JoinSession([FromBody]ChatSessionDto dto)
        {
            if (ModelState.IsValid)
            {
                return chatSessionBO.EndChatSession(dto);
            }
            else
            {
                string message = ModelState.ToErrorMessage();
                throw new Exception(message);
            }
        }

        [HttpGet]
        [Route("api/ChatSession/CreateChatSession")]
        public int CreateChatSession([FromUri]ChatSessionDto dto)
        {
            if (dto == null) { throw new ArgumentNullException("request"); }

            return chatSessionBO.CreateChatSession(dto);
        }

        [HttpPost]
        [Route("api/ChatSession/JoinToSession")]
        public void JoinToSession([FromBody]ChatSessionDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("ChatSessionDto");
            }
            var dto2 = chatSessionBO.JoinChatSession(dto);
        }
    }
}
