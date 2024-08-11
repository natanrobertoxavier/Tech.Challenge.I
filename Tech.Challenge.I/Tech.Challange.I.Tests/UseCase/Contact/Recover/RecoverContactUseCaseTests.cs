using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tech.Challenge.I.Application.UseCase.Contact.Recover;
using Tech.Challenge.I.Communication;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Domain.Entities;
using Tech.Challenge.I.Domain.Repositories.Contact;
using Tech.Challenge.I.Domain.Repositories.DDD;
using Tech.Challenge.I.Domain.Repositories.Factories;


namespace Tech.Challange.I.Tests.UseCase.Contact.Recover;
public class RecoverContactUseCaseTests
{
    private readonly Mock<IContactReadOnlyRepository> _mockContactReadOnlyRepository;
    private readonly Mock<IRegionDDDReadOnlyRepositoryFactory> _mockRepositoryFactory;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IRegionDDDReadOnlyRepository> _mockRegionRepository;
    private readonly Mock<IServiceScope> _mockScope;
    private readonly RecoverContactUseCase _useCase;

    public RecoverContactUseCaseTests()
    {
        _mockContactReadOnlyRepository = new Mock<IContactReadOnlyRepository>();
        _mockRepositoryFactory = new Mock<IRegionDDDReadOnlyRepositoryFactory>();
        _mockMapper = new Mock<IMapper>();
        _mockRegionRepository = new Mock<IRegionDDDReadOnlyRepository>();
        _mockScope = new Mock<IServiceScope>();

        _mockRepositoryFactory.Setup(f => f.Create())
                              .Returns((_mockRegionRepository.Object, _mockScope.Object));

        _useCase = new RecoverContactUseCase(
            _mockContactReadOnlyRepository.Object,
            _mockRepositoryFactory.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task Execute_ReturnsAllContacts_WhenContactsExist()
    {
        // Arrange
        var contacts = new List<Challenge.I.Domain.Entities.Contact> { new Challenge.I.Domain.Entities.Contact() };
        _mockContactReadOnlyRepository.Setup(repo => repo.RecoverAll()).ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAll(), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsEmptyList_WhenNoContactsExist()
    {
        // Arrange
        var contacts = new List<Challenge.I.Domain.Entities.Contact>();
        _mockContactReadOnlyRepository.Setup(repo => repo.RecoverAll()).ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAll(), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsContactsForRegion_WhenDDDsExist()
    {
        // Arrange
        var region = RegionRequestEnum.Sudeste;
        var dddIds = new List<Guid> { Guid.NewGuid() };
        var contacts = new List<Challenge.I.Domain.Entities.Contact> { new Challenge.I.Domain.Entities.Contact() };

        _mockRegionRepository.Setup(repo => repo.RecoverListDDDByRegion(RegionRequestEnum.Sudeste.GetDescription())).ReturnsAsync(dddIds);
        _mockContactReadOnlyRepository.Setup(repo => repo.RecoverAllByDDDId(dddIds)).ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute(region);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Execute_ReturnsEmptyList_WhenNoDDDsForRegion()
    {
        // Arrange
        var region = RegionRequestEnum.Southeast;
        var dddIds = new List<Guid>();

        _mockRegionRepository.Setup(repo => repo.RecoverListDDDByRegion(region.GetDescription())).ReturnsAsync(dddIds);

        // Act
        var result = await _useCase.Execute(region);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Execute_ReturnsContactsForDDD_WhenRegionExists()
    {
        // Arrange
        var ddd = 11;
        var regionId = Guid.NewGuid();
        var contacts = new List<Domain.Entities.Contact> { new Domain.Entities.Contact() };

        _mockRegionRepository.Setup(repo => repo.RecoverByDDD(ddd)).ReturnsAsync(new RegionDDD { Id = regionId });
        _mockContactReadOnlyRepository.Setup(repo => repo.RecoverByDDDId(regionId)).ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute(ddd);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Execute_ReturnsEmptyList_WhenNoRegionForDDD()
    {
        // Arrange
        var ddd = 11;

        _mockRegionRepository.Setup(repo => repo.RecoverByDDD(ddd)).ReturnsAsync((RegionDDD)null);

        // Act
        var result = await _useCase.Execute(ddd);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Execute_ThrowsException_WhenRecoverByDDDIdFails()
    {
        // Arrange
        var ddd = 11;
        var regionId = Guid.NewGuid();

        _mockRegionRepository.Setup(repo => repo.RecoverByDDD(ddd)).ReturnsAsync(new RegionDDD { Id = regionId });
        _mockContactReadOnlyRepository.Setup(repo => repo.Reco
