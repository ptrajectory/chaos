

using Microsoft.AspNetCore.SignalR;

namespace chaos.AuthRepository;


public class ChatHubIDProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(IdentityData.UserClaimName)?.Value;
    }
}