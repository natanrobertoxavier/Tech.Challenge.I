using Microsoft.EntityFrameworkCore;
using Tech.Challenge.I.Domain.Repositories.DDD;

namespace Tech.Challenge.I.Infrastructure.RepositoryAccess.Repository;
public class RegionDDDRepository(
    TechChallengeContext context) : IRegionDDDReadOnlyRepository, IRegionDDDWriteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public async Task<bool> ThereIsDDDNumber(int ddd) => 
        await _context.DDDRegions.AnyAsync(c => c.DDD.Equals(ddd));
}
