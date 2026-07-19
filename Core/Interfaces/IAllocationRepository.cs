namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IAllocationRepository {
    Task SaveAllocationAsync(AssetAllocation allocation);
    Task<IEnumerable<AssetAllocation>> GetAllHistoryAsync();
}