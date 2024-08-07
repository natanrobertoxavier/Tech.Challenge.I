using AutoMapper;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Domain.Repositories;
using Tech.Challenge.I.Domain.Repositories.Contact;
using Tech.Challenge.I.Domain.Repositories.DDD;

namespace Tech.Challenge.I.Application.UseCase.Contact.Update;
public class UpdateContactUseCase(
    IContactReadOnlyRepository contactReadOnlyRepository,
    IRegionDDDReadOnlyRepository regionReadOnlyRepository,
    IContactWriteOnlyRepository contactWriteOnlyRepository,
    IWorkUnit workUnit,
    IMapper mapper) : IUpdateContactUseCase
{
    private IContactReadOnlyRepository _contactReadOnlyRepository = contactReadOnlyRepository;
    private IContactWriteOnlyRepository _contactWriteOnlyRepository = contactWriteOnlyRepository;
    private IRegionDDDReadOnlyRepository _regionReadOnlyRepository = regionReadOnlyRepository;
    private IMapper _mapper = mapper;
    private IWorkUnit _workUnit = workUnit;

    public async Task Execute(Guid id, RequestContactJson request)
    {
        var contactToUpdate = _mapper.Map<Domain.Entities.Contact>(request);
        var ddd = await _regionReadOnlyRepository.RecoverByDDD(request.DDD);
        contactToUpdate.Id = id;
        contactToUpdate.DDDId = ddd.Id;

        _contactWriteOnlyRepository.Update(contactToUpdate);
        await _workUnit.Commit();
    }
}
