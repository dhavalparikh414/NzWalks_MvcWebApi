using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllAsync();
}
