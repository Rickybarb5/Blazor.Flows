using Blazored.Diagrams.Diagrams;
using Blazored.Diagrams.Interfaces;
using Blazored.Diagrams.Services.Behaviours;
using Blazored.Diagrams.Services.Events;

namespace Blazored.Diagrams.Services.Diagrams;

/// <summary>
/// Manages diagram events, behaviours and child models.
/// </summary>
public interface IDiagramService : IDisposable
{
    /// <summary>
    /// Diagram instance.
    /// </summary>
    IDiagram Diagram { get; }

    /// <summary>
    /// Allows access to event subscription and publish.
    /// </summary>
    IEventAggregator Events { get; set; }
    
    /// <summary>
    /// Allows behaviour customization.
    /// </summary>
    IBehaviourContainer Behaviours { get; set; }
    
    /// <summary>
    /// Allows diagram serialization/deserialization.
    /// </summary>
    ISerializationContainer Storage { get; set; }
    
    /// <summary>
    /// Allows adding models to the diagram.
    /// </summary>
    public IAddContainer Add { get; set; }
    
    /// <summary>
    /// Allows deleting models from the diagram.
    /// </summary>
    public IDeleteContainer Remove { get; set; }

    /// <summary>
    /// Customize diagram options.
    /// </summary>
    IOptionsContainer Options { get; set; }

    /// <summary>
    /// Replaces the diagram instance with another.
    /// </summary>
    /// <param name="diagram">A diagram instance</param>
    void UseDiagram(IDiagram diagram);

    /// <summary>
    ///     Returns the center point of a model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    (int CenterX, int CenterY) GetCenterCoordinates<T>(T model) where T : ISize, IPosition;


    /// <summary>
    /// Centers a model in another model
    /// </summary>
    /// <param name="toCenter">Model that will change position.</param>
    /// <param name="container">Model where the model will be centered to. </param>
    /// <typeparam name="TModel">Any diagram model that implements <see cref="ISize"/>, <see cref="IPosition"/>.</typeparam>
    /// <typeparam name="TContainer">Any diagram model that implements <see cref="ISize"/>, <see cref="IPosition"/></typeparam>
    void CenterIn<TContainer, TModel>(
        TModel toCenter,
        TContainer container)
        where TModel : ISize, IPosition
        where TContainer : ISize, IPosition;

    /// <summary>
    ///     Changes the position of a model to be in the center of the viewport, accounting for pan and zoom.
    /// </summary>
    /// <param name="toCenter">Model to be centered.</param>
    /// <typeparam name="TModel">Type of the model to be centered.</typeparam>
    public void CenterInViewport<TModel>(TModel toCenter)
        where TModel : IPosition, ISize;

    /// <summary>
    /// Changes the pan and zoom to fit all diagram components on screen (if zoom allows it)
    /// </summary>
    /// <param name="parameters">Customization options.</param>
    void FitToScreen(DiagramService.FitToScreenParameters parameters);
}