
namespace chaos.Exceptions.Services;

public class ChaosAppException: Exception {

    public string tag {get;set;} = String.Empty;

    public readonly string? resource;
    public readonly object? incoming;

    public ChaosAppException(string message) : base( )
    {

    }

    public ChaosAppException(string? resource, object? incoming) : base()
    {
        this.resource = resource;
        this.incoming = incoming;
    }

    public ChaosAppException(string message, string? resource, object? incoming): base(message)
    {
        this.resource = resource;
        this.incoming = incoming;
    }

    public ChaosAppException(string message, Exception inner,string? resource, object? incoming): base(message, inner)
    {
        this.resource = resource;
        this.incoming = incoming;
    }



}