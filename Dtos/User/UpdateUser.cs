using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Dtos.User
{
    public class UpdateUser
    {
        public string? FirstName{get; set;}

        public string? LastName{get;set;}

        public string? UserName{get; set;}

        public string? Avatar{get; set;}

        public string? Bio{get; set;}
    }
}