using AutoMapper;
using Tech.Challenge.I.Communication;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Communication.Response.Enum;

namespace Tech.Challenge.I.Application.Services.Automapper;
public class TechChallangeProfile : Profile
{
    public TechChallangeProfile()
    {
        RequestToEntity();
        EntityToRequest();
    }

    private void EntityToRequest()
    {
        CreateMap<Domain.Entities.RegionDDD, Communication.Response.RegionDDDResponseJson>()
            .ForMember(destiny => destiny.Region, config => config.MapFrom(origin => EnumExtensions.GetEnumValueFromDescription<RegionResponseEnum>(origin.Region)));
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entities.User>()
           .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Request.RequestRegionDDDJson, Domain.Entities.RegionDDD>()
            .ForMember(destiny => destiny.Region, config => config.MapFrom(origin => origin.Region.GetDescription()));
    }
}
