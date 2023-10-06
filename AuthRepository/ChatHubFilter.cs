

using Microsoft.AspNetCore.SignalR;

namespace chaos.AuthRepository;


public class ChatHubFilter: IHubFilter {
    public ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next){
        if(invocationContext.Context.User is null) throw new HubException("invalid_auth");

        var expires_in = invocationContext.Context.User.Claims.FirstOrDefault((x) => x.Type == "expires")?.Value ?? throw new HubException("invalid_auth");

        var expiry_date = DateTime.Parse(expires_in);

        if(DateTime.UtcNow.Subtract(expiry_date) > TimeSpan.Zero){
            throw new HubException("auth_expired");
        }
        
        return next(invocationContext);
    }
}