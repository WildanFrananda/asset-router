namespace AssetRouter.Infrastructure.Strategies;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;

public class DefaultPreset : IAllocationPreset {
    public const string PresetName = "Default";

    public string Name => PresetName;

    public IEnumerable<AllocationRule> GetRules() => [
        new() { BucketName = "Dana Darurat", Percentage = 20m, SortOrder = 1 },
        new() { BucketName = "Emas", Percentage = 15m, SortOrder = 2 },
        new() { BucketName = "Saham (Fundamental)", Percentage = 30m, SortOrder = 3 },
        new() { BucketName = "Kripto", Percentage = 15m, SortOrder = 4 },
        new() { BucketName = "Kebutuhan Hidup / Sisa", Percentage = 20m, SortOrder = 5 }
    ];
}
