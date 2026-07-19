namespace AssetRouter.Application.Services;

using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;
using AssetRouter.Infrastructure.Strategies;

public class PortfolioManagerService(
    IEnumerable<IAllocationPreset> presets,
    IRuleRepository ruleRepository,
    IAllocationRepository allocationRepository
) {
    public IEnumerable<string> AvailablePresets => presets.Select(s => s.Name);

    public List<AllocationRule> GetPresetRules(string presetName) {
        var preset = presets.FirstOrDefault(p => p.Name == presetName)
            ?? throw new InvalidOperationException($"Preset '{presetName}' not found");

        return preset.GetRules().ToList();
    }

    public async Task<List<AllocationRule>> GetRulesAsync() {
        var rules = await ruleRepository.GetRulesAsync();

        if (rules.Count == 0) {
            rules = GetPresetRules(DefaultPreset.PresetName);
            await ruleRepository.ReplaceRulesAsync(rules);
        }

        return rules;
    }

    public async Task SaveRulesAsync(List<AllocationRule> rules) {
        ValidateRules(rules);
        await ruleRepository.ReplaceRulesAsync(rules);
    }

    public async Task<List<AssetAllocation>> GenerateAndSavePortfolioAsync(decimal salary, List<AllocationRule> rules) {
        if (salary <= 0) {
            throw new ArgumentException("Salary must be above 0", nameof(salary));
        }

        ValidateRules(rules);

        var results = rules.Select(r => new AssetAllocation {
            Category = r.BucketName,
            Percentage = r.Percentage,
            Amount = salary * r.Percentage / 100m
        }).ToList();

        foreach (var item in results) {
            await allocationRepository.SaveAllocationAsync(item);
        }

        return results;
    }

    public Task<IEnumerable<AssetAllocation>> GetHistoryAsync() {
        return allocationRepository.GetAllHistoryAsync();
    }

    private static void ValidateRules(List<AllocationRule> rules) {
        if (rules.Count == 0) {
            throw new InvalidOperationException("Rule allocation empty");
        }

        var total = rules.Sum(r => r.Percentage);

        if (total != 100m) {
            throw new InvalidOperationException($"Total percentage must be 100%, now {total}%");
        }
    }
}
