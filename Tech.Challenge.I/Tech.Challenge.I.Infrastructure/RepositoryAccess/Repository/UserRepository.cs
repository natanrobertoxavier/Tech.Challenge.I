using Microsoft.EntityFrameworkCore;
using Tech.Challenge.I.Domain.Entities;
using Tech.Challenge.I.Domain.Repositories.User;

namespace Tech.Challenge.I.Infrastructure.RepositoryAccess.Repository;

public class UserRepository(
    TechChallengeContext context) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public async Task<bool> ThereIsUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(c => c.Email.Equals(email));
    }

    public Task<User> RecoverByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> RecoverByEmailPassword(string email, string senha)
    {
        throw new NotImplementedException();
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }
}
