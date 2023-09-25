


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chaos.Models
{

    public class Participant {
        [Key]
        public string ID {get; set;} = String.Empty;

        public string UserID {get; set;}= String.Empty;

        [ForeignKey("UserID")]
        public User? User {get; set;}

        public string ChannelID { get; set;}= String.Empty;

        [ForeignKey("ChannelID")]
        public Channel? Channel {get; set;}

        [Column(TypeName ="timestamptz")]
        public DateTime CreatedAt = DateTime.UtcNow;

        public Participant(){}

        public Participant(string PID, string UsrID, string ChID){
            this.ID = PID;
            this.UserID = UsrID;
            this.ChannelID = ChID;
        }
    }
    
}