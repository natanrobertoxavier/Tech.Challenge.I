﻿using AutoMapper;
using Tech.Challenge.I.Communication;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Domain.Repositories.Contact;
using Tech.Challenge.I.Domain.Repositories.Factories;

namespace Tech.Challenge.I.Application.UseCase.Contact.Recover;
public class RecoverContactUseCase(
    IContactReadOnlyRepository contactReadOnlyRepository,
    IRegionDDDReadOnlyRepositoryFactory repositoryFactory,
    IMapper mapper) : IRecoverContactUseCase
{
    private readonly IContactReadOnlyRepository _contactReadOnlyRepository = contactReadOnlyRepository;
    private readonly IRegionDDDReadOnlyRepositoryFactory _repositoryFactory = repositoryFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ResponseContactJson>> Execute()
    {
        var entities = await _contactReadOnlyRepository.RecoverAll();

        return await MapToResponseContactJson(entities);
    }

    public async Task<IEnumerable<ResponseContactJson>> Execute(RegionRequestEnum region)
    {
        var dddIds = await RecoverDDDIdsByRegion(region.GetDescription());

        var entities = await _contactReadOnlyRepository.RecoverAllByDDDId(dddIds);

        return await MapToResponseContactJson(entities);
    }

    public async Task<IEnumerable<ResponseContactJson>> Execute(int ddd)
    {
        var regionIds = await RecoverRegionIdByDDD(ddd);

        var entities = await _contactReadOnlyRepository.RecoverByDDDId(regionIds);

        return await MapToResponseContactJson(entities);
    }

    private async Task<IEnumerable<ResponseContactJson>> MapToResponseContactJson(IEnumerable<Domain.Entities.Contact> entities)
    {
        var tasks = entities.Select(async entity =>
        {
            var (regionReadOnlyrepository, scope) = _repositoryFactory.Create();

            using (scope)
            {
                var ddd = await regionReadOnlyrepository.RecoverById(entity.DDDId);

                return new ResponseContactJson
                {
                    ContactId = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DDD = ddd.DDD,
                    Region = ddd.Region,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber
                };
            }
        });

        var responseContactJson = await Task.WhenAll(tasks);
        return responseContactJson;
    }

    private async Task<IEnumerable<Guid>> RecoverDDDIdsByRegion(string region)
    {
        var (regionReadOnlyRepository, scope) = _repositoryFactory.Create();

        using (scope)
        {
            var ddd = await regionReadOnlyRepository.RecoverListDDDByRegion(region);

            return ddd.Select(ddd => ddd.Id).ToList();
        }
    }

    private async Task<Guid> RecoverRegionIdByDDD(int ddd)
    {
        var (regionReadOnlyRepository, scope) = _repositoryFactory.Create();

        using (scope)
        {
            var regionDDD = await regionReadOnlyRepository.RecoverByDDD(ddd);

            return regionDDD.Id;
        }
    }
}
