using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chaos.Dtos.Channel;
using chaos.Dtos.User;
using chaos.Services;
using Microsoft.AspNetCore.Mvc;

namespace chaos.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class User: ControllerBase
    {
        
        private readonly IUser user;

        public User(IUser _user) {
            this.user = _user;
        }

        [HttpPost]
        public ActionResult<string> createUser([FromBody] CreateUser NewUserData){
            string id = this.user.createUser(NewUserData);

            return Ok(id);
        }


        [HttpGet("{userId}")]
        public ActionResult<GetUser?> getUser(string userId){

            GetUser? user = this.user.getUser(userId);
            Console.WriteLine($"Here is the user {user}");
            return Ok(user);
        }

        
        [HttpPatch("{userId}")]
        public ActionResult<GetUser?> updateUser(string userId, [FromBody] UpdateUser data) {
            GetUser? updatedUser = this.user.updateUser(userId, data);
            return Ok(updatedUser);
        }


        [HttpGet("channels/{userId}")]
        public ActionResult<GetChannel> getUserChannels(string userId){
            List<GetChannel> channels = this.user.getUserChannels(userId);
            return Ok(channels);
        }
        
        
    }
}