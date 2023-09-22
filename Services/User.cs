

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

        Mapper CreateUserMapper = new Mapper(new MapperConfiguration((cfg) => cfg.CreateMap<Dtos.User.CreateUser, Models.User>()));

        Mapper UpdateUserMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Dtos.User.UpdateUser, Models.User>()));

        Mapper GetUserMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Dtos.User.GetUser, Models.User>()));

        Mapper GetChannelMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Dtos.Channel.GetChannel, Models.Channel>()));

        public User(ChatContext _context){
            this.context = _context;
        }
        public string createUser(CreateUser NewUserData)
        {
            string user_id = Utils.GenerateUniqueID("usr");

            Models.User NewUser = new Models.User(user_id);
            CreateUserMapper.Map(NewUserData,NewUser);
            this.context.USER.Add(NewUser);
            this.context.SaveChanges();

            return user_id;
        }

        public GetUser? getUser(string UserID)
        {
            Models.User? user = this.context.USER.Find(UserID);
            Dtos.User.GetUser? GetUserDTO = null;
            GetUserMapper.Map(user, GetUserDTO);

            return GetUserDTO;
        }

        public List<GetChannel> getUserChannels(string UserID)
        {

            List<GetChannel> channels = new List<GetChannel>();
            var participating_in = this.context.PARTICIPANT.Where((participant)=> participant.UserID == UserID);
            var user_channels = this.context.CHANNEL.Where((channel)=>participating_in.Any(participation => participation.ChannelID == channel.ID)); 

            foreach(var channel in user_channels){
                GetChannel dto = new GetChannel();

                GetChannelMapper.Map(channel, dto);

                channels.Add(dto);

            }

            return channels;
        }

        public UpdateUser updateUser(string UserID, UpdateUser UpdatedUserData)
        {
            
            var user = this.context.USER.Find(UserID);
            UpdateUserMapper.Map(UpdatedUserData, user);
            this.context.SaveChanges();
            UpdateUser dto = new UpdateUser();

            GetUserMapper.Map(user, dto);

            return dto;
        }
    }

}