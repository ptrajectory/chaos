using chaos.Models;
using System.Threading.Channels;
using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using chaos.Hubs;
using AutoMapper;
using chaos.Dtos.User;
using chaos.Dtos.Channel;
using chaos.Dtos.Message;

namespace chaos.Services;

public class MessageService : BackgroundService, IMessageSink
{

     Mapper GetMessageMapper = new Mapper(new MapperConfiguration((cfg)=>{ 
            cfg.CreateMap<Models.User, GetUser>();
            cfg.CreateMap<Models.Channel, GetChannel>();
            cfg.CreateMap<Models.Message, Dtos.Message.GetMessage>();
    }));
    
    private readonly IServiceProvider _serviceProvider;

    private readonly Channel<Message> _channel;
    public MessageService(
        IServiceProvider serviceProvider
    ){
        this._serviceProvider = serviceProvider;
        this._channel = System.Threading.Channels.Channel.CreateUnbounded<Message>();
    }
    public ValueTask PushAsync(Message message)
    {
        return _channel.Writer.WriteAsync(message);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var message = await _channel.Reader.ReadAsync(stoppingToken);
        using var scope = _serviceProvider.CreateScope();
        var hub = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub>>();

        var dto = GetMessageMapper.Map<GetMessage>(message);

        await hub.Clients.Group(message.ChannelID).SendAsync(
            "ReceieveNewMessage", 
            dto, 
            stoppingToken
        );
    }
}