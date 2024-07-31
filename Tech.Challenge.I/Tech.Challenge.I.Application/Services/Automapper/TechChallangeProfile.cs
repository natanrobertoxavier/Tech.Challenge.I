using AutoMapper;
using Tech.Challenge.I.Communication;

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
        CreateMap<Domain.Entities.RegionDDD, Communication.Request.RequestRegionDDDJson>()
            .ForMember(destiny => destiny.Region, config => config.Ignore());
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entities.User>()
           .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Request.RequestRegionDDDJson, Domain.Entities.RegionDDD>()
            .ForMember(destiny => destiny.Region, config => config.MapFrom(origin => origin.Region.GetDescription()));
    }
}
