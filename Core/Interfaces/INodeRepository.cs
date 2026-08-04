namespace AssetRouter.Core.Interfaces;

using AssetRouter.Core.Entities;

public interface INodeRepository {
    Task<List<AllocationNode>> GetNodesAsync();

    Task SaveNodesAsync(IEnumerable<AllocationNode> nodes);
    Task AddNodeAsync(AllocationNode node);
    Task DeleteNodeAsync(Guid nodeId);
    Task ResetToDefaultAsync();

    List<AllocationNode> GetDefaultPresetNodes();
}