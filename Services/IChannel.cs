using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chaos.Dtos.Channel;
using chaos.Dtos.Message;
using chaos.Dtos.Participant;
using chaos.Dtos.User;
using chaos.Models;

namespace chaos.Services
{
    public interface IChannel
    {
        public string createChannel(CreateChannel NewChannelData);

        public GetChannel? getChannel(string ChannelID);

        public GetChannel? updateChannel(string ChannelID, UpdateChannel  UpdatedChannelData);

        public Task<string> addMessage(string ChannelID, CreateMessage NewChannelMessage);

        public GetMessage? getMessage(string MessageID);

        public List<GetMessage> getMessages(string ChannelID);

        public string addParticipant(string ChannelID, CreateParticipant NewParticipantData);

        public List<GetUser> getChannelParticipants(string ChannelID);

        public GetParticipant? deleteChannelParticipant(string ParticipantID);


    }
}