using System.Text.Json;
using Blazored.Diagrams.Diagrams;
using Blazored.Diagrams.Services.Serialization;

namespace Blazored.Diagrams.Services.Diagrams;

/// <inheritdoc />
public class SerializationContainer : ISerializationContainer
{
    private readonly IDiagramSerializationService _diagramSerializationService;
    private readonly IDiagramService _diagramService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    public SerializationContainer(IDiagramService service)
    {
        _diagramService = service;
        _diagramSerializationService = new DiagramSerializationService();
    }

    /// <inheritdoc />
    public string Save<TDiagram>(TDiagram diagram)
        where TDiagram : IDiagram
    {
        var json = _diagramSerializationService.ToJson(diagram);
        return json;
    }

    /// <inheritdoc />
    public TDiagram FromJson<TDiagram>(string json)
        where TDiagram : IDiagram
    {
        var diagram = _diagramSerializationService.FromJson<TDiagram>(json);
        _diagramService.SwitchDiagram(diagram);
        return diagram;
    }
}