using Blazored.Diagrams.Behaviours;
using Blazored.Diagrams.Interfaces;

namespace Blazored.Diagrams.Services.Behaviours;

public interface IBehaviourContainer : IDisposable
{
    /// <summary>
    /// Register a behaviour.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <typeparam name="TBehaviour"></typeparam>
    /// <returns></returns>
    IBehaviourContainer RegisterBehaviour<TBehaviour>(TBehaviour behaviour)
        where TBehaviour : IBehaviour;

    /// <summary>
    /// Register a behaviour and it's options.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="options"></param>
    /// <typeparam name="TBehaviour"></typeparam>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    /// <returns></returns>
    IBehaviourContainer Register<TBehaviour, TBehaviourOptions>(TBehaviour behaviour,
        TBehaviourOptions options)
        where TBehaviour : IBehaviour
        where TBehaviourOptions : IBehaviourOptions;

    /// <summary>
    /// Register the behaviour and options using a function.
    /// </summary>
    /// <param name="behaviourConfiguration"></param>
    /// <param name="optionConfiguration"></param>
    /// <typeparam name="TBehaviour"></typeparam>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    /// <returns></returns>
    public IBehaviourContainer Register<TBehaviour, TBehaviourOptions>(
        Func<TBehaviour> behaviourConfiguration,
        Func<TBehaviourOptions> optionConfiguration)
        where TBehaviour : IBehaviour
        where TBehaviourOptions : IBehaviourOptions;

    /// <summary>
    /// Sets the <see cref="IBehaviourOptions.IsEnabled"/> flag to true.
    /// </summary>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    void EnableBehaviour<TBehaviourOptions>()
        where TBehaviourOptions : IBehaviourOptions;

    /// <summary>
    /// Sets the <see cref="IBehaviourOptions.IsEnabled"/> flag to false.
    /// </summary>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    void DisableBehaviour<TBehaviourOptions>()
        where TBehaviourOptions : IBehaviourOptions;

    /// <summary>
    /// Gets the behaviour options by type.
    /// </summary>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    /// <returns></returns>
    TBehaviourOptions GetBehaviourOptions<TBehaviourOptions>()
        where TBehaviourOptions : IBehaviourOptions;

    /// <summary>
    /// Register behaviour options.
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="TBehaviourOptions"></typeparam>
    /// <returns></returns>
    IBehaviourContainer RegisterBehaviourOptions<TBehaviourOptions>(TBehaviourOptions options)
        where TBehaviourOptions : IBehaviourOptions;
}