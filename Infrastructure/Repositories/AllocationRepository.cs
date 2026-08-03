namespace AssetRouter.Infrastructure.Repositories;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;
using AssetRouter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
