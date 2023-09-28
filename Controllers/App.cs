
using AutoMapper;
using chaos.Dtos.App;
using chaos.Services;
using Microsoft.AspNetCore.Mvc;
using Supabase.Core.Extensions;

namespace chaos.Controllers; 


[ApiController]
[Route("api/{organization_id}/apps")]
public class AppController: ControllerBase
{

    private readonly IApps _app;


    public AppController(IApps app){
        this._app = app;
    }



    [HttpPost]
    public ActionResult<string> createApp(string organization_id, CreateApp app){
        app.OrgID = organization_id;
        var new_app_id = this._app.createApp(app);

        return Ok(new_app_id);

    }

    [HttpGet("{id}")]
    public ActionResult<GetApp> getApp(string organization_id, string id){

        var app = this._app.getApp(id);

        return Ok(app);
    }


    [HttpPatch("{id}")]
    public ActionResult<GetApp> updateApp(string organization_id, string id, UpdateApp UpdatedData){

        var app = this._app.updateApp(id, UpdatedData);

        return Ok(app);

    }

}