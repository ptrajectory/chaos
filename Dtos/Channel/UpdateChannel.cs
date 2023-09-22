

namespace chaos.Dtos.Channel {
    public class UpdateChannel {

        public DateTime CreatedAt {get; set;}

        public string Name {get; set;} = String.Empty;

        public string Description {get; set;} = String.Empty;

        public string Icon {get; set;} = String.Empty;

        public string Banner {get; set;} = String.Empty;

    }
}