using System.Security.Authentication;
using AutoMapper;
using chaos.Dtos.Channel;
using chaos.Dtos.Message;
using chaos.Dtos.User;
using chaos.Models;
using chaos.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace chaos.Hubs;

public interface IClientInterface 
{
    Task UserTyping(TypingResponse typingResponse);


    Task ReceieveNewMessage(GetMessage message);


    Task Ping(string message);
}


[Authorize]
public class ChatHub: Hub<IClientInterface> {

    private readonly ChatContext _context;
    private readonly IMapper _mapper;

    Mapper GetMessageMapper = new Mapper(new MapperConfiguration((cfg)=>{ 
            cfg.CreateMap<Models.User, GetUser>();
            cfg.CreateMap<Models.Channel, GetChannel>();
            cfg.CreateMap<Models.Message, Dtos.Message.GetMessage>();
    }));


    public ChatHub (ChatContext context, IMapper mapper) {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<string> JoinChannel(JoinChannelModel data){

        var channel_participation = this._context.PARTICIPANT.FirstOrDefault(participation => participation.ChannelID == data.ChannelID && participation.UserID == Context.UserIdentifier) ?? throw new HubException("User Isn't a member of the channel");

        await Groups.AddToGroupAsync(Context.ConnectionId, channel_participation.ChannelID);

        return "connection_established";
    }


    public Task LeaveChannel(string chn){
        return Groups.RemoveFromGroupAsync(this.Context.ConnectionId, chn);
    }

    public Task TestPing(string chn){
        return Clients.Groups(chn).Ping("Pong");
    }

    public Task SendMessage(CreateMessage message)
    {

        var msg_id = Utils.GenerateUniqueID("msg");

        var msg = _context.MESSAGE.Add(new Message(){
            ChannelID = message.ChannelID,
            SenderID = message.SenderID,
            ID = msg_id,
            TextContent = message.TextContent
        });


        _context.SaveChanges();

        if(msg is null) throw new HubException("unexpected_error");

        foreach(string upload in message.Uploads){
            _context.MESSAGE_MEDIA.Add(new MessageMedia{
                ID = utils.Utils.GenerateUniqueID("msg_media"),
                MediaID = upload,
                MessageID = msg_id, 
            
            });
        }

        var NewMessage = _context.MESSAGE
        .Include(m => m.Sender)
        // .Include(m => m.MessageMedia)
        .FirstOrDefault(m => m.ID == msg_id);

        if(NewMessage is null) throw new HubException("unexpected_error");

        _context.MESSAGE.Entry(NewMessage)
        .Collection(m=>m.MessageMedia);
        

        var dto = GetMessageMapper.Map<GetMessage>(NewMessage);
        
        return Clients.GroupExcept(message.ChannelID, new List<string>{
            Context.ConnectionId
        }).ReceieveNewMessage(dto);
    }


    public Task UserTyping(UserTypingData data){

        if(data.ChannelID is null) throw new HubException("No Channel ID Specified");

        return Clients.GroupExcept(data.ChannelID, new List<string> { Context.ConnectionId }).UserTyping(new TypingResponse {
            Typing = data.Typing,
            UserID = Context.UserIdentifier
        });


    }

}