namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IRuleRepository {
    Task<List<AllocationRule>> GetRulesAsync();
    Task ReplaceRulesAsync(IEnumerable<AllocationRule> rules);
}
