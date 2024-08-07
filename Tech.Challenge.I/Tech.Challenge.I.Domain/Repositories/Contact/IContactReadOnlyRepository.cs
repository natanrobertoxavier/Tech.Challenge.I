namespace Tech.Challenge.I.Domain.Repositories.Contact;
public interface IContactReadOnlyRepository
{
    Task<bool> ThereIsRegisteredContact(Guid dddId, string phoneNumber);
    Task<IEnumerable<Entities.Contact>> RecoverAll();
    Task<IEnumerable<Entities.Contact>> RecoverByDDDId(Guid id);
    Task<IEnumerable<Entities.Contact>> RecoverAllByDDDId(IEnumerable<Guid> dddIds);
    Task<Entities.Contact> RecoverByContactId(Guid id);
}
