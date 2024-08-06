using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.Contact.Register;
public interface IRegisterContactUseCase
{
    Task Execute(RequestContactJson request);
}
