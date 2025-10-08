using Blazored.Diagrams.Interfaces;

namespace Blazored.Diagrams.Options.Diagram;

/// <inheritdoc />
public class DiagramOptions : IDiagramOptions
{
    /// <inheritdoc />
    public IDiagramStyleOptions Style { get; set; } = new DiagramStyleOptions();

    /// <inheritdoc />
    public IVirtualizationOptions Virtualization { get; init; } = new VirtualizationOptions();

    /// <inheritdoc />
    public List<IBehaviourOptions> BehaviourOptions { get; set; } = [];
}