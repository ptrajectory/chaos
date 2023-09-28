
using chaos.Dtos.App;

namespace chaos.Services;


public interface IApps {


    public string createApp (CreateApp NewAppData);

    public GetApp? getApp(string AppID);

    public GetApp? updateApp(string AppID, UpdateApp NewAppData);

}

