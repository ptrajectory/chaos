


namespace chaos.Models
{

    public class Participant {
        public string ID {get; set;}

        public string UserID {get; set;}

        public string ChannelID { get; set;}

        public DateTime CreatedAt = DateTime.Now;

        public Participant(string PID, string UsrID, string ChID){
            this.ID = PID;
            this.UserID = UsrID;
            this.ChannelID = ChID;
        }
    }
    
}