namespace Tech.Challenge.I.Domain.Repositories.DDD;
public interface IRegionDDDReadOnlyRepository
{
    Task<bool> ThereIsDDDNumber(int ddd);
    Task<IEnumerable<Entities.RegionDDD>> RecoverAll();
    Task<IEnumerable<Entities.RegionDDD>> RecoverListDDDByRegion(string region);
    Task<IEnumerable<Entities.RegionDDD>> RecoverListByDDD(int dDD);
    Task<Entities.RegionDDD> RecoverByDDD(int dDD);
    Task<Entities.RegionDDD> RecoverById(Guid id);
}
