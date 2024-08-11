﻿using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tech.Challenge.I.Application.UseCase.Contact.Recover;
using Tech.Challenge.I.Communication;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Domain.Repositories.Contact;
using Tech.Challenge.I.Domain.Repositories.DDD;
using Tech.Challenge.I.Domain.Repositories.Factories;


namespace Tech.Challange.I.Tests.UseCase.Contact.Recover;
public class RecoverContactUseCaseTests
{
    private readonly Mock<IContactReadOnlyRepository> _mockContactReadOnlyRepository;
    private readonly Mock<IRegionDDDReadOnlyRepositoryFactory> _mockRepositoryFactory;
    private readonly Mock<IRegionDDDReadOnlyRepository> _mockRegionRepository;
    private readonly Mock<IServiceScope> _mockScope;
    private readonly RecoverContactUseCase _useCase;

    public RecoverContactUseCaseTests()
    {
        _mockContactReadOnlyRepository = new Mock<IContactReadOnlyRepository>();
        _mockRepositoryFactory = new Mock<IRegionDDDReadOnlyRepositoryFactory>();
        _mockRegionRepository = new Mock<IRegionDDDReadOnlyRepository>();
        _mockScope = new Mock<IServiceScope>();

        _mockRepositoryFactory.Setup(f => f.Create())
                              .Returns((_mockRegionRepository.Object, _mockScope.Object));

        _useCase = new RecoverContactUseCase(
            _mockContactReadOnlyRepository.Object,
            _mockRepositoryFactory.Object);
    }
    [Fact]
    public async Task Execute_ReturnsAllContacts_WhenContactsExist()
    {
        // Arrange
        var regionDDDId = Guid.NewGuid();

        var contacts = new List<Challenge.I.Domain.Entities.Contact>
        {
            new() { DDDId = regionDDDId }
        };

        var regionDDD = new Challenge.I.Domain.Entities.RegionDDD { DDD = 11, Region = "Sudeste" };

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverAll())
            .ReturnsAsync(contacts);

        _mockRegionRepository
            .Setup(repo => repo.RecoverById(regionDDDId))
            .ReturnsAsync(regionDDD);

        // Act
        var result = await _useCase.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAll(), Times.Once);
        _mockRegionRepository.Verify(repo => repo.RecoverById(regionDDDId), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsEmptyList_WhenNoContactsExist()
    {
        // Arrange
        var contacts = new List<Challenge.I.Domain.Entities.Contact>();

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverAll())
            .ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAll(), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsContacts_WhenRegionIsValid()
    {
        // Arrange
        var regionDDDId = new List<Guid> { Guid.NewGuid() };

        var regionDescription = RegionRequestEnum.Sudeste.GetDescription();

        var contacts = new List<Challenge.I.Domain.Entities.Contact>
        {
            new() { DDDId = regionDDDId[0] }
        };

        var regionDDD = new List<Challenge.I.Domain.Entities.RegionDDD>
        { 
            new() { Id = regionDDDId[0], DDD = 11, Region = "Sudeste" }
        };

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverAllByDDDId(regionDDDId))
            .ReturnsAsync(contacts);

        _mockRegionRepository
            .Setup(repo => repo.RecoverListDDDByRegion(regionDescription))
            .ReturnsAsync(regionDDD);

        _mockRegionRepository
            .Setup(repo => repo.RecoverById(regionDDDId[0]))
            .ReturnsAsync(regionDDD[0]);

        // Act
        var result = await _useCase.Execute(RegionRequestEnum.Sudeste);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAllByDDDId(regionDDDId), Times.Once);
        _mockRegionRepository.Verify(repo => repo.RecoverById(regionDDDId[0]), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsEmptyList_WhenNoContactsForRegion()
    {
        // Arrange
        var region = RegionRequestEnum.Sudeste;
        var regionDescription = region.GetDescription();

        var dddIds = new List<Guid>();
        var contacts = new List<Challenge.I.Domain.Entities.Contact>();

        _mockRegionRepository
            .Setup(repo => repo.RecoverListDDDByRegion(regionDescription))
            .ReturnsAsync(new List<Challenge.I.Domain.Entities.RegionDDD>());

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverAllByDDDId(dddIds))
            .ReturnsAsync(contacts);

        // Act
        var result = await _useCase.Execute(region);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRegionRepository.Verify(repo => repo.RecoverListDDDByRegion(regionDescription), Times.Once);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverAllByDDDId(dddIds), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsContacts_WhenDDDIsValid()
    {
        // Arrange
        var regionDDDId = Guid.NewGuid();
        var ddd = 11;

        var regionDescription = RegionRequestEnum.Sudeste.GetDescription();

        var contacts = new List<Challenge.I.Domain.Entities.Contact>
        {
            new() { DDDId = regionDDDId }
        };

        var regionDDD = new Challenge.I.Domain.Entities.RegionDDD()
        {
            Id = regionDDDId, DDD = 11, Region = "Sudeste" 
        };

        _mockRegionRepository
            .Setup(repo => repo.RecoverByDDD(ddd))
            .ReturnsAsync(regionDDD);

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverByDDDId(regionDDDId))
            .ReturnsAsync(contacts);

        _mockRegionRepository
            .Setup(repo => repo.RecoverById(regionDDDId))
            .ReturnsAsync(regionDDD);

        // Act
        var result = await _useCase.Execute(ddd);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverByDDDId(regionDDDId), Times.Once);
        _mockRegionRepository.Verify(repo => repo.RecoverByDDD(ddd), Times.Once);
    }

    [Fact]
    public async Task Execute_ReturnsContacts_WhenNoContactsForDDD()
    {
        // Arrange
        var regionDDDId = Guid.NewGuid();
        var ddd = 11;

        var regionDescription = RegionRequestEnum.Sudeste.GetDescription();

        var regionDDD = new Challenge.I.Domain.Entities.RegionDDD()
        {
            Id = regionDDDId, DDD = 11, Region = "Sudeste" 
        };

        _mockRegionRepository
            .Setup(repo => repo.RecoverByDDD(ddd))
            .ReturnsAsync(regionDDD);

        _mockContactReadOnlyRepository
            .Setup(repo => repo.RecoverByDDDId(regionDDDId))
            .ReturnsAsync((List<Challenge.I.Domain.Entities.Contact>) null);

        _mockRegionRepository
            .Setup(repo => repo.RecoverById(regionDDDId))
            .ReturnsAsync(regionDDD);

        // Act
        var result = await _useCase.Execute(ddd);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockContactReadOnlyRepository.Verify(repo => repo.RecoverByDDDId(regionDDDId), Times.Once);
        _mockRegionRepository.Verify(repo => repo.RecoverByDDD(ddd), Times.Once);
    }
}