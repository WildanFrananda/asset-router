namespace AssetRouter.Core.Services;

using AssetRouter.Core.Entities;

public class LiquidOverflowResult {
    public List<AllocationNode> ProcessedNodes { get; set; } = new();
    public List<OverflowValve> ActiveValves { get; set; } = new();
    public decimal TotalOverflowDistributed { get; set; }
}

public class LiquidOverflowEngine {
    public LiquidOverflowResult CalculateAllocationWithOverflow(decimal salary, List<AllocationNode> nodes) {
        if (salary < 0) {
            throw new ArgumentException("Nominal salary must be greater than 0", nameof(salary));
        }

        var processedNodes = nodes.Select(n => new AllocationNode {
            Id = n.Id,
            Name = n.Name,
            Category = n.Category,
            Percentage = n.Percentage,
            TargetCapAmount = n.TargetCapAmount,
            CurrentAccumulatedAmount = n.CurrentAccumulatedAmount,
            IsOverflowEnabled = n.IsOverflowEnabled,
            OverflowTargetNodeId = n.OverflowTargetNodeId,
            X = n.X,
            Y = n.Y
        }).ToList();

        var activeValves = new List<OverflowValve>();
        decimal totalOverflow = 0m;

        foreach (var node in processedNodes) {
            node.AllocatedAmount = salary * (node.Percentage / 100m);
        }

        foreach (var node in processedNodes) {
            if (node.TargetCapAmount > 0 && node.IsOverflowEnabled) {
                decimal projectedTotal = node.CurrentAccumulatedAmount + node.AllocatedAmount;

                if (projectedTotal > node.TargetCapAmount) {
                    decimal excess = projectedTotal - node.TargetCapAmount;

                    node.AllocatedAmount = Math.Max(0m, node.TargetCapAmount - node.CurrentAccumulatedAmount);
                    totalOverflow += excess;

                    var targetNode = processedNodes.FirstOrDefault(n => n.Id == node.OverflowTargetNodeId)
                        ?? processedNodes.FirstOrDefault(n => n.Category == "Stocks" || n.Category == "Gold");

                    if (targetNode != null) {
                        targetNode.AllocatedAmount += excess;

                        activeValves.Add(new OverflowValve {
                            SourceNodeId = node.Id,
                            TargetNodeId = targetNode.Id,
                            Status = "Overflowing",
                            TransferredOverflowAmount = excess,
                            FlowVelocity = 1.5
                        });
                    }
                }
            }
        }

        return new LiquidOverflowResult {
            ProcessedNodes = processedNodes,
            ActiveValves = activeValves,
            TotalOverflowDistributed = totalOverflow
        };
    }
}
