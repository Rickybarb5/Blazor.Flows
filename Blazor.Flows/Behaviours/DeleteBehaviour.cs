using Blazor.Flows.Events;
using Blazor.Flows.Options.Behaviours;
using Blazor.Flows.Services.Diagrams;

namespace Blazor.Flows.Behaviours;

/// <summary>
///     Performs cleanup actions when something is deleted from the diagram.
/// </summary>
public class DeleteBehaviour : BaseBehaviour
{
    private readonly IDiagramService _service;
    private readonly DeleteBehaviourOptions _behaviourOptions;
    
    private void SubscribeToEvents()
    {
        Subscriptions =
        [
            _service.Events.SubscribeTo<NodeRemovedEvent>(e => CleanupDependencies(e.Model)),
            _service.Events.SubscribeTo<LayerRemovedEvent>(e => CleanupDependencies(e.Model)),
            _service.Events.SubscribeTo<GroupRemovedEvent>(e => CleanupDependencies(e.Model)),
            _service.Events.SubscribeTo<PortRemovedEvent>(e => CleanupDependencies(e.Model)),
            _service.Events.SubscribeTo<LinkRemovedEvent>(e => CleanupDependencies(e.Model)),
        ];
    }

    /// <summary>
    /// Instantiates a new <see cref="DeleteBehaviour"/>
    /// </summary>
    /// <param name="service"></param>
    public DeleteBehaviour(IDiagramService service)
    {
        _service = service;
        _behaviourOptions = _service.Behaviours.GetBehaviourOptions<DeleteBehaviourOptions>()!;
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

    private void CleanupDependencies(IDisposable obj)
    {
        obj.Dispose();
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        _behaviourOptions.OnEnabledChanged.Unsubscribe(OnEnabledChanged);
    }
}