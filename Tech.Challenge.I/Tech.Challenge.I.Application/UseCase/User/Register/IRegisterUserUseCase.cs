using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Application.UseCase.User.Register;
public interface IRegisterUserUseCase
{
    Task<RegisteredUserResponseJson> Execute(RequestRegisterUserJson request);
}
