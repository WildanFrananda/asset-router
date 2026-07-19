namespace AssetRouter.Infrastructure.Repositories;

using System.Linq;
using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;
using AssetRouter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class AllocationRepository(AppDbContext dbContext) : IAllocationRepository {
    public async Task SaveAllocationAsync(AssetAllocation allocation) {
        dbContext.Allocations.Add(allocation);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AssetAllocation>> GetAllHistoryAsync() {
        return await dbContext.Allocations.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }
}
