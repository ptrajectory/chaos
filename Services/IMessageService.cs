

using chaos.Models;

namespace chaos.Services;

public interface IMessageSink{
    public ValueTask PushAsync(Message message);
}