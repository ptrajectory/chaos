
using Newtonsoft.Json;

namespace chaos.Dtos.App;

public class GetAppCredentials {

    [JsonProperty("test_client_secret")]
    public string? TEST_CLIENT_SECRET {get; set;}

    [JsonProperty("client_secret")]
    public string? CLIENT_SECRET {get;set;}

}