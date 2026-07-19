namespace AssetRouter.Core.Entities;

public class AllocationRule {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string BucketName { get; set; } = string.Empty;
    public decimal Percentage { get; set; }
    public int SortOrder { get; set; }
}