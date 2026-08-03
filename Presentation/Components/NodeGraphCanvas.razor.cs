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

    private AllocationNode? _draggedNode;
    private double _dragOffsetX;
    private double _dragOffsetY;

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