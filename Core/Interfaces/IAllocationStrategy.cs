namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IAllocationStrategy {
    string ProfileName { get; }
    IEnumerable<AssetAllocation> Calculate(decimal salary);
}
