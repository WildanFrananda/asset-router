namespace AssetRouter.Presentation.Components;

using System.Globalization;
using Microsoft.AspNetCore.Components;
using AssetRouter.Core.Entities;

public partial class EmergencyFundAlert {
    [Parameter, EditorRequired]
    public EmergencyFundStatus? Status { get; set; }

    private static string Bar(decimal value) => value.ToString("0.##", CultureInfo.InvariantCulture);
}
