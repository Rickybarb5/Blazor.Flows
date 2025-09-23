using Blazored.Diagrams.Behaviours;
using Blazored.Diagrams.Links;

namespace Blazored.Diagrams.Options.Behaviours;

/// <summary>
/// Options for <see cref="DrawLinkBehavior"/>
/// </summary>
public class DrawLinkOptions : BehaviourOptionsBase
{
    /// <summary>
    ///     Type of link that the behaviour will create.
    /// Default is <see cref="Link"/>
    /// </summary>
    public Type LinkType = typeof(Link);

}