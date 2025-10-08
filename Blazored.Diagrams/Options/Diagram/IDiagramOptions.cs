using Blazored.Diagrams.Interfaces;

namespace Blazored.Diagrams.Options.Diagram;

public interface IDiagramOptions
{
    /// <inheritdoc />
    IDiagramStyleOptions Style { get; set; }

    /// <inheritdoc />
    IVirtualizationOptions Virtualization { get; init; }

    /// <inheritdoc />
    List<IBehaviourOptions> BehaviourOptions { get; set; }
}