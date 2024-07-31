using AutoMapper;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Domain.Repositories.DDD;

namespace Tech.Challenge.I.Application.UseCase.DDD.Recover;
public class RecoverRegionDDDUseCase(
    IRegionDDDReadOnlyRepository regionDDDReadOnlyRepository,
    IMapper mapper) : IRecoverRegionDDDUseCase
{
    private readonly IRegionDDDReadOnlyRepository _regionDDDReadOnlyRepository = regionDDDReadOnlyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<RegionDDDResponseJson>> Execute()
    {
        try
        {
            var result = await _regionDDDReadOnlyRepository.RecoverAll();

            return _mapper.Map<IEnumerable<RegionDDDResponseJson>>(result);
        }
        catch (Exception e)
        {
            var teste = e.Message;
            throw;
        }
    }
}
