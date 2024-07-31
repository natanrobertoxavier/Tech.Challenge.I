using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.DDD.Recover;
public interface IRecoverRegionDDDUseCase
{
    Task<IEnumerable<RegionDDDResponseJson>> Execute();
}
