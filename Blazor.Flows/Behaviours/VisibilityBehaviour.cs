using Blazor.Flows.Events;
using Blazor.Flows.Extensions;
using Blazor.Flows.Groups;
using Blazor.Flows.Layers;
using Blazor.Flows.Nodes;
using Blazor.Flows.Options.Behaviours;
using Blazor.Flows.Ports;
using Blazor.Flows.Services.Diagrams;

namespace Blazor.Flows.Behaviours;

/// <summary>
/// Handles visibility changes for a  model and its children.
/// </summary>
public class VisibilityBehaviour : BaseBehaviour
{
    private readonly IDiagramService _service;
    private readonly VisibilityBehaviourOptions _behaviourOptions;

    /// <summary>
    /// Instantiates a new <see cref="VisibilityBehaviour"/>
    /// </summary>
    /// <param name="service"><see cref="IDiagramService"/>.</param>
    public VisibilityBehaviour(IDiagramService service)
    {
        _service = service;
        _behaviourOptions = _service.Behaviours.GetBehaviourOptions<VisibilityBehaviourOptions>();
        _behaviourOptions.OnEnabledChanged.Subscribe(OnEnabledChanged);
        OnEnabledChanged(_behaviourOptions.IsEnabled);
    }
    private void OnEnabledChanged(BehaviourEnabledEvent ev)
    {
        OnEnabledChanged(ev.IsEnabled);
    }

    private void OnEnabledChanged(bool isEnabled)
    {
        if (isEnabled)
        {
            SubscribeToEvents();
        }
        else
        {
            DisposeSubscriptions();
        }
    }

    private void SubscribeToEvents()
    {
        Subscriptions =
        [
            _service.Events.SubscribeTo<GroupVisibilityChangedEvent>(e => HandleVisibilityChanged(e.Model)),
            _service.Events.SubscribeTo<NodeVisibilityChangedEvent>(e => HandleVisibilityChanged(e.Model)),
            _service.Events.SubscribeTo<LayerVisibilityChangedEvent>(e => HandleVisibilityChanged(e.Model)),
            _service.Events.SubscribeTo<PortVisibilityChangedEvent>(e => HandleVisibilityChanged(e.Model)),
        ];
    }

    /// <summary>
    /// Groups and its children are rendered in separate. When the group is not visible,
    /// its children should also be.
    /// </summary>
    /// <param name="group"></param>
    private void HandleVisibilityChanged(IGroup group)
    {
        group.AllNodes.ForEach(x=>
        {
            x.IsVisible = group.IsVisible;
            HandleVisibilityChanged(x);
        });
        group.AllGroups.ForEach(x=>
        {
            x.IsVisible = group.IsVisible;
            HandleVisibilityChanged(x);
        });
        group.AllPorts.ForEach(x=>
        {
            x.IsVisible = group.IsVisible;
            HandleVisibilityChanged(x);
        });
    }

    private void HandleVisibilityChanged(ILayer layer)
    {
        layer.Nodes.ForEach(x=>
        {
            x.IsVisible = layer.IsVisible;
            HandleVisibilityChanged(x);
        });
        layer.Groups.ForEach(x=>
        {
            x.IsVisible = layer.IsVisible;
            HandleVisibilityChanged(x);
        });
    }

    private void HandleVisibilityChanged(INode node)
    {
        
        node.Ports.ForEach(x=>
        {
            x.IsVisible = node.IsVisible;
            HandleVisibilityChanged(x);
        });
    }

    private void HandleVisibilityChanged(IPort port)
    {
        port.OutgoingLinks.ForEach(x=>
        {
            x.IsVisible = port.IsVisible;
        });
    }
    
    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        _behaviourOptions.OnEnabledChanged.Unsubscribe(OnEnabledChanged);
    }
}