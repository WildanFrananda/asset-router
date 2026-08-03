namespace AssetRouter.Core.Entities;

public class OverflowValve {
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SourceNodeId { get; set; }

    public Guid TargetNodeId { get; set; }

    public string Status { get; set; } = "Idle";

    public decimal TransferredOverflowAmount { get; set; }

    public double FlowVelocity { get; set; } = 1.0;

    public bool IsOverflowing => TransferredOverflowAmount > 0;
}