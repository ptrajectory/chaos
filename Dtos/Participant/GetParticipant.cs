

namespace chaos.Dtos.Participant {

    public class GetParticipant {
        public string ID {get; set;} = String.Empty;

        public string UserID {get; set;} = String.Empty;

        public string ChannelID { get; set; } = String.Empty;

        public DateTime CreatedAt;
    }

}