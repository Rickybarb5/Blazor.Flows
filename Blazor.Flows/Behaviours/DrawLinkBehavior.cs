using Blazor.Flows.Events;
using Blazor.Flows.Extensions;
using Blazor.Flows.Interfaces;
using Blazor.Flows.Links;
using Blazor.Flows.Options.Behaviours;
using Blazor.Flows.Ports;
using Blazor.Flows.Services.Diagrams;

namespace Blazor.Flows.Behaviours;

/// <summary>
///     Behaviour for creating a link when a port is clicked.
/// </summary>
public class DrawLinkBehavior : BaseBehaviour
{
    private readonly IDiagramService _service;
    private readonly DrawLinkBehaviourOptions _behaviourOptions;
    private int _initialClickX;
    private int _initialClickY;
    private bool _isCreatingLink;

    private IPort? _sourcePort;
    private IPort? _targetPort;

    /// <summary>
    ///     Initializes a new instance of <see cref="DrawLinkBehavior"/>
    /// </summary>
    /// <param name="service"></param>
    public DrawLinkBehavior(IDiagramService service)
    {
        _service = service;
        _behaviourOptions = _service.Behaviours.GetBehaviourOptions<DrawLinkBehaviourOptions>();
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

    private ILink? Link { get; set; }
    
    private void SubscribeToEvents()
    {
        Subscriptions =
        [
            _service.Events.SubscribeTo<PortPointerDownEvent>(StartLinkCreation),
            _service.Events.SubscribeTo<PortPointerUpEvent>(CreateLink),
            _service.Events.SubscribeTo<DiagramPointerMoveEvent>(UpdateTargetPosition),
            _service.Events.SubscribeTo<DiagramPointerUpEvent>(OnDiagramPointerUp),
        ];
    }

    /// <summary>
    ///     1-When pointer is down on a port, start link creation.
    /// </summary>
    /// <param name="e"></param>
    private void StartLinkCreation(PortPointerDownEvent e)
    {
        Link = null;
        _sourcePort = null;
        _targetPort = null;
        _isCreatingLink = false;
        _initialClickX = 0;
        _initialClickY = 0;
        ClearUnboundedLinks();
        if (!e.Model.CanCreateLink()) return;
        _isCreatingLink = true;
        _sourcePort = e.Model;
        _initialClickX = (int)e.Args.ClientX;
        _initialClickY = (int)e.Args.ClientY;

        Link = AddLink(_sourcePort, _behaviourOptions.LinkType);
        _service.Events.Publish(new DrawLinkStartEvent(Link));
        var startCoordinates = _service.GetCenterCoordinates(_sourcePort);
        Link.SetTargetPosition(startCoordinates.CenterX, startCoordinates.CenterY);
    }

    private static ILink AddLink(ILinkContainer sourcePort, Type linkType)
    {

        // Create an instance of linkType using reflection
        var link = (ILink?)Activator.CreateInstance(linkType);
        if (link is null)
        {
            throw new InvalidOperationException($"Link couldn't be created with component type: {linkType.Name}");
        }

        sourcePort.OutgoingLinks.AddInternal(link);
        return link;
    }

    /// <summary>
    ///     2-Pointer is moving.
    /// </summary>
    /// <param name="e"></param>
    private void UpdateTargetPosition(DiagramPointerMoveEvent e)
    {
        _isCreatingLink = e.Args.Buttons == 1 && _sourcePort is not null;

        if (_isCreatingLink && Link is not null)
        {
            // Calculate new target position based on the movement from the initial click
            var newX = (int)(_sourcePort!.PositionX + (e.Args.ClientX - _initialClickX) / _service.Diagram.Zoom);
            var newY = (int)(_sourcePort.PositionY + (e.Args.ClientY - _initialClickY) / _service.Diagram.Zoom);

            Link.SetTargetPosition(newX, newY);
        }
    }

    /// <summary>
    ///     3-When the pointer is released on top of a port.
    /// </summary>
    /// <param name="e"></param>
    private void CreateLink(PortPointerUpEvent e)
    {
        if (_isCreatingLink && _sourcePort is not null && Link is not null)
        {
            _targetPort = e.Model;
            OnDiagramPointerUp(null);
        }

        ClearUnboundedLinks();
    }

    /// <summary>
    ///     4-When the mouse is released.
    /// </summary>
    /// <param name="e"></param>
    private void OnDiagramPointerUp(DiagramPointerUpEvent? e)
    {
        if (IsConnectionPossible())
        {
            _targetPort!.IncomingLinks.AddInternal(Link!);
            _service.Events.Publish(new DrawLinkCreatedEvent(Link!));
        }
        else
        {
            if (_sourcePort is not null && Link is not null)
            {
                if (Link is not null)
                {
                    _sourcePort.OutgoingLinks.RemoveInternal(Link);
                }
                _service.Events.Publish(new DrawLinkCancelledEvent(Link!));
            }
        }

        //Port gets selected when the pointer goes down.
        if (_sourcePort != null) _sourcePort.IsSelected = false;
        ClearUnboundedLinks();

        Link = null;
        _sourcePort = null;
        _targetPort = null;
        _isCreatingLink = false;
        _initialClickX = 0;
        _initialClickY = 0;
    }

    private bool IsConnectionPossible()
    {
        return _targetPort is not null &&
               _sourcePort is not null &&
               Link is not null &&
               _sourcePort.CanConnectTo(_targetPort) &&
               _targetPort.CanConnectTo(_sourcePort);
    }

    private void ClearUnboundedLinks()
    {
        _service.Diagram.AllLinks
            .Where(x => x.TargetPort is null)
            .ForEach(l => l.Dispose());
    }
    
    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        _behaviourOptions.OnEnabledChanged.Unsubscribe(OnEnabledChanged);
    }
}