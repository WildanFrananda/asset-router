namespace AssetRouter.Infrastructure.Repositories;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;
using AssetRouter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class RuleRepository(AppDbContext dbContext) : IRuleRepository {
    public Task<List<AllocationRule>> GetRulesAsync() {
        return dbContext.AllocationRules
            .AsNoTracking()
            .OrderBy(r => r.SortOrder)
            .ToListAsync();
    }

    public async Task ReplaceRulesAsync(IEnumerable<AllocationRule> rules) {
        var snapshot = rules.Select(r => new AllocationRule {
            BucketName = r.BucketName,
            Percentage = r.Percentage,
            SortOrder = r.SortOrder
        }).ToList();

        await dbContext.AllocationRules.ExecuteDeleteAsync();
        dbContext.AllocationRules.AddRange(snapshot);
        await dbContext.SaveChangesAsync();
    }
}
