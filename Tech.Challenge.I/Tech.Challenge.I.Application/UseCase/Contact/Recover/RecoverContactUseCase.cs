using AutoMapper;
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
        var regionIds = await RecoverRegionIdByRegion(region.GetDescription());

        var entities = await _contactReadOnlyRepository.RecoverByDDDId(regionIds);

        return await MapToResponseContactJson(entities);
    }

    private async Task<IEnumerable<ResponseContactJson>> MapToResponseContactJson(IEnumerable<Domain.Entities.Contact> entities)
    {
        try
        {
            var tasks = entities.Select(async entity =>
            {
                var (regionReadOnlyrepository, scope) = _repositoryFactory.Create();

                using (scope)
                {
                    var ddd = await regionReadOnlyrepository.RecoverById(entity.DDDId);

                    return new ResponseContactJson
                    {
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
        catch (Exception e)
        {
            var teste = e.Message;
            throw;
        }
    }

    private async Task<IEnumerable<Guid>> RecoverRegionIdByRegion(string region)
    {
        var (regionReadOnlyRepository, scope) = _repositoryFactory.Create();

        using (scope)
        {
            var ddd = await regionReadOnlyRepository.RecoverByRegion(region);

            return ddd.Select(ddd => ddd.Id).ToList();
        }
    }
}
