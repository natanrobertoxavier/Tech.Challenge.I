namespace Tech.Challenge.I.Domain.Repositories.Contact;
public interface IContactReadOnlyRepository
{
    Task<bool> ThereIsRegisteredContact(Guid dddId, string phoneNumber);
}
