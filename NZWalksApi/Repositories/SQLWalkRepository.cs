using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalksApi.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NzWalksDbContext _dbContext;

    public SQLWalkRepository(NzWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _dbContext.Walks.ToListAsync();
    }
}
