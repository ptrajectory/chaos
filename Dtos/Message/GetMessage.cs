using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Dtos.Message
{
    public class GetMessage
    {

        public string ID {get; set;} = String.Empty; 
        public string SenderID {get; set;} = String.Empty;

        public DateTime CreatedAt {get; set;}

        public string ChannelID {get; set;} = String.Empty;
        public string? TextContent {get;set;}
        
    }
}