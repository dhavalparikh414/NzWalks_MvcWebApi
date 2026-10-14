using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace NZWalksApi.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly NzWalksDbContext _nzWalksDbContext;

    public SQLRegionRepository(NzWalksDbContext nzWalksDbContext)
    {
        _nzWalksDbContext = nzWalksDbContext;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _nzWalksDbContext.Regions.AddAsync(region);
        await _nzWalksDbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var region = await GetByIdAsync(id);
        if (region == null)
        {
            return null;
        }

        _nzWalksDbContext.Regions.Remove(region);
        await _nzWalksDbContext.SaveChangesAsync();
        return region;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _nzWalksDbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        return await _nzWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await GetByIdAsync(id);
        if (existingRegion == null)
        {
            return null;
        }

        existingRegion.Name = region.Name;
        existingRegion.Code = region.Code;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await _nzWalksDbContext.SaveChangesAsync();
        return existingRegion;
    }
}
