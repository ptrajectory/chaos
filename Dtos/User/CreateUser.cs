using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Dtos.User
{
    public class CreateUser
    {

        public string? FirstName{get; set;} = String.Empty;

        public string? LastName{get;set;} = String.Empty;

        public string UserName{get; set;} = String.Empty;

        public string Avatar{get; set;} = String.Empty;

        public string? Bio{get; set;} = String.Empty;
    }
}