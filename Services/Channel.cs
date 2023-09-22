using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.Message;
using chaos.Dtos.Participant;
using chaos.Models;
using chaos.utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace chaos.Services
{
    public class Channel : IChannel
    {
        ChatContext context;

        Mapper UpdateChannelMapper = new Mapper(new MapperConfiguration((cfg)=>cfg.CreateMap<Dtos.Channel.UpdateChannel, Models.Channel>()));

        Mapper UpdateMessageMapper = new Mapper(new MapperConfiguration((cfg)=>cfg.CreateMap<Dtos.Message.UpdateMessage, Models.Message>()));

        Mapper GetParticipantMapper = new Mapper(new MapperConfiguration((cfg)=>cfg.CreateMap<Models.Participant, Dtos.Participant.GetParticipant>()) );

        Mapper GetMessageMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.Message, Dtos.Message.GetMessage>()));

        Mapper GetChannelMapper = new Mapper(new MapperConfiguration((cfg)=> cfg.CreateMap<Models.Channel, Dtos.Channel.GetChannel>()));


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
            this.context.CHANNEL.Add(new Models.Channel(channel_id,NewChannelData.CreatorID));
            this.context.SaveChanges();
            return channel_id;
        }

        public GetChannel? getChannel(string ChannelID)
        {
            Models.Channel? channel =  this.context.CHANNEL.Find(ChannelID);
            Dtos.Channel.GetChannel dto = new Dtos.Channel.GetChannel();
            GetChannelMapper.Map(channel, dto);
            return dto;
        }

        public GetMessage getMessage(string MessageID)
        {
            Dtos.Message.GetMessage dto = new Dtos.Message.GetMessage();
            Models.Message? msg = this.context.MESSAGE.Find(MessageID);
            GetMessageMapper.Map(msg, dto);
            return dto;
        }

        public List<GetMessage> getMessages(string ChannelID)
        {
            List<GetMessage> messages = new List<GetMessage>();
            var channel_messages = this.context.MESSAGE.Where((message)=>message.ChannelID == ChannelID);

            foreach(var message in channel_messages){

                GetMessage dto = new GetMessage();
                GetMessageMapper.Map(message, dto);
                messages.Append(dto);

            }

            return messages;
        }

        public GetChannel? updateChannel(string ChannelID, UpdateChannel UpdatedChannelData)
        {   
            GetChannel dto = new GetChannel();

            Models.Channel? channel = this.context.CHANNEL.Find(ChannelID);

            if(null != channel){
                UpdateChannelMapper.Map(UpdatedChannelData, channel);

                this.context.SaveChanges();

                GetChannelMapper.Map(channel, dto);

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

        public List<GetParticipant> getChannelParticipants(string ChannelID)
        {
            List<Dtos.Participant.GetParticipant> participants = new List<Dtos.Participant.GetParticipant>();
            foreach (var participant in this.context.PARTICIPANT.Where((participant)=>participant.ChannelID == ChannelID)) {
                Dtos.Participant.GetParticipant dto = new Dtos.Participant.GetParticipant();
                GetParticipantMapper.Map(participant, dto);
                participants.Append(dto);
            }

            return participants;
        }

        public void deleteChannelParticipant(string ParticipantID)
        {
            Participant? participant = this.context.PARTICIPANT.Find(ParticipantID);

            if(null != participant){

                this.context.PARTICIPANT.Remove(participant);

                this.context.SaveChanges();

            }
        }

    }
}