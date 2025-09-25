using System.Text.Json.Serialization;

namespace Blazored.Diagrams.Ports;

/// <summary>
/// A port anchor defines where the port is positioned.
/// </summary>
public class PortAnchor : IPortAnchor
{
    private int _positionX;
    private int _positionY;
    
    /// <summary>
    /// The port to which this anchor belongs to.
    /// </summary>]
    [JsonIgnore]
    public required IPort Port { get; init; }

    /// <summary>
    /// X position of the anchor.
    /// </summary>
    public virtual int PositionX
    {
        get => _positionX + OffsetX; 
        set => _positionX = value;
    }

    /// <summary>
    /// Y position of the anchor.
    /// </summary>
    public virtual int PositionY
    {
        get => _positionY + OffsetY;
        set => _positionY = value;
    }

    /// <summary>
    /// Offset in pixels for the X position.
    /// </summary>
    public virtual int OffsetX { get; set; }

    /// <summary>
    /// Offset in pixels for Y position.
    /// </summary>
    public virtual int OffsetY { get; set; }

  
}