namespace Blazored.Diagrams.Ports;

/// <summary>
///  Useful method for <see cref="IPortAnchor"/>
/// </summary>
public static class PortAnchorExtensions
{
    /// <summary>
    /// Centers the port in the middle of its parent.
    /// </summary>
    public static (int,int) SetPositionToCenterParent(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX + anchor.Port.Parent.Width / 2;
        var positionY = anchor.Port.Parent.PositionY + anchor.Port.Parent.Height / 2;

        return (positionX, positionY);
    }

    /// <summary>
    /// Centers the port at the top left of its parent.
    /// </summary>
    public static (int,int) SetPositionToTopLeft(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX;
        var positionY = anchor.Port.Parent.PositionY;

        return (positionX, positionY);
    }

    /// <summary>
    /// Centers the port at the top right of its parent.
    /// </summary>
    public static (int,int) SetPositionToTopRight(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX + anchor.Port.Parent.Width;
        var positionY = anchor.Port.Parent.PositionY;

        return (positionX, positionY);
    }

    /// <summary>
    /// Centers the port at the top of its parent.
    /// </summary>
    /// <returns></returns>
    public static (int,int) SetPositionToTopCenter(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX + anchor.Port.Parent.Width / 2;
        var positionY = anchor.Port.Parent.PositionY;

        return (positionX, positionY);
    }

    /// <summary>
    /// Centers the port at the bottom left of its parent.
    /// </summary>
    public static (int,int) SetPositionToBottomLeft(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX;
        var positionY = anchor.Port.Parent.PositionY + anchor.Port.Parent.Height;

        return (positionX, positionY);
    }

    /// <summary>
    /// Centers the port at the bottom right of its parent.
    /// </summary>
    public static (int,int) SetPositionToBottomRight(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX + anchor.Port.Parent.Width;
        var positionY = anchor.Port.Parent.PositionY + anchor.Port.Parent.Height;

        return (positionX, positionY);
    }


    /// <summary>
    /// Centers the port at the bottom of its parent.
    /// </summary>
    public static (int,int) SetPositionToBottomCenter(this IPortAnchor anchor)
    {
        var positionX = anchor.Port.Parent.PositionX + anchor.Port.Parent.Width / 2;
        var positionY = anchor.Port.Parent.PositionY + anchor.Port.Parent.Height;

        return (positionX, positionY);
    }}