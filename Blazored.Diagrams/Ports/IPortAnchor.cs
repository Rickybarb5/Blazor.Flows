using Blazored.Diagrams.Interfaces;

namespace Blazored.Diagrams.Ports;

public interface IPortAnchor
{
    IPort Port { get; init; }
    int PositionX { get; set; }
    int PositionY { get; set; }
}