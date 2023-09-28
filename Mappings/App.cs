

using AutoMapper;
using chaos.Dtos.App;
using chaos.Dtos.User;

namespace chaos.Mappings;

public class AppMappings: Profile {

    public AppMappings(){
        CreateMap<CreateApp, Models.Apps>();
        CreateMap<UpdateApp, Models.Apps>()
        .ForAllMembers((opts)=> opts.Condition((src, dest, srcMember)=> srcMember is not null));
    }

}