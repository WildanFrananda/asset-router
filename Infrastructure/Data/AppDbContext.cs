namespace AssetRouter.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using AssetRouter.Core.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<AssetAllocation> Allocations { get; set; }
    public DbSet<AllocationRule> AllocationRules { get; set; }
}
