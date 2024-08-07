using AutoMapper;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Domain.Repositories.Contact;
using Tech.Challenge.I.Domain.Repositories.DDD;
using Tech.Challenge.I.Domain.Repositories.Factories;

namespace Tech.Challenge.I.Application.UseCase.Contact.Recover;
public class RecoverContactUseCase(
    IContactReadOnlyRepository contactReadOnlyRepository,
    IRegionDDDReadOnlyRepository regionDDDReadOnlyRepository,
    IRegionDDDReadOnlyRepositoryFactory repositoryFactory,
    IMapper mapper) : IRecoverContactUseCase
{
    private readonly IContactReadOnlyRepository _contactReadOnlyRepository = contactReadOnlyRepository;
    private readonly IRegionDDDReadOnlyRepository _regionDDDReadOnlyRepository = regionDDDReadOnlyRepository;
    private readonly IRegionDDDReadOnlyRepositoryFactory _repositoryFactory = repositoryFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ResponseContactJson>> Execute()
    {
        var entities = await _contactReadOnlyRepository.RecoverAll();

        return await MapToResponseContactJson(entities);
    }

    private async Task<IEnumerable<ResponseContactJson>> MapToResponseContactJson(IEnumerable<Domain.Entities.Contact> entities)
    {
        try
        {
            var tasks = entities.Select(async entity =>
            {
                var (repository, scope) = _repositoryFactory.Create();
                try
                {
                    var ddd = await repository.RecoverById(entity.DDDId);

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
                finally
                {
                    scope.Dispose();
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
}
