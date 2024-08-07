namespace Tech.Challenge.I.Domain.Repositories.Contact;
public interface IContactReadOnlyRepository
{
    Task<bool> ThereIsRegisteredContact(Guid dddId, string phoneNumber);
    Task<IEnumerable<Entities.Contact>> RecoverAll();
    Task<IEnumerable<Entities.Contact>> RecoverByDDDId(IEnumerable<Guid> ids);
    Task<Entities.Contact> RecoverByContactId(Guid id);
}
