namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface IAllocationRepository {
    Task SaveSnapshotAsync(AllocationSnapshot snapshot);
    Task<List<AllocationSnapshot>> GetHistoryAsync();
}