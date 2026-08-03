namespace AssetRouter.Presentation.Components;

using System.Globalization;
using AssetRouter.Core.Entities;
using Microsoft.AspNetCore.Components;

public partial class AllocationChart {
    [Parameter, EditorRequired]
    public IEnumerable<AssetAllocation> Items { get; set; } = [];

    private const decimal Circumference = 502.65m;

    private static readonly string[] Palette = [
        "#3498db", "#f1c40f", "#2ecc71", "#9b59b6",
        "#e67e22", "#1abc9c", "#e74c3c", "#34495e"
    ];

    private record Segment(string Category, decimal Percentage, decimal Amount, string Color, string Dash, string Offset);

    private List<Segment> Segments = new();

    private decimal TotalAmount;

    protected override void OnParametersSet() {
        Segments = BuildSegments();
        TotalAmount = Items.Sum(i => i.Amount);
    }

    private List<Segment> BuildSegments() {
        var result = new List<Segment>();
        var totalPct = Items.Sum(i => i.Percentage);

        if (totalPct <= 0) {
            return result;
        }

        var offset = 0m;
        var colorIndex = 0;

        foreach (var item in Items) {
            var arcLength = Circumference * item.Percentage / totalPct;
            result.Add(new Segment(
                item.Category,
                item.Percentage,
                item.Amount,
                Palette[colorIndex % Palette.Length],
                $"{Svg(arcLength)} {Svg(Circumference)}",
                Svg(-offset)
            ));
            offset += arcLength;
            colorIndex++;
        }

        return result;
    }

    private static string Svg(decimal value) => value.ToString("0.##", CultureInfo.InvariantCulture);
}

