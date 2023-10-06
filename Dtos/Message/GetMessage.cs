using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chaos.Dtos.Channel;
using chaos.Dtos.Media;
using chaos.Dtos.User;

namespace chaos.Dtos.Message
{
    public class GetMessage
    {

        public string ID {get; set;} = String.Empty; 
        public string SenderID {get; set;} = String.Empty;

        public DateTime CreatedAt {get; set;}

        public string ChannelID {get; set;} = String.Empty;
        public string? TextContent {get;set;}

        public GetUser? Sender {get;set;}

        public GetChannel? Channel {get; set;}
        
        public List<GetMessageMedia> MessageMedia{get;set;} = new List<GetMessageMedia>();
    }
}