using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace chaos.Models
{
    public class Channel
    {
        
        public string ID {get; set;} = String.Empty;

        public string CreatorID {get; set;} = String.Empty;


        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public string? Name {get; set;} = String.Empty;

        public string? Description {get; set;} = String.Empty;

        public string? Icon {get; set;} = String.Empty;

        public string? Banner {get; set;} = String.Empty;

        public Channel(){}

        public Channel(string ChannelID, string CreatorID){
            this.ID = ChannelID;
            this.CreatorID = CreatorID;
            this.CreatedAt = DateTime.Now;
        }   
    }
}