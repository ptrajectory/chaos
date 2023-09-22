using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace chaos.Models
{
    public class Message
    {
        public string ID {get;}
        public string SenderID {get; set;}

        public DateTime CreatedAt {get; set;}

        public string ChannelID {get; set;}
        public string? TextContent {get;set;}

        public  Message(string MsgId, string SenderID, string ChannelID){
            this.ID = MsgId;
            this.SenderID = SenderID;
            this.CreatedAt = DateTime.Now;
            this.ChannelID = ChannelID;
        }
    }
}