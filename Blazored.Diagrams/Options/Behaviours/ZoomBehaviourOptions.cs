
using Blazored.Diagrams.Behaviours;
using Newtonsoft.Json;

namespace Blazored.Diagrams.Options.Behaviours;

/// <summary>
/// Options for <see cref="ZoomBehavior"/>
/// </summary>
public class ZoomBehaviourOptions : BaseBehaviourOptions
{
    /// <summary>
    ///     Default value for  the zoom step.
    /// </summary>
    [JsonIgnore] private const decimal DefaultZoomStep = 0.1m;

    /// <summary>
    ///     Default value for the minimum  zoom value.
    ///     Must be positive.
    /// </summary>
    [JsonIgnore] private const decimal DefaultMinimumZoom = 0.1m;

    /// <summary>
    ///     Default value for the maximum zoom value.
    /// </summary>
    [JsonIgnore] private const decimal DefaultMaximumZoom = 3;

    /// <summary>
    ///     Minimum allowed zoom value of the diagram.
    /// </summary>
    public decimal MinZoom { get; set; } = DefaultMinimumZoom;

    /// <summary>
    ///     Maximum allowed zoom value of the diagram.
    /// </summary>
    public decimal MaxZoom { get; set; } = DefaultMaximumZoom;

    /// <summary>
    ///     Step value from zooming with the pointer wheel.
    /// </summary>
    public decimal ZoomStep { get; set; } = DefaultZoomStep;
}