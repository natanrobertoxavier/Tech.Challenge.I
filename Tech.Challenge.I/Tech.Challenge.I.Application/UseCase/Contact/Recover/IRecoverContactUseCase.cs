using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.Contact.Recover;
public interface IRecoverContactUseCase
{
    Task<IEnumerable<ResponseContactJson>> Execute();
    Task<IEnumerable<ResponseContactJson>> Execute(RegionRequestEnum region);
}
