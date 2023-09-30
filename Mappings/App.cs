

using AutoMapper;
using chaos.Dtos.App;
using chaos.Dtos.User;

namespace chaos.Mappings;

public class AppMappings: Profile {

    public AppMappings(){
        CreateMap<CreateApp, Models.Apps>();
        CreateMap<UpdateApp, Models.Apps>()
        .ForAllMembers((opts)=> opts.Condition((src, dest, srcMember)=> srcMember is not null));
        CreateMap<Models.Apps, GetAppCredentials>()
        .ForMember(dest => dest.CLIENT_SECRET, opt => opt.MapFrom(src => src.CLIENT_SECRET))
        .ForMember(dest => dest.TEST_CLIENT_SECRET, opt => opt.MapFrom(src => src.TEST_CLIENT_SECRET));
    }

}