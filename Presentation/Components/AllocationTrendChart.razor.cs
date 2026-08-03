namespace AssetRouter.Presentation.Components;

using System.Globalization;
using AssetRouter.Core.Entities;
using Microsoft.AspNetCore.Components;

public partial class AllocationTrendChart {
    [Parameter, EditorRequired]
    public List<AllocationSnapshot> History { get; set; } = new();

    private const decimal ChartHeight = 150m;
    private const decimal BaseLine = 160m;
    private const decimal BarWidth = 34m;
    private const int MaxBars = 6;

    private static readonly string[] Palette = [
        "#3498db", "#f1c40f", "#2ecc71", "#9b59b6",
        "#e67e22", "#1abc9c", "#e74c3c", "#34495e"
    ];

    private record Segment(string Category, decimal Amount, decimal Y, decimal Height, string Color);
    private record Bar(decimal X, string Label, List<Segment> Segments);
    private record Legend(string Category, string Color);

    private List<Bar> Bars = new();
    private List<Legend> LegendEntries = new();

    protected override void OnParametersSet() {
        Bars = BuildBars();
    }

    private List<Bar> BuildBars() {
        var result = new List<Bar>();

        var recent = History
            .OrderByDescending(s => s.CreatedAt)
            .Take(MaxBars)
            .OrderBy(s => s.CreatedAt)
            .ToList();

        if (recent.Count == 0) {
            return result;
        }

        var categories = recent
            .SelectMany(s => s.Items.Select(i => i.Category))
            .Distinct()
            .ToList();

        LegendEntries = categories
            .Select((c, i) => new Legend(c, Palette[i % Palette.Length]))
            .ToList();

        var colorOf = LegendEntries.ToDictionary(l => l.Category, l => l.Color);
        var maxSalary = recent.Max(s => s.Salary);

        if (maxSalary <= 0) {
            return result;
        }

        var gap = (320m - recent.Count * BarWidth) / (recent.Count + 1);

        for (var index = 0; index < recent.Count; index++) {
            var snapshot = recent[index];
            var x = gap + index * (BarWidth + gap);
            var cursor = BaseLine;
            var segments = new List<Segment>();

            foreach (var item in snapshot.Items) {
                var height = item.Amount / maxSalary * ChartHeight;
                cursor -= height;
                segments.Add(new Segment(
                    item.Category,
                    item.Amount,
                    cursor,
                    height,
                    colorOf.GetValueOrDefault(item.Category, "#bdc3c7")
                ));
            }

            result.Add(new Bar(x, snapshot.CreatedAt.ToLocalTime().ToString("dd/MM"), segments));
        }

        return result;
    }

    private static string Num(decimal value) => value.ToString("0.##", CultureInfo.InvariantCulture);
}
