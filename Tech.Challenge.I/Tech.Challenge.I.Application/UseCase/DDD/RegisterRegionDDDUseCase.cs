using AutoMapper;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Domain.Repositories;
using Tech.Challenge.I.Domain.Repositories.DDD;

namespace Tech.Challenge.I.Application.UseCase.DDD;
public class RegisterRegionDDDUseCase(
    IRegionDDDReadOnlyRepository userReadOnlyRepository,
    IRegionDDDWriteOnlyRepository userWriteOnlyRepository,
    IMapper mapper,
    IWorkUnit workUnit)  : IRegisterRegionDDDUseCase
{
    private readonly IRegionDDDReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IRegionDDDWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWorkUnit _workUnit = workUnit;

    public async Task<IEnumerable<RegionDDDResponseJson>> Execute(RequestRegistrationRegionDDDJson request)
    {
        await Validate(request);

        throw new NotImplementedException();
    }

    private async Task Validate(RequestRegistrationRegionDDDJson request)
    {
        var thereIsUserWithEmail = await _userReadOnlyRepository.ThereIsDDDNumber(request.DDD);
    }
}
