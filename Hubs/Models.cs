namespace chaos.Hubs; 


public class JoinChannelModel {
    public string? ChannelID{get; set;}
}

public record JoinChannelRecord {
    public string? ChannelID{get; set;}
}


public class UserTypingData {
    public bool Typing {get; set;} = false;
    public string? ChannelID {get; set;}
}


public class TypingResponse {
    public bool Typing {get; set; } = false;

    public string? UserID {get; set;}
}