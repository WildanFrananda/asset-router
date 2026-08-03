namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;

public class ConservativePreset : IAllocationPreset {
    public string Name => "Conservative (Safe)";

    public IEnumerable<AllocationRule> GetRules() => [
        new() { BucketName = "Dana Darurat", Percentage = 35m, SortOrder = 1 },
        new() { BucketName = "Emas", Percentage = 25m, SortOrder = 2 },
        new() { BucketName = "Saham (Fundamental)", Percentage = 20m, SortOrder = 3 },
        new() { BucketName = "Kripto", Percentage = 5m, SortOrder = 4 },
        new() { BucketName = "Kebutuhan Hidup / Sisa", Percentage = 15m, SortOrder = 5 }
    ];
}
