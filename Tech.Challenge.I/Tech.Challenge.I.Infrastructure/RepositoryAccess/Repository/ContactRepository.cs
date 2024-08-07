using Microsoft.EntityFrameworkCore;
using Tech.Challenge.I.Domain.Entities;
using Tech.Challenge.I.Domain.Repositories.Contact;

namespace Tech.Challenge.I.Infrastructure.RepositoryAccess.Repository;
public class ContactRepository(
    TechChallengeContext context) : IContactReadOnlyRepository, IContactWriteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public Task<bool> ThereIsRegisteredContact(Guid dddId, string phoneNumber) =>
        _context.Contacts.AnyAsync(c => c.PhoneNumber.Equals(phoneNumber) && 
                                   c.DDDId.Equals(dddId));

    public async Task Add(Contact contact) =>
        await _context.Contacts.AddAsync(contact);

    public async Task<IEnumerable<Contact>> RecoverAll() =>
        await _context.Contacts.ToListAsync();

    public async Task<IEnumerable<Contact>> RecoverByDDDId(Guid id) =>
        await _context.Contacts.Where(c => c.DDDId.Equals(id)).ToListAsync();

    public async Task<Contact> RecoverByContactId(Guid id) =>
        await _context.Contacts.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();

    public void Remove(Contact contact) =>
        _context.Contacts.Remove(contact);

    public void Update(Contact contact) =>
        _context.Contacts.Update(contact);

    public async Task<IEnumerable<Contact>> RecoverAllByDDDId(IEnumerable<Guid> dddIds) =>
        await _context.Contacts.Where(c => dddIds.Contains(c.DDDId)).ToListAsync();
}
