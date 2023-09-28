using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq; 
using System.Threading.Tasks;

namespace chaos.Models
{
    public class Channel
    {
        [Key]
        public string ID {get; set;} = String.Empty;

        public string CreatorID {get; set;} = String.Empty;

        [ForeignKey("CreatorID")]
        public User? User {get; set;}

        [Column(TypeName ="timestamptz")]
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public string? Name {get; set;} = String.Empty;

        public string? Description {get; set;} = String.Empty;

        public string? Icon {get; set;} = String.Empty;

        public string? Banner {get; set;} = String.Empty;

        public string? AppID {get;set;}

        [ForeignKey("AppID")]
        public Apps? App {get;set;}

        public string? OrgID {get;set;}

        [ForeignKey("OrgID")]
        public Organization? Organization {get;set;}

        public Channel(){}

        public Channel(string ChannelID, string CreatorID){
            this.ID = ChannelID;
            this.CreatorID = CreatorID;
            this.CreatedAt = DateTime.UtcNow;
        }   
    }
}