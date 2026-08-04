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

    public async Task<List<AllocationSnapshot>> GetHistoryAsync() {
        var list = await dbContext.Snapshots
            .AsNoTracking()
            .Include(s => s.Items)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        if (list.Count == 0) {
            list = GetInitialPresetSnapshot();
            dbContext.Snapshots.AddRange(list);
            await dbContext.SaveChangesAsync();
        }

        return list;
    }

    private static List<AllocationSnapshot> GetInitialPresetSnapshot() {
        var now = DateTime.UtcNow;

        return new List<AllocationSnapshot> {
            new AllocationSnapshot {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddMonths(-2),
                Salary = 14000000m,
                Items = new List<AssetAllocation> {
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Emergency", Percentage = 20m, Amount = 2800000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Expenses", Percentage = 40m, Amount = 5600000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Stocks", Percentage = 20m, Amount = 2800000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Gold", Percentage = 10m, Amount = 1400000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Crypto", Percentage = 10m, Amount = 1400000m }
                }
            },
            new AllocationSnapshot {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddMonths(-1),
                Salary = 15000000m,
                Items = new List<AssetAllocation> {
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Emergency", Percentage = 20m, Amount = 3000000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Expenses", Percentage = 35m, Amount = 5250000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Stocks", Percentage = 20m, Amount = 3000000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Gold", Percentage = 15m, Amount = 2250000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Crypto", Percentage = 10m, Amount = 1500000m }
                }
            },
            new AllocationSnapshot {
                Id = Guid.NewGuid(),
                CreatedAt = now,
                Salary = 15000000m,
                Items = new List<AssetAllocation> {
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Emergency", Percentage = 20m, Amount = 3000000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Expenses", Percentage = 35m, Amount = 5250000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Stocks", Percentage = 20m, Amount = 3000000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Gold", Percentage = 15m, Amount = 2250000m },
                    new AssetAllocation { Id = Guid.NewGuid(), Category = "Crypto", Percentage = 10m, Amount = 1500000m }
                }
            }
        };
    }
}
