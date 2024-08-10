using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tech.Challenge.I.Api.Controllers;
using Tech.Challenge.I.Application.UseCase.DDD.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Exceptions.ExceptionBase;

namespace Tech.Challange.I.Tests.Controllers;
public class RegionDDDControllerTests
{
    [Fact]
    public async Task RegisterDDD_ReturnsCreatedResult_WhenRequestIsValid()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterRegionDDDUseCase>();

        var request = new RequestRegionDDDJson
        {
            DDD = 11,
            Region = RegionRequestEnum.Sudeste
        };

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .Returns(Task.CompletedTask);

        var controller = new RegionDDDController();

        // Act
        var result = await controller.RegisterDDD(mockUseCase.Object, request) as CreatedResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal(nameof(controller.RecoverAll), result.Location);
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task RegisterDDD_ReturnsBadRequestResult_WhenRequestIsInvalid()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterRegionDDDUseCase>();

        RequestRegionDDDJson request = null;

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .ThrowsAsync(new ValidationErrorsException(new List<string>
                   {
                       "DDD deve ser entre 10 e 99"
                   }));

        var controller = new RegionDDDController();

        // Act
        var exception = await Assert.ThrowsAsync<ValidationErrorsException>(() =>
            controller.RegisterDDD(mockUseCase.Object, request));

        // Assert
        Assert.Contains("DDD deve ser entre 10 e 99", exception.ErrorMessages);
    }

    [Fact]
    public async Task RegisterDDD_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterRegionDDDUseCase>();

        var request = new RequestRegionDDDJson
        {
            DDD = 11,
            Region = RegionRequestEnum.Sudeste
        };

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .ThrowsAsync(new UnauthorizedAccessException("Usuário sem permissão"));

        var controller = new RegionDDDController();

        // Act
        var result = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            controller.RegisterDDD(mockUseCase.Object, request));

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Usuário sem permissão", result.Message);
    }

}
