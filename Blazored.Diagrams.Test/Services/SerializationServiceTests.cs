using Blazored.Diagrams.Diagrams;
using Blazored.Diagrams.Groups;
using Blazored.Diagrams.Links;
using Blazored.Diagrams.Nodes;
using Blazored.Diagrams.Ports;
using Blazored.Diagrams.Services.Diagrams;
using Blazored.Diagrams.Services.Providers;
using Blazored.Diagrams.Services.Serialization;

namespace Blazored.Diagrams.Test.Services;

public class SerializationServiceTests
{
    private IDiagramService diagramService;
    private ISerializationService serializationService;

    public SerializationServiceTests()
    {
        var provider = new DiagramServiceProvider();
        diagramService = provider.GetDiagramService(new Diagram());
        serializationService = new SerializationService();
    }
    
    [Fact]
    public void Serialization_Works()
    {
        // Arrange
        var node = new Node();
        var group = new Group();
        var groupNode = new Node();
        var nodePort = new Port();
        var groupPort = new Port();
        var link = new Link();
        diagramService
            .Add
            .Node(node)
            .Group(group)
            .NodeTo(group, groupNode)
            .PortTo(node, nodePort)
            .PortTo(group, groupPort)
            .AddLinkTo(nodePort, groupPort, link);
        
        var expected = serializationService.ToJson(diagramService.Diagram);
        
        // Act
        var diagram = serializationService.FromJson<Diagram>(expected);
        var actual = serializationService.ToJson<Diagram>(diagram);
        
        Assert.Equal(expected, actual);
    }
}