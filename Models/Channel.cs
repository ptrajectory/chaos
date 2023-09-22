using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace chaos.Models
{
    public class Channel
    {
        
        public string ID {get; set;} 

        public string CreatorID {get; set;}


        public DateTime CreatedAt {get; set;}

        public string Name {get; set;} = String.Empty;

        public string Description {get; set;} = String.Empty;

        public string Icon {get; set;} = String.Empty;

        public string Banner {get; set;} = String.Empty;

        public Channel(string ChID, string CreatorID){
            this.ID = ChID;
            this.CreatorID = CreatorID;
            this.CreatedAt = DateTime.Now;
        }   
    }
}