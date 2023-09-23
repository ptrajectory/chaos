


namespace chaos.Models
{

    public class Participant {
        public string ID {get; set;} = String.Empty;

        public string UserID {get; set;}= String.Empty;

        public string ChannelID { get; set;}= String.Empty;

        public DateTime CreatedAt = DateTime.Now;

        public Participant(){}

        public Participant(string PID, string UsrID, string ChID){
            this.ID = PID;
            this.UserID = UsrID;
            this.ChannelID = ChID;
        }
    }
    
}