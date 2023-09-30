

using AutoMapper;
using chaos.Dtos.App;
using chaos.Exceptions.Services;
using chaos.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace chaos.Services;


public class App : IApps
{
    ChatContext _context;
    IMapper _mapper;

    public App(ChatContext context, IMapper mapper){
        this._context = context;
        this._mapper = mapper; 
    }
        
    public string createApp(CreateApp NewAppData)
    {
        var app = this._mapper.Map<Apps>(NewAppData);

        if(app.ID is not null){
            app.CLIENT_SECRET = utils.Utils.GenerateEncryptedJwt(app.ID, "prod");
            app.TEST_CLIENT_SECRET = utils.Utils.GenerateEncryptedJwt(app.ID, "test");
        }

        this._context.APPS.Add(app);
        
        if(app.ID is not null){
            this._context.SaveChanges();
            return app.ID;
        }
        throw new ChaosAppException(resource: "app", incoming: app);
    }

    public GetApp? getApp(string AppID)
    {
        var app = this._context.APPS.Find(AppID);

        var dto = this._mapper.Map<GetApp>(app);

        return dto;
    }

    public GetApp? updateApp(string AppID, UpdateApp NewAppData)
    {
        var app = this._context.APPS.Find(AppID) ?? throw new ChaosAppException(message: "NOT FOUND", resource: "app", incoming: null);
        this._mapper.Map<UpdateApp, Apps>(NewAppData, app);

        this._context.SaveChanges();

        var dto = this._mapper.Map<GetApp>(app);
        
        return dto;
    }


    public GetAppCredentials? getAppCredentials(string AppID){

        var app = this._context.APPS.Find(AppID);

        var dto = this._mapper.Map<GetAppCredentials>(app);

        Console.WriteLine(dto.CLIENT_SECRET);

        return dto;

    }
}