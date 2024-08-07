using Microsoft.EntityFrameworkCore;
using Tech.Challenge.I.Domain.Entities;
using Tech.Challenge.I.Domain.Repositories.DDD;

namespace Tech.Challenge.I.Infrastructure.RepositoryAccess.Repository;
public class RegionDDDRepository(
    TechChallengeContext context) : IRegionDDDReadOnlyRepository, IRegionDDDWriteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public async Task Add(RegionDDD ddd) =>
        await _context.DDDRegions.AddAsync(ddd);

    public async Task<IEnumerable<RegionDDD>> RecoverAll()
        => await _context.DDDRegions.ToListAsync();

    public async Task<RegionDDD> RecoverByDDD(int dDD) =>
        await _context.DDDRegions.Where(c => c.DDD.Equals(dDD)).FirstOrDefaultAsync();

    public async Task<IEnumerable<RegionDDD>> RecoverByRegion(string region) =>
        await _context.DDDRegions.Where(r => r.Region == region).ToListAsync();

    public async Task<bool> ThereIsDDDNumber(int ddd) => 
        await _context.DDDRegions.AnyAsync(c => c.DDD.Equals(ddd));

    public async Task<RegionDDD> RecoverById(Guid id) =>
        await _context.DDDRegions.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
}
