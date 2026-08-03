namespace AssetRouter.Application.Services;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;
using AssetRouter.Infrastructure.Strategies;

public class PortfolioManagerService(
    IEnumerable<IAllocationPreset> presets,
    IRuleRepository ruleRepository,
    IAllocationRepository allocationRepository
) {
    public IEnumerable<string> AvailablePresets => presets.Select(s => s.Name);
    public const string EmergencyBucket = "Emergency Fund";
    public const string ExpenseBucket = "Expense Fund";
    private const int TargetMonths = 6;

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
            throw new ArgumentException("Salary must be above 0", nameof(salary));
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

    public async Task<EmergencyFundStatus> GetEmergencyFundStatusAsync(List<AllocationRule> rules) {
        var history = await allocationRepository.GetHistoryAsync();

        var accumulated = history
            .SelectMany(s => s.Items)
            .Where(i => i.Category == EmergencyBucket)
            .Sum(i => i.Amount);

        var lastSalary = history.FirstOrDefault()?.Salary ?? 0m;
        var expensePercent = rules.FirstOrDefault(r => r.BucketName == ExpenseBucket)?.Percentage ?? 0m;
        var monthlyExpense = lastSalary * expensePercent / 100m;

        var status = new EmergencyFundStatus {
            Accumulated = accumulated,
            MonthlyExpense = monthlyExpense,
            Target = monthlyExpense * TargetMonths
        };

        if (status.IsTargetReached) {
            status.SuggestedBucket = rules
                .Where(r => r.BucketName != EmergencyBucket && r.BucketName != ExpenseBucket)
                .OrderBy(r => r.Percentage)
                .FirstOrDefault()?.BucketName;
        }

        return status;
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
