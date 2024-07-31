using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.DDD.Recover;
public interface IRecoverRegionDDDUseCase
{
    Task<IEnumerable<RegionDDDResponseJson>> Execute();
    Task<IEnumerable<RegionDDDResponseJson>> Execute(RegionRequestEnum request);
}
