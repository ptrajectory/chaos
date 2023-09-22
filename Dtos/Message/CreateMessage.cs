using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Dtos.Message
{
    public class CreateMessage
    {
        public string SenderID {get; set;} = String.Empty;
        public string ChannelID {get; set;} = String.Empty;
        public string? TextContent {get;set;}
    }
}