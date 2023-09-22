using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chaos.Dtos.Channel;
using chaos.Dtos.User;

namespace chaos.Services
{
    public interface IUser
    {
        public string createUser(CreateUser NewUserData);

        public GetUser? getUser(string UserID);

        public UpdateUser updateUser(string UserID, UpdateUser UpdatedUserData);

        public List<GetChannel> getUserChannels(string UserID);
    }
}