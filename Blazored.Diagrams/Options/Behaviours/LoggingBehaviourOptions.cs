using Blazored.Diagrams.Behaviours;

namespace Blazored.Diagrams.Options.Behaviours;

/// <summary>
///     Options for <see cref="EventLoggingBehavior"/>
///     Disabled by default.
/// </summary>
public class LoggingBehaviourOptions : BaseBehaviourOptions
{
    /// <summary>
    /// Instantiates a new Logging behaviour options
    /// </summary>
    public LoggingBehaviourOptions()
    {
        IsEnabled = false;
    }
}