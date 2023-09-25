

using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.User;
using chaos.Models;
using chaos.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace chaos.Services {

    class User : IUser
    {
        ChatContext context;

        IMapper mapper;

        Mapper CreateUserMapper = new Mapper(new MapperConfiguration((cfg) => cfg.CreateMap< Dtos.User.CreateUser, Models.User>()));

        Mapper UpdateUserMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Dtos.User.UpdateUser, Models.User>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember)=> srcMember != null))
        ));

        Mapper GetUserMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.User, Dtos.User.GetUser>()));

        Mapper GetChannelMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.Channel, Dtos.Channel.GetChannel>()));

        public User(ChatContext _context, IMapper _mapper){
            this.context = _context;
            this.mapper = _mapper;
        }
        public string createUser(CreateUser NewUserData)
        {
            string user_id = Utils.GenerateUniqueID("usr");
            var NewUser = CreateUserMapper.Map<Models.User>(NewUserData);
            NewUser.ID = user_id;
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
            var userChannels = context.PARTICIPANT
            .Where(participant => participant.UserID == UserID)
            .Select(participant => participant.Channel)
            .ToList();

            var channels = GetChannelMapper.Map<List<GetChannel>>(userChannels);

            return channels;
        }

        public GetUser? updateUser(string UserID, UpdateUser UpdatedUserData)
        {
            
            var user = this.context.USER.Find(UserID);

            UpdateUserMapper.Map(UpdatedUserData, user);
            this.context.SaveChanges();

            var dto = GetUserMapper.Map<GetUser>(user);

            return dto;
        }
    }

}