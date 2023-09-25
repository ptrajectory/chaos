

using chaos.Dtos.Channel;
using chaos.Dtos.User;

namespace chaos.Dtos.Participant {

    public class GetParticipant {
        public string ID {get; set;} = String.Empty;

        public string UserID {get; set;} = String.Empty;

        public string ChannelID { get; set; } = String.Empty;

        public DateTime CreatedAt;


        public GetUser? User {get; set;}

        public GetChannel? Channel {get; set;}
    }

}