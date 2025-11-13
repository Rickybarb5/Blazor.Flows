using Blazor.Flows.Groups;
using Blazor.Flows.Interfaces;
using Blazor.Flows.Layers;
using Blazor.Flows.Links;
using Blazor.Flows.Nodes;
using Blazor.Flows.Ports;

namespace Blazor.Flows.Services.Diagrams;

public partial class DiagramService
{
    /// <inheritdoc />
    public IDiagramService AddGroupTo(IGroupContainer parent, IGroup group)
    {
        switch (parent)
        {
            case IGroup groupParent when groupParent.Id == group.Id || groupParent.AllGroups.Any(g => g.Id == group.Id):
                throw new InvalidOperationException("Cannot add group to itself.");
            // Add parent group if not in diagram.
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                throw new InvalidOperationException($"Group {groupParent.Id} hasn't been added to the diagram yet.");
            // Add layer if not in diagram.
            case ILayer layerParent when Diagram.Layers.All(n => n.Id != layerParent.Id):
                throw new InvalidOperationException($"Layer {layerParent.Id} hasn't been added to the diagram yet.");
        }

        parent.Groups.AddInternal(group);
        return this;
    }


    /// <inheritdoc />
    public IDiagramService AddNodeTo(INodeContainer parent, INode node)
    {
        switch (parent)
        {
            // Add parent group if not in diagram
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                throw new InvalidOperationException($"Group {groupParent.Id} hasn't been added to the diagram yet.");
            // Add parent layer if not in diagram
            case ILayer layerParent when Diagram.Layers.All(n => n.Id != layerParent.Id):
                throw new InvalidOperationException($"Layer {layerParent.Id} hasn't been added to the diagram yet.");
        }

        parent.Nodes.AddInternal(node);
        return this;
    }

    /// <inheritdoc />
    public IDiagramService AddPortTo(IPortContainer parent, IPort port)
    {
        switch (parent)
        {
            // Add parent group if not in diagram
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                throw new InvalidOperationException($"Group {groupParent.Id} hasn't been added to the diagram yet.");
                break;
            // Add parent node if not in diagram
            case INode nodeParent when Diagram.AllNodes.All(n => n.Id != nodeParent.Id):
                throw new InvalidOperationException($"Node {nodeParent.Id} hasn't been added to the diagram yet.");
                break;
        }

        parent.Ports.AddInternal(port);
        return this;
    }

    /// <inheritdoc />
    public IDiagramService AddLinkTo(ILinkContainer sourcePort, ILinkContainer? targetPort, ILink link)
    {
        if (sourcePort is IPort sp && Diagram.AllPorts.All(n => n.Id != sp.Id))
        {
            AddPortTo(sp.Parent, sp);
        }

        if (targetPort is IPort tp && Diagram.AllPorts.All(n => n.Id != tp.Id))
        {
            AddPortTo(tp.Parent, tp);
        }

        sourcePort.OutgoingLinks.AddInternal(link);
        targetPort?.IncomingLinks.AddInternal(link);
        return this;
    }

    /// <inheritdoc />
    public virtual IDiagramService AddNode(INode node)
    {
        if (Diagram.AllNodes.All(x => x.Id != node.Id))
        {
            Diagram.CurrentLayer.Nodes.AddInternal(node);
        }
        return this;
    }

    /// <inheritdoc />
    public virtual IDiagramService AddGroup(IGroup group)
    {
        if (Diagram.AllGroups.All(x => x.Id != group.Id))
        {
            Diagram.CurrentLayer.Groups.AddInternal(group);
        }
       
        return this;
    }

    /// <inheritdoc />
    public virtual IDiagramService AddLayer(ILayer layer)
    {
        if (Diagram.Layers.All(x => x.Id != layer.Id))
        {
            Diagram.Layers.AddInternal(layer);
        }
        return this;
    }
}