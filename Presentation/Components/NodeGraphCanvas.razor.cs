namespace AssetRouter.Presentation.Components;

using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AssetRouter.Core.Entities;

public partial class NodeGraphCanvas {
    [Parameter] public decimal MonthlySalary { get; set; } = 15000000m;
    [Parameter] public List<AllocationNode> Nodes { get; set; } = new();
    [Parameter] public List<OverflowValve> ActiveValves { get; set; } = new();
    [Parameter] public EventCallback<decimal> OnSalaryChanged { get; set; }
    [Parameter] public EventCallback<List<AllocationNode>> OnNodesUpdated { get; set; }

    [Parameter] public EventCallback<AllocationNode> OnNodeAdded { get; set; }
    [Parameter] public EventCallback<Guid> OnNodeDeleted { get; set; }
    [Parameter] public EventCallback OnNodesReset { get; set; }

    private AllocationNode? _draggedNode;
    private double _dragOffsetX;
    private double _dragOffsetY;

    // Modal State                                                                                                                                                                                      
    protected bool _showAddModal = false;
    protected string _newNodeName = "";
    protected string _newNodeCategory = "Stocks";
    protected decimal _newNodePercentage = 10m;
    protected decimal _newNodeTargetCap = 0m;

    private async Task OnSalaryInputChange(ChangeEventArgs e) {
        if (decimal.TryParse(e.Value?.ToString(), out decimal val) && val > 0) {
            await OnSalaryChanged.InvokeAsync(val);
        }
    }

    private async Task OnNodePercentChange(ChangeEventArgs e, AllocationNode node) {
        if (decimal.TryParse(e.Value?.ToString(), out decimal pct)) {
            node.Percentage = Math.Clamp(pct, 0m, 100m);
            await OnNodesUpdated.InvokeAsync(Nodes);
        }
    }

    private void StartDraggingNode(MouseEventArgs e, AllocationNode node) {
        _draggedNode = node;
        _dragOffsetX = e.ClientX - node.X;
        _dragOffsetY = e.ClientY - node.Y;
    }

    private void HandleCanvasMouseMove(MouseEventArgs e) {
        if (_draggedNode != null) {
            _draggedNode.X = Math.Max(10, e.ClientX - _dragOffsetX);
            _draggedNode.Y = Math.Max(10, e.ClientY - _dragOffsetY);
        }
    }

    private async Task HandleCanvasMouseUp() {
        if (_draggedNode != null) {
            _draggedNode = null;
            await OnNodesUpdated.InvokeAsync(Nodes);
        }
    }

    protected void ShowAddModal() {
        _newNodeName = "";
        _newNodePercentage = 10m;
        _newNodeTargetCap = 0m;
        _showAddModal = true;
    }

    protected void CloseAddModal() => _showAddModal = false;

    protected async Task ConfirmAddNode() {
        if (string.IsNullOrWhiteSpace(_newNodeName)) return;

        var newNode = new AllocationNode {
            Id = Guid.NewGuid(),
            Name = _newNodeName,
            Category = _newNodeCategory,
            Percentage = _newNodePercentage,
            TargetCapAmount = _newNodeTargetCap,
            CurrentAccumulatedAmount = 0m,
            X = 620,
            Y = 100 + (Nodes.Count * 60)
        };

        _showAddModal = false;
        await OnNodeAdded.InvokeAsync(newNode);
    }

    protected async Task TriggerNodeDelete(Guid id) {
        await OnNodeDeleted.InvokeAsync(id);
    }

    protected async Task TriggerNodesReset() {
        await OnNodesReset.InvokeAsync();
    }

    protected static string GetCubicBezierPath(double x1, double y1, double x2, double y2) {
        double dx = Math.Abs(x2 - x1) * 0.5;
        return string.Format(CultureInfo.InvariantCulture, "M {0},{1} C {2},{1} {3},{4} {5},{4}",
            x1, y1, x1 + dx, x2 - dx, y2, x2);
    }

    protected static string FormatCurrency(decimal amount) {
        var culture = new CultureInfo("id-ID");
        return string.Format(culture, "Rp {0:N0}", amount);
    }
}