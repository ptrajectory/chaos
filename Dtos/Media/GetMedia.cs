

namespace chaos.Dtos.Media;

public class GetMedia {
    public string? ID {get;set;}
    public string Type {get;set;} = "image";
    public DateTime CreatedAt = DateTime.UtcNow;
    public double size = 0; // size in bytes
    public string? OwnerId {get;set;}
    public string? Name {get; set;}
    public string? FileUrl {get; set;}

}