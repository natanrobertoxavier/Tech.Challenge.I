
namespace Tech.Challenge.I.Domain.Repositories.Contact;
public interface IContactWriteOnlyRepository
{
    Task Add(Entities.Contact contact);
    void Remove(Entities.Contact contact);
}
