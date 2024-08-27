﻿using Tech.Challenge.I.Domain.Repositories;
using Tech.Challenge.I.Domain.Repositories.Contact;

namespace Tech.Challenge.I.Application.UseCase.Contact.Delete;
public class DeleteContactUseCase(
    IContactReadOnlyRepository contactReadOnlyRepository,
    IContactWriteOnlyRepository contactWriteOnlyRepository,
    IWorkUnit workUnit) : IDeleteContactUseCase
{
    private IContactReadOnlyRepository _contactReadOnlyRepository = contactReadOnlyRepository;
    private IContactWriteOnlyRepository _contactWriteOnlyRepository = contactWriteOnlyRepository;
    private IWorkUnit _workUnit = workUnit;

    public async Task<bool> Execute(Guid id)
    {
        var contact = await _contactReadOnlyRepository.RecoverByContactIdAsync(id);

        if (contact is null)
            return false;

        _contactWriteOnlyRepository.Remove(contact);
        await _workUnit.Commit();

        return true;
    }
}
