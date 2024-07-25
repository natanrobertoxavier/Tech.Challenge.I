using AutoMapper;

namespace Tech.Challenge.I.Application.Services.Automapper;
public class TechChallangeProfile : Profile
{
    public TechChallangeProfile()
    {
        RequestToEntity();
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entities.User>()
           .ForMember(destiny => destiny.Password, config => config.Ignore());
    }
}
