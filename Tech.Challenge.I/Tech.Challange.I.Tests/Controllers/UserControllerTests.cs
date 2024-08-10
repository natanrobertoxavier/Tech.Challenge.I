using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tech.Challenge.I.Api.Controllers;
using Tech.Challenge.I.Application.UseCase.User.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challange.I.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task RegisterUser_ReturnsCreatedResult_WithValidRequest()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterUserUseCase>();

        var request = new RequestRegisterUserJson
        {
            // Inicialize as propriedades necessárias para o teste
        };

        var response = new ResponseRegisteredUserJson
        {
            // Inicialize as propriedades necessárias para o teste
        };

        mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<RequestRegisterUserJson>()))
                   .ReturnsAsync(response);

        var controller = new UserController();

        // Act
        var result = await controller.RegisterUser(mockUseCase.Object, request) as CreatedResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal(nameof(LoginController.Login), result.Location);
        Assert.Equal(response, result.Value);
    }
}
