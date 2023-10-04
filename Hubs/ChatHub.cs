using Microsoft.AspNetCore.SignalR;

namespace chaos.Hubs;


public class ChatHub: Hub {

    public async Task<string> JoinChannel(){
        Console.WriteLine("Joining Channel");
        await Groups.AddToGroupAsync(this.Context.ConnectionId, "chat room");

        return "welcome to the channel";
    }


    public Task LeaveChannel(string chn){
        return Groups.RemoveFromGroupAsync(this.Context.ConnectionId, chn);
    }

    public async Task SendMessage()
    {
        Console.WriteLine("Message");
        await this.Clients.All
        .SendAsync("send_message", "message incoming");
    }

}