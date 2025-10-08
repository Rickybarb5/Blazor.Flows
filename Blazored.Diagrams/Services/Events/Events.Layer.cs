using Blazored.Diagrams.Groups;
using Blazored.Diagrams.Layers;
using Blazored.Diagrams.Nodes;

namespace Blazored.Diagrams.Services.Events;

// Layer Events

/// <summary>
///     Event triggered when a new layer will start to be used.
/// </summary>
/// <param name="OldLayer">Previous layer that was being used.</param>
/// <param name="NewLayer">New layer that will be used.</param>
public record CurrentLayerChangedEvent(ILayer OldLayer, ILayer NewLayer) : IEvent;

/// <summary>
///     Base layer event.
/// </summary>
/// <param name="Model">Layer that triggered the event</param>
public record LayerEvent(ILayer Model) : ModelEventBase<ILayer>(Model);

/// <summary>
///     Event triggered when a layer is added to the diagram.
/// </summary>
/// <param name="Model">Layer that triggered the event</param>
public record LayerAddedEvent(ILayer Model) : LayerEvent(Model);

/// <summary>
///     Event triggered when a layer is removed from the diagram.
/// </summary>
/// <param name="Model">Layer that triggered the event</param>
public record LayerRemovedEvent(ILayer Model) : LayerEvent(Model);

/// <summary>
///     Event triggered when the visibility of a layer changes.
/// </summary>
/// <param name="Model">Layer that triggered the event</param>
public record LayerVisibilityChangedEvent(ILayer Model) : LayerEvent(Model);

/// <summary>
///     Event triggered when a redraw is requested for a layer.
/// </summary>
/// <param name="Model">Layer that requested the redraw.</param>
public record LayerRedrawEvent(ILayer Model) : LayerEvent(Model);

/// <summary>
///     Event triggered when a node is added to a group.
/// </summary>
/// <param name="Model"></param>
/// <param name="Node"></param>
public record NodeAddedToLayerEvent(ILayer Model, INode Node) : LayerEvent(Model);

/// <summary>
/// Event triggered when a node is removed from a layer.
/// </summary>
/// <param name="Model"></param>
/// <param name="Node"></param>
public record NodeRemovedFromLayerEvent(ILayer Model, INode Node) : LayerEvent(Model);

/// <summary>
/// Event triggered when a group is added to another group.
/// </summary>
/// <param name="Model"></param>
/// <param name="AddedGroup"></param>
public record GroupAddedToLayerEvent(ILayer Model, IGroup AddedGroup) : LayerEvent(Model);

/// <summary>
///     Event triggered when a group is removed from the layer.
/// </summary>
/// <param name="Model"></param>
/// <param name="RemovedGroup"></param>
public record GroupRemovedFromLayerEvent(ILayer Model, IGroup RemovedGroup) : LayerEvent(Model);