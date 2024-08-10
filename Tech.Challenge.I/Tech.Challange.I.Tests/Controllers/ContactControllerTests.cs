using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tech.Challenge.I.Api.Controllers;
using Tech.Challenge.I.Application.UseCase.Contact.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Exceptions.ExceptionBase;

namespace Tech.Challange.I.Tests.Controllers;
public class ContactControllerTests
{
    [Fact]
    public async Task RegisterContact_ReturnsCreatedResult_WhenRequestIsValid()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterContactUseCase>();

        var request = new RequestContactJson
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DDD = 11,
            PhoneNumber = "99999-9999"
        };

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .Returns(Task.CompletedTask);

        var controller = new ContactController();

        // Act
        var result = await controller.RegisterContact(mockUseCase.Object, request) as CreatedResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal(nameof(ContactController.RecoverAllContacts), result.Location);
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task RegisterContact_ReturnsBadRequestResult_WhenRequestIsInvalid()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterContactUseCase>();

        var request = new RequestContactJson
        {
            FirstName = string.Empty,
            LastName = "Doe",
            Email = string.Empty,
            DDD = 11,
            PhoneNumber = "99999-9999"
        };

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .ThrowsAsync(new ValidationErrorsException(new List<string>
                   {
                       "Nome em branco",
                       "Email em branco"
                   }));

        var controller = new ContactController();

        // Act
        var exception = await Assert.ThrowsAsync<ValidationErrorsException>(() =>
            controller.RegisterContact(mockUseCase.Object, request));

        // Assert
        Assert.Contains("Nome em branco", exception.ErrorMessages);
        Assert.Contains("Email em branco", exception.ErrorMessages);
    }

    [Fact]
    public async Task RegisterContact_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var mockUseCase = new Mock<IRegisterContactUseCase>();

        var request = new RequestContactJson
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DDD = 11,
            PhoneNumber = "99999-9999"
        };

        mockUseCase.Setup(useCase => useCase.Execute(request))
                   .ThrowsAsync(new UnauthorizedAccessException("Usuário sem permissão"));

        var controller = new ContactController();

        // Act
        var result = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            controller.RegisterContact(mockUseCase.Object, request));

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Usuário sem permissão", result.Message);
    }
}
