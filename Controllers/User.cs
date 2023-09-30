using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chaos.AuthRepository;
using chaos.Dtos.Channel;
using chaos.Dtos.User;
using chaos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chaos.Controllers
{
    [ApiController]
    [Route("api/organizations/{organization_id}/apps/{app_id}/users")]
    [Authorize]
    [RequiresClaim(claimName: IdentityData.OwnerClaimName, param: "app_id", claimValue: null)]
    public class User: ControllerBase
    {
        
        private readonly IUser user;

        public User(IUser _user) {
            this.user = _user;
        }

        [HttpPost]
        public ActionResult<string> createUser(string organization_id, string app_id, [FromBody] CreateUser NewUserData){
            NewUserData.AppID = app_id;
            NewUserData.OrgID = organization_id;
            string id = this.user.createUser(NewUserData);

            return Ok(id);
        }


        [HttpGet("{userId}")]
        public ActionResult<GetUser?> getUser(string organization_id, string app_id, string userId){

            GetUser? user = this.user.getUser(userId);
            Console.WriteLine($"Here is the user {user}");
            return Ok(user);
        }

        
        [HttpPatch("{userId}")]
        public ActionResult<GetUser?> updateUser(string organization_id, string app_id, string userId, [FromBody] UpdateUser data) {
            GetUser? updatedUser = this.user.updateUser(userId, data);
            return Ok(updatedUser);
        }


        [HttpGet("channels/{userId}")]
        public ActionResult<GetChannel> getUserChannels(string organization_id, string app_id, string userId){
            List<GetChannel> channels = this.user.getUserChannels(userId);
            return Ok(channels);
        }
        
        
    }
}