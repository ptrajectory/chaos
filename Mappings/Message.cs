

using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.Media;
using chaos.Dtos.Message;
using chaos.Dtos.Participant;
using chaos.Dtos.User;
using chaos.Models;

namespace chaos.Mappings;

public class Message: Profile {

    public Message(){

        CreateMap<Participant, GetParticipant>();

        CreateMap<User, GetUser>();

        CreateMap<Channel, GetChannel>();
        
        CreateMap<MediaUploads, GetMedia>();

        CreateMap<MessageMedia, GetMessageMedia>();

        CreateMap<Message, GetMessage>();

    }

}