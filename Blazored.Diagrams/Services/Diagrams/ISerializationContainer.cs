using Blazored.Diagrams.Diagrams;

namespace Blazored.Diagrams.Services.Diagrams;

/// <summary>
/// Allows for saving/ loading diagrams.
/// </summary>
public interface ISerializationContainer
{
    string Save<TDiagram>(TDiagram diagram)
        where TDiagram : IDiagram;

    /// <inheritdoc />
    TDiagram FromJson<TDiagram>(string json)
        where TDiagram : IDiagram;
}