
namespace chaos.Dtos.App;


public class GetApp {
    public string? ID {get; set;}

    public string? Name {get; set;}

    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    public string? OrgID;
}