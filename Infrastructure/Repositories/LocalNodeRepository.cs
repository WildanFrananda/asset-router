namespace AssetRouter.Infrastructure.Repositories;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;

public class LocalNodeRepository : INodeRepository {
    private List<AllocationNode> _inMemoryNodes = new();

    public Task<List<AllocationNode>> GetNodesAsync() {
        if (_inMemoryNodes.Count == 0) {
            _inMemoryNodes = GetDefaultPresetNodes();
        }
        return Task.FromResult(_inMemoryNodes);
    }

    public Task SaveNodesAsync(IEnumerable<AllocationNode> nodes) {
        _inMemoryNodes = nodes.ToList();
        return Task.CompletedTask;
    }

    public List<AllocationNode> GetDefaultPresetNodes() {
        var emergencyId = Guid.NewGuid();
        var stocksId = Guid.NewGuid();
        var goldId = Guid.NewGuid();
        var cryptoId = Guid.NewGuid();
        var expensesId = Guid.NewGuid();

        return new List<AllocationNode> {
            new AllocationNode {
                Id = emergencyId,
                Name = "Emergency Fund",
                Category = "Emergency",
                Percentage = 20m,
                TargetCapAmount = 30000000m,
                CurrentAccumulatedAmount = 15000000m,
                IsOverflowEnabled = true,
                OverflowTargetNodeId = stocksId,
                X = 320,
                Y = 60
            },
            new AllocationNode {
                Id = expensesId,
                Name = "Living Expenses",
                Category = "Expenses",
                Percentage = 35m,
                TargetCapAmount = 0m,
                CurrentAccumulatedAmount = 0m,
                IsOverflowEnabled = false,
                X = 320,
                Y = 240
            },
            new AllocationNode {
                Id = goldId,
                Name = "Gold (Hedging)",
                Category = "Gold",
                Percentage = 15m,
                TargetCapAmount = 0m,
                CurrentAccumulatedAmount = 10000000m,
                IsOverflowEnabled = false,
                X = 620,
                Y = 60
            },
            new AllocationNode {
                Id = stocksId,
                Name = "Fundamental Stocks",
                Category = "Stocks",
                Percentage = 20m,
                TargetCapAmount = 0m,
                CurrentAccumulatedAmount = 25000000m,
                IsOverflowEnabled = false,
                X = 620,
                Y = 240
            },
            new AllocationNode {
                Id = cryptoId,
                Name = "Crypto Assets",
                Category = "Crypto",
                Percentage = 10m,
                TargetCapAmount = 0m,
                CurrentAccumulatedAmount = 5000000m,
                IsOverflowEnabled = false,
                X = 620,
                Y = 420
            }
        };
    }
}