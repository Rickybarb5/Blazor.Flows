namespace Blazored.Diagrams.Components.Models;

/// <summary>
///     How the <see cref="DefaultLinkComponent"/> is drawn.
/// </summary>
public enum DefaultLinkComponentPathType
{
    /// <summary>
    /// Curved link.
    /// </summary>
    Curved,

    /// <summary>
    /// Straight line.
    /// </summary>
    Line,

    /// <summary>
    /// Orthogonal line
    /// </summary>
    Orthogonal,
    
    /// <summary>
    /// Custom path will be used.
    /// </summary>
    Custom,
}