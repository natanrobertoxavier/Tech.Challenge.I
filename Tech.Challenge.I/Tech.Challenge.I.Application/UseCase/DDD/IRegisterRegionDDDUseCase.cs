using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.DDD;
public interface IRegisterRegionDDDUseCase
{
    Task<IEnumerable<RegionDDDResponseJson>> Execute(RequestRegistrationRegionDDDJson request);
}
