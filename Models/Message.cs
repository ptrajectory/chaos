using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace chaos.Models
{
    public class Message
    {
        public string ID {get; set;} = String.Empty;
        public string SenderID {get; set;} = String.Empty;

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public string ChannelID {get; set;}= String.Empty;
        public string? TextContent {get;set;}= String.Empty;

        public Message(){}

        public  Message(string MsgId, string SenderID, string ChannelID){
            this.ID = MsgId;
            this.SenderID = SenderID;
            this.CreatedAt = DateTime.Now;
            this.ChannelID = ChannelID;
        }
    }
}