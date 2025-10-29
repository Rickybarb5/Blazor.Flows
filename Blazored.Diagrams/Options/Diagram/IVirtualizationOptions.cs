namespace Blazored.Diagrams.Options.Diagram;

/// <summary>
/// Defines the virtualization options.
/// </summary>
public interface IVirtualizationOptions
{
    /// <summary>
    /// Enables/disables virtualization.
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// Space in pixels to make model outside the viewport disappear.
    /// </summary>
    int BufferSize { get; set; }
}