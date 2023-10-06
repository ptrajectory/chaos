using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chaos.Models
{
    public class Message
    {
        [Key]
        public string ID {get; set;} = String.Empty;
        public string SenderID {get; set;} = String.Empty;

        [ForeignKey("SenderID")]
        public User? Sender{get; set;}

        [Column(TypeName ="timestamptz")]
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public string ChannelID {get; set;}= String.Empty;

        [ForeignKey("ChannelID")]
        public Channel? Channel {get; set;}

        public string? TextContent {get;set;}= String.Empty;

        public List<MessageMedia> MessageMedia{get; set;} = new List<MessageMedia>();
        public Message(){}

        public  Message(string MsgId, string SenderID, string ChannelID){
            this.ID = MsgId;
            this.SenderID = SenderID;
            this.CreatedAt = DateTime.UtcNow;
            this.ChannelID = ChannelID;
        }
    }
}