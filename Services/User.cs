

using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.User;
using chaos.Models;
using chaos.utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace chaos.Services {

    class User : IUser
    {
        ChatContext context;

        Mapper CreateUserMapper = new Mapper(new MapperConfiguration((cfg) => cfg.CreateMap<Models.User, Dtos.User.CreateUser>()));

        Mapper UpdateUserMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Dtos.User.UpdateUser, Models.User>()));

        Mapper GetUserMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.User, Dtos.User.GetUser>()));

        Mapper GetChannelMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.Channel, Dtos.Channel.GetChannel>()));

        public User(ChatContext _context){
            this.context = _context;
        }
        public string createUser(CreateUser NewUserData)
        {
            string user_id = Utils.GenerateUniqueID("usr");
            var NewUser = CreateUserMapper.Map<Models.User>(NewUserData);
            this.context.USER.Add(NewUser);
            this.context.SaveChanges();

            return user_id;
        }

        public GetUser? getUser(string UserID)
        {
            Models.User? user = this.context.USER.Find(UserID);

            if(null != user){
                var dto = GetUserMapper.Map<GetUser>(user);
                return dto;
            }

            return null;
        }

        public List<GetChannel> getUserChannels(string UserID)
        {

            List<GetChannel> channels = new List<GetChannel>();
            var participating_in = this.context.PARTICIPANT.Where((participant)=> participant.UserID == UserID);
            var user_channels = this.context.CHANNEL.Where((channel)=>participating_in.Any(participation => participation.ChannelID == channel.ID)); 

            foreach(var channel in user_channels){

                var dto = GetChannelMapper.Map<GetChannel>(channel);

                channels.Add(dto);

            }

            return channels;
        }

        public GetUser? updateUser(string UserID, UpdateUser UpdatedUserData)
        {
            
            var user = this.context.USER.Find(UserID);

            if(user == null) return null;
            UpdateUserMapper.Map(UpdatedUserData, user);
            this.context.SaveChanges();

            var dto = GetUserMapper.Map<GetUser>(user);

            return dto;
        }
    }

}