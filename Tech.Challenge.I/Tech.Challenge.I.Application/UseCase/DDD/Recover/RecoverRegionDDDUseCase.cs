﻿using AutoMapper;
using Tech.Challenge.I.Communication;
using Tech.Challenge.I.Communication.Request.Enum;
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
        var result = await _regionDDDReadOnlyRepository.RecoverAll();

        return _mapper.Map<IEnumerable<RegionDDDResponseJson>>(result);
    }

    public async Task<IEnumerable<RegionDDDResponseJson>> Execute(RegionRequestEnum request)
    {
        var result = await _regionDDDReadOnlyRepository.RecoverByRegion(request.GetDescription());

        return _mapper.Map<IEnumerable<RegionDDDResponseJson>>(result);
    }
}
