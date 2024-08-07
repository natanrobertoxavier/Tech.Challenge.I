using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.Contact.Recover;
public interface IRecoverContactUseCase
{
    Task<IEnumerable<ResponseContactJson>> Execute();
}
