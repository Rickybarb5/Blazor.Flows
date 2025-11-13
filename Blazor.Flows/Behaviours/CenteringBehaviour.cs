using Blazor.Flows.Events;
using Blazor.Flows.Interfaces;
using Blazor.Flows.Options.Behaviours;
using Blazor.Flows.Services.Diagrams;

namespace Blazor.Flows.Behaviours;

/// <summary>
/// This behaviour centers a model in the viewport if it's position is 0,0.
/// It also centers models within other models if the position is 0,0.
/// </summary>
public class CenteringBehaviour : BaseBehaviour
{
    private readonly IDiagramService _diagramService;
    CenteringBehaviourOptions _behaviourOptions;

    /// <summary>
    /// Instantiates a new <see cref="CenteringBehaviour"/>
    /// </summary>
    /// <param name="service"><see cref="IDiagramService"/>.</param>
    public CenteringBehaviour(IDiagramService service)
    {
        _diagramService = service;
        _behaviourOptions = service.Behaviours.GetBehaviourOptions<CenteringBehaviourOptions>();
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
            _diagramService.Events.SubscribeTo<NodeAddedEvent>(Handle),
            _diagramService.Events.SubscribeTo<GroupAddedEvent>(Handle),
            _diagramService.Events.SubscribeTo<NodeAddedToGroupEvent>(Handle),
            _diagramService.Events.SubscribeTo<GroupAddedToGroupEvent>(Handle),
        ];
    }

    private void Handle(GroupAddedToGroupEvent obj)
    {
        CenterInModel(obj.AddedGroup, obj.ParentModel);
    }

    private void Handle(NodeAddedToGroupEvent obj)
    {
        CenterInModel(obj.Node, obj.Model);
    }

    private void Handle(GroupAddedEvent obj)
    {
        CenterInViewPort(obj.Model);
    }

    private void Handle(NodeAddedEvent obj)
    {
        CenterInViewPort(obj.Model);
    }

    private void CenterInViewPort<T>(T model)
        where T: ISize, IPosition
    {
        if (model.PositionX == 0 && model.PositionY == 0)
        {
            _diagramService.CenterInViewport(new CenterInViewportParameters<T>(model));
        }
    }

    private void CenterInModel<TAdded, TParent>(TAdded added, TParent parent)
        where TAdded : ISize, IPosition
        where TParent : ISize, IPosition
    {
        if (added.PositionX == 0 && added.PositionY == 0)
        {
            var padding = parent is IPadding pad ? pad.Padding : 0; 
            _diagramService.CenterIn(new CenterInParameters<TAdded, TParent>(added, parent));
        }
    }
    
    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        _behaviourOptions.OnEnabledChanged.Unsubscribe(OnEnabledChanged);
    }
}