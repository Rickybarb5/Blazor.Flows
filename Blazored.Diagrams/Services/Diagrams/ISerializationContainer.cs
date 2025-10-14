using Blazored.Diagrams.Diagrams;

namespace Blazored.Diagrams.Services.Diagrams;

/// <summary>
/// Allows for saving/ loading diagrams.
/// </summary>
public interface ISerializationContainer
{
    /// <summary>
    /// Saves the current diagram instance to json.
    /// </summary>
    /// <param name="diagram"></param>
    /// <typeparam name="TDiagram"></typeparam>
    /// <returns></returns>
    string Save<TDiagram>(TDiagram diagram)
        where TDiagram : IDiagram;

    
    /// <summary>
    /// Loads a JSON and uses the deserialization as the new diagram for the diagram service.
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="TDiagram"></typeparam>
    /// <returns></returns>
    TDiagram Load<TDiagram>(string json)
        where TDiagram : IDiagram;
}