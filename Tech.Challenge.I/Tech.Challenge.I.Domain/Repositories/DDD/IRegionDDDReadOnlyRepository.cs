namespace Tech.Challenge.I.Domain.Repositories.DDD;
public interface IRegionDDDReadOnlyRepository
{
    Task<bool> ThereIsDDDNumber(int ddd);
}
