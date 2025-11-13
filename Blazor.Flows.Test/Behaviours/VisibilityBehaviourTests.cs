using Blazor.Flows.Extensions;
using Blazor.Flows.Groups;
using Blazor.Flows.Links;
using Blazor.Flows.Nodes;
using Blazor.Flows.Ports;
using Blazor.Flows.Services.Diagrams;

namespace Blazor.Flows.Test.Behaviours;

public class VisibilityBehaviourTests
{
    private IDiagramService CreateService()
    {
        var service = new DiagramService();
        return service;
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Visibility_Toggle_For_Layer_Affects_Child_Components(bool isVisible)
    {
        // Arrange
        var service = CreateService();
        service.AddNode(new Node());
        service.AddGroup(new Group());
        service.AddPortTo(service.Diagram.AllNodes.First(), new Port());
        service.AddPortTo(service.Diagram.AllGroups.First(), new Port());
        service.AddLinkTo(
            service.Diagram.AllNodes[0].Ports[0], 
            service.Diagram.AllGroups[0].Ports[0],
            new OrthogonalLink());

        // Act
        service.Diagram.CurrentLayer.IsVisible = isVisible;

        //Assert
        Assert.Equal(isVisible, service.Diagram.CurrentLayer.IsVisible);
        service.Diagram.CurrentLayer.AllNodes.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        service.Diagram.CurrentLayer.AllPorts.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        service.Diagram.CurrentLayer.AllGroups.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        service.Diagram.CurrentLayer.AllLinks.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Visibility_Toggle_For_Node_Affects_Child_Components(bool isVisible)
    {
        // Arrange
        var service = CreateService();
        var model = new Node();
        var model2 = new Node();
        service.AddNode(model);
        service.AddNode(model2);
        service.AddPortTo(model, new Port());
        service.AddPortTo(model2, new Port());
        service.AddLinkTo(model2.Ports[0],model.Ports[0], new LineLink());

        // Act
        model.IsVisible = isVisible;

        //Assert
        Assert.Equal(isVisible, model.IsVisible);
        model.Ports.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        model.Ports.SelectMany(x=>x.OutgoingLinks).ForEach(x => Assert.Equal(isVisible, x.IsVisible));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Visibility_Toggle_For_Ports_Affects_Child_Components(bool isVisible)
    {
        // Arrange
        var service = CreateService();
        var model = new Port();
        var model3 = new Node();
        var model2 = new Node();
        service.AddNode(model3);
        service.AddNode(model2);
        service.AddPortTo(model2, new Port());
        service.AddPortTo(model3, new Port());
        service.AddLinkTo(model2.Ports[0],model3.Ports[0], new LineLink());

        // Act
        model.IsVisible = isVisible;

        //Assert
        Assert.Equal(isVisible, model.IsVisible);
        model.OutgoingLinks.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Visibility_Toggle_For_Groups_Affects_Child_Components(bool isVisible)
    {
        // Arrange
        var service = CreateService();
        var model = new Group();
        var model3 = new Group();
        var model2 = new Node();
        service.AddGroup(model);
        service.AddNodeTo(model,model2);
        service.AddGroupTo(model,model3);
        service.AddPortTo(model, new Port());
        service.AddPortTo(model2, new Port());
        service.AddPortTo(model3, new Port());
        service.AddLinkTo(model2.Ports[0],model.Ports[0], new LineLink());
        service.AddLinkTo(model3.Ports[0],model2.Ports[0], new LineLink());

        // Act
        model.IsVisible = isVisible;

        //Assert
        Assert.Equal(isVisible, model.IsVisible);
        model.AllPorts.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        model.AllNodes.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        model.AllGroups.ForEach(x => Assert.Equal(isVisible, x.IsVisible));
        model.AllPorts.SelectMany(x=>x.OutgoingLinks).ForEach(x => Assert.Equal(isVisible, x.IsVisible));
    }
}