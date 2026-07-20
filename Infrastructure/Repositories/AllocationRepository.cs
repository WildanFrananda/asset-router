namespace AssetRouter.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;
using AssetRouter.Infrastructure.Data;

public class AllocationRepository(AppDbContext dbContext) : IAllocationRepository {
    public async Task SaveSnapshotAsync(AllocationSnapshot snapshot) {
        dbContext.Snapshots.Add(snapshot);
        await dbContext.SaveChangesAsync();
    }

    public Task<List<AllocationSnapshot>> GetHistoryAsync() {
        return dbContext.Snapshots
            .AsNoTracking()
            .Include(s => s.Items)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }
}
