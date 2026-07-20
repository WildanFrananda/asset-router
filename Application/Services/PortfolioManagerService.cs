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

    public async Task<AllocationSnapshot> GenerateAndSavePortfolioAsync(decimal salary, List<AllocationRule> rules) {
        if (salary <= 0) {
            throw new ArgumentException("Gaji harus di atas 0", nameof(salary));
        }
        ValidateRules(rules);

        var snapshot = new AllocationSnapshot {
            Salary = salary,
            Items = rules.Select(r => new AssetAllocation {
                Category = r.BucketName,
                Percentage = r.Percentage,
                Amount = salary * r.Percentage / 100m
            }).ToList()
        };

        await allocationRepository.SaveSnapshotAsync(snapshot);
        return snapshot;
    }

    public Task<List<AllocationSnapshot>> GetHistoryAsync() {
        return allocationRepository.GetHistoryAsync();
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
