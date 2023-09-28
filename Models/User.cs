using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Models
{
    public class User
    {
        [Key]
        public string ID{get;set;}= String.Empty;

        public string? FirstName{get; set;} = String.Empty;

        public string? LastName{get;set;} = String.Empty;

        public string? UserName{get; set;} = String.Empty;

        public string? Avatar{get; set;} = String.Empty;

        public string? Bio{get; set;} = String.Empty;

        [Column(TypeName = "timestamptz")]
        public DateTime CreatedAt = DateTime.UtcNow;

        public string? AppID {get; set;}

        public string? OrgID {get;set;}

        [ForeignKey("AppID")]
        public Apps? App {get;set;} 

        [ForeignKey("OrgID")]
        public Organization? Organization {get;set;}

        public List<Channel> CreatedChannels{get;set;} = new List<Channel>();

        public List<Message> Messages{get;set;} = new List<Message>();

        public List<Participant> JoinedChannels {get;set;} = new List<Participant>();




        public User(){}

        public User(string UsID){
            this.ID = UsID;
        }
    }
}