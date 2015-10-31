using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignalRChatApi.Models;
using SignalRChatApi.Mapper;
using SignalRChat.Dtos;
using SignalRChatApi.Repositories;
using SignalRChatApi.BusinessObjects;
using System.Collections.Generic;
using System.Net.Http;

namespace SignalRChatApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserBO userBO;

        public UserController(IUserBO userBO)
        {
            this.userBO = userBO;
        }

        // GET api/User
        public IQueryable<User> GetUsers()
        {
            return null;
        }

        // GET api/User/5
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult GetUser(int id)
        {
            UserDto user = userBO.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/User/5
        [HttpGet]
        [Route("api/User/Name/{userName}")]
        public UserDto GetUserByName(string userName)
        {
            UserDto user = userBO.GetUser(userName);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        [Route("api/User/GetOnLineUser")]
        [ResponseType(typeof(IEnumerable<UserDto>))]
        public IHttpActionResult GetOnLineUser()
        {
            IEnumerable<UserDto> list = userBO.GetOnLineUser();
            if(list==null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // PUT api/User/5
        [HttpPut]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult Update([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userBO.UpdateUser(user);

            return Ok(user);
        }

        // PUT api/User/5
        [HttpPut]
        [Route("api/User/OnLine")]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult UserOnLine([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userBO.UserOnLine(user);

            return Ok(user);
        }

        // POST api/User
        [HttpPost]
        public HttpResponseMessage Add([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, user);
            }

            UserDto user2 = userBO.AddUser(user);

            //return Ok(user2);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, user2);
            return response;

        }

        // DELETE api/User/5
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult DeleteUser(int id)
        {
            //User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //db.Users.Remove(user);
            //db.SaveChanges();

            return NotFound();
        }

        [HttpGet]
        [Route("api/User/UserOnLine/{userid}")]
        public bool UserIsOnline(int userid)
        {
            return userBO.UserIsOnLine(userid);
        }

        
    }
}