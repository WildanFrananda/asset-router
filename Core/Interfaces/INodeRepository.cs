namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface INodeRepository {
    Task<List<AllocationNode>> GetNodesAsync();

    Task SaveNodesAsync(IEnumerable<AllocationNode> nodes);

    List<AllocationNode> GetDefaultPresetNodes();
}