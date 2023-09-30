using chaos.Dtos.Auth;

namespace chaos.AuthRepository;

public interface IAuthService {

    public GetAccessToken generateAccessToken(string APP_ID, string Environment);

}