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
            
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                AddGroup(groupParent);
                break;
            
            case ILayer layerParent when Diagram.Layers.All(n => n.Id != layerParent.Id):
                AddLayer(layerParent);
                break;
        }

        parent.Groups.AddInternal(group);
        return this;
    }


    /// <inheritdoc />
    public IDiagramService AddNodeTo(INodeContainer parent, INode node)
    {
        switch (parent)
        {
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                AddGroup(groupParent);
                break;
            
            case ILayer layerParent when Diagram.Layers.All(n => n.Id != layerParent.Id):
                AddLayer(layerParent);
                break;
        }

        parent.Nodes.AddInternal(node);
        return this;
    }

    /// <inheritdoc />
    public IDiagramService AddPortTo(IPortContainer parent, IPort port)
    {
        switch (parent)
        {
            case IGroup groupParent when Diagram.AllGroups.All(n => n.Id != groupParent.Id):
                AddGroup(groupParent);
                break;
            
            case INode nodeParent when Diagram.AllNodes.All(n => n.Id != nodeParent.Id):
                AddNode(nodeParent);
                break;
        }

        parent.Ports.AddInternal(port);
        return this;
    }

    /// <inheritdoc />
    public IDiagramService AddLinkTo(IPort sourcePort, IPort targetPort, ILink link)
    {
        if (!sourcePort.CanCreateLink() && !sourcePort.CanConnectTo(targetPort))
        {
            return this;
        }
        if (Diagram.AllPorts.All(n => n.Id != sourcePort.Id))
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (sourcePort.Parent is null)
            {
                throw new InvalidOperationException($"Source port does not have a parent.");
            }
            AddPortTo(sourcePort.Parent, sourcePort);
        }

        if (Diagram.AllPorts.All(n => n.Id != targetPort.Id))
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (targetPort.Parent is null)
            {
                throw new InvalidOperationException($"Target port does not have a parent.");
            }
            AddPortTo(targetPort.Parent, targetPort);
        }

        sourcePort.OutgoingLinks.AddInternal(link);
        targetPort.IncomingLinks.AddInternal(link);
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