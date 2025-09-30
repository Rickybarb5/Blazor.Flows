using Blazored.Diagrams.Diagrams;
using Blazored.Diagrams.Groups;
using Blazored.Diagrams.Layers;
using Blazored.Diagrams.Links;
using Blazored.Diagrams.Nodes;
using Blazored.Diagrams.Ports;

namespace Blazored.Diagrams.Services.Diagrams;

/// <inheritdoc />
public class DeleteContainer : IDeleteContainer
{
    private IDiagram Diagram;
    public DeleteContainer(IDiagram diagram)
    {
        Diagram = diagram;
    }
    /// <inheritdoc />
    public virtual IDeleteContainer Node(INode nodeToRemove)
    {
        if (Diagram.Layers.Select(layer => layer.Nodes.Remove(nodeToRemove)).Any(removed => removed))
        {
            return this;
        }

        Diagram.Layers
            .SelectMany(x => x.AllGroups)
            .Select(group => group.Nodes.Remove(nodeToRemove))
            .Any(removed => removed);

        return this;
    }

    /// <inheritdoc />
    public virtual IDeleteContainer Group(IGroup groupToRemove)
    {
        foreach (var layer in Diagram.Layers)
        {
            var removed = layer.Groups.Remove(groupToRemove);
            if (removed) 
                return this;
        }

        Diagram.Layers
            .SelectMany(x => x.AllGroups)
            .Select(group => group.Groups.Remove(groupToRemove))
            .Any(removed => removed);
        
        return this;
    }

    /// <inheritdoc />
    public virtual IDeleteContainer Layer(ILayer layer)
    {
        Diagram.Layers.Remove(layer);
        return this;
    }

    /// <inheritdoc />
    public IDeleteContainer Remove(IPort port)
    {
        var success = Diagram.Layers
            .SelectMany(x => x.AllNodes)
            .Select(node => node.Ports.Remove(port))
            .Any(removed => removed);
        if (!success)
        {
            Diagram.Layers
                .SelectMany(x => x.AllGroups)
                .Select(group => group.Ports.Remove(port))
                .Any(removed => removed);
        }

        return this;
    }

    /// <inheritdoc />
    public virtual IDeleteContainer Link(ILink linkToRemove)
    {
        linkToRemove.SourcePort.OutgoingLinks.Remove(linkToRemove);
        linkToRemove?.TargetPort?.IncomingLinks.Remove(linkToRemove);
        
        return this;
    }
}