using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.Message;
using chaos.Dtos.Participant;
using chaos.Dtos.User;
using chaos.Models;
using chaos.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace chaos.Services
{
    public class Channel : IChannel
    {
        private readonly ChatContext context;

        Mapper CreateChannelMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Dtos.Channel.CreateChannel, Models.Channel>())
        );

        Mapper UpdateChannelMapper = new Mapper(new MapperConfiguration((cfg)=>cfg.CreateMap<Dtos.Channel.UpdateChannel, Models.Channel>()
            .ForAllMembers((opts)=> opts.Condition((src, dest, srcMember)=> srcMember != null ))
        ));

        Mapper UpdateMessageMapper = new Mapper(new MapperConfiguration((cfg)=>cfg.CreateMap<Dtos.Message.UpdateMessage, Models.Message>()
            .ForAllMembers((opts)=> opts.Condition((src, dest, srcMember)=> srcMember != null))
        ));

        Mapper GetParticipantMapper = new Mapper(new MapperConfiguration((cfg)=>{
            cfg.CreateMap<Models.User, Dtos.User.GetUser>();
            cfg.CreateMap<Models.Channel, GetChannel>();
            cfg.CreateMap<Models.Participant, Dtos.Participant.GetParticipant>();
        } ) );

        Mapper GetMessageMapper = new Mapper(new MapperConfiguration((cfg)=>{ 
            cfg.CreateMap<Models.User, GetUser>();
            cfg.CreateMap<Models.Channel, GetChannel>();
            cfg.CreateMap<Models.Message, Dtos.Message.GetMessage>();
        }));

        Mapper GetChannelMapper = new Mapper(new MapperConfiguration((cfg)=>{ 
            cfg.CreateMap<Models.User, GetUser>();
            cfg.CreateMap<Models.Channel, Dtos.Channel.GetChannel>();
        }));

        Mapper GetUserMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.User, Dtos.User.GetUser>()));


        public Channel(ChatContext _context){
            this.context = _context;
        }
        public string addMessage(string ChannelID, CreateMessage NewChannelMessage)
        {
            string message_id = Utils.GenerateUniqueID("msg");

            Models.Message msg = new Message(message_id, NewChannelMessage.SenderID, ChannelID);
            msg.TextContent = NewChannelMessage.TextContent;

            this.context.MESSAGE.Add(msg);
            this.context.SaveChanges();

            return message_id;
        }

        public string createChannel(CreateChannel NewChannelData)
        {
            string channel_id = Utils.GenerateUniqueID("chn");
            var channel = CreateChannelMapper.Map<Models.Channel>(NewChannelData);
            channel.ID = channel_id;
            this.context.CHANNEL.Add(channel);
            this.context.SaveChanges();
            return channel_id;
        }

        public GetChannel? getChannel(string ChannelID)
        {
            Models.Channel? channel =  this.context.CHANNEL
            .Include(chn => chn.User)
            .FirstOrDefault(chn => chn.ID == ChannelID);


            var dto = GetChannelMapper.Map<GetChannel>(channel);
            return dto;
        }

        public GetMessage? getMessage(string MessageID)
        {
            Models.Message? msg = this.context.MESSAGE
            .Include(msg => msg.Channel)
            .Include(msg => msg.Sender)
            .FirstOrDefault(msg => msg.ID == MessageID);
            
            
            if(null != msg){
                var dto = GetMessageMapper.Map<GetMessage>(msg);
                return dto;
            }

            return null;
        }

        public List<GetMessage> getMessages(string ChannelID)
        {

            var channel_message = this.context.MESSAGE.
            Where(msg => msg.ChannelID == ChannelID)
            .Include(msg => msg.Sender);

            return GetMessageMapper.Map<List<GetMessage>>(channel_message);
        }

        public GetChannel? updateChannel(string ChannelID, UpdateChannel UpdatedChannelData)
        {  

            Models.Channel? channel = this.context.CHANNEL.Find(ChannelID);

            if(null != channel){
                UpdateChannelMapper.Map(UpdatedChannelData, channel);

                this.context.SaveChanges();

                var dto = GetChannelMapper.Map<GetChannel>(channel);

                return dto;
            }
            
            return null;
        }

        public string addParticipant(string ChannelID, CreateParticipant NewParticipantData)
        {
            string participant_id = Utils.GenerateUniqueID("prt");

            Models.Participant NewParticipant = new Models.Participant(participant_id, NewParticipantData.UserID, ChannelID);

            this.context.PARTICIPANT.Add(NewParticipant);

            this.context.SaveChanges();

            return participant_id;

        }

        public List<GetUser> getChannelParticipants(string ChannelID)
        {
            var channelParticipants = context.PARTICIPANT
            .Where(participant => participant.ChannelID == ChannelID)
            .Select(participant => participant.User)
            .ToList();

            return GetUserMapper.Map<List<GetUser>>(channelParticipants);

            
        }

        public GetParticipant? deleteChannelParticipant(string ParticipantID)
        {
            Participant? participant = this.context.PARTICIPANT.Find(ParticipantID);

            if(null != participant){
                this.context.PARTICIPANT.Remove(participant);

                this.context.SaveChanges();

                var dto = GetParticipantMapper.Map<GetParticipant>(participant);

                return dto;

            }

            return null;
        }

    }
}