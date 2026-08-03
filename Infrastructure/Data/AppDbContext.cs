namespace AssetRouter.Infrastructure.Data;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options, IDbPersistence persistence) : DbContext(options) {
    public DbSet<AllocationSnapshot> Snapshots { get; set; }
    public DbSet<AllocationRule> AllocationRules { get; set; }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await persistence.PersistAsync();

        return result;
    }
}
