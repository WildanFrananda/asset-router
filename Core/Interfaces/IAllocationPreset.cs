namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IAllocationPreset {
    string Name { get; }
    IEnumerable<AllocationRule> GetRules();
}
