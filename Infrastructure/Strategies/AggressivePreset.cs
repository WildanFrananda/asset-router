namespace AssetRouter.Infrastructure.Strategies;

using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Entities;

public class AggressivePreset : IAllocationPreset {
    public string Name => "Aggressive (High Risk)";

    public IEnumerable<AllocationRule> GetRules() => [
        new() { BucketName = "Dana Darurat", Percentage = 10m, SortOrder = 1 },
        new() { BucketName = "Emas", Percentage = 5m, SortOrder = 2 },
        new() { BucketName = "Saham (Fundamental)", Percentage = 40m, SortOrder = 3 },
        new() { BucketName = "Kripto", Percentage = 25m, SortOrder = 4 },
        new() { BucketName = "Kebutuhan Hidup / Sisa", Percentage = 20m, SortOrder = 5 }
    ];
}
