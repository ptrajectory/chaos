using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


       

        
        
    }
}