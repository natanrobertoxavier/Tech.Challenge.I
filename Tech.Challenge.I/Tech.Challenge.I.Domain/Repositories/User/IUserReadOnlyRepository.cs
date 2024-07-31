namespace Tech.Challenge.I.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ThereIsUserWithEmail(string email);
    Task<Entities.User> RecoverByEmailPassword(string email, string senha);
    Task<Entities.User> RecoverByEmail(string email);
    Task<Entities.User> RecoverEmailPassword(string email, string encryptedPassword);
}
