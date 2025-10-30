using Blazored.Diagrams.Extensions;
using Blazored.Diagrams.Interfaces;
using Blazored.Diagrams.Options.Behaviours;

namespace Blazored.Diagrams.Services.Diagrams;

public partial class DiagramService
{
    /// <summary>
    ///     Returns the center point of a model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public (int CenterX, int CenterY) GetCenterCoordinates<T>(T model) where T : ISize, IPosition
    {
        return (model.PositionX + model.Width / 2, model.PositionY + model.Height / 2);
    }


    /// <inheritdoc />
    public virtual void CenterIn<TContainer, TModel>(CenterInParameters<TContainer, TModel> parameters)
        where TModel : ISize, IPosition
        where TContainer : ISize, IPosition
    {
        var padding = parameters.Container is IPadding p ? p.Padding : 0;
        
        var innerPositionX = parameters.Container.PositionX + padding;
        var innerPositionY = parameters.Container.PositionY + padding;

        
        var innerWidth = parameters.Container.Width - (padding * 2);
        var innerHeight = parameters.Container.Height - (padding * 2);

        
        var targetCenterX = innerPositionX + innerWidth / 2;
        var targetCenterY = innerPositionY + innerHeight / 2;

        
        var newPositionX = targetCenterX - parameters.Container.Width / 2;
        var newPositionY = targetCenterY - parameters.Container.Height / 2;
        
        parameters.Model.SetPosition(newPositionX, newPositionY);
    }


    /// <inheritdoc />
    public virtual void CenterInViewport<TModel>(CenterInViewportParameters<TModel> parameters)
        where TModel : IPosition, ISize
    {
        // Diagram might not be rendered on screen yet.
        if (Diagram.Width == 0 || Diagram.Height == 0)
        {
            return;
        }

        var viewportCenterX = (Diagram.Width / 2 - Diagram.PanX) / Diagram.Zoom;
        var viewportCenterY = (Diagram.Height / 2 - Diagram.PanY) / Diagram.Zoom;

        var newPositionX = (int)(viewportCenterX - parameters.Model.Width / 2);
        var newPositionY = (int)(viewportCenterY - parameters.Model.Height / 2);

        parameters.Model.SetPosition(newPositionX, newPositionY);
    }
    
    /// <inheritdoc />
    public virtual void ZoomToModel<TModel>(ZoomToModelParameters<TModel> parameters)
        where TModel : IPosition, ISize
    {
        if (Diagram.Width <= 0 || Diagram.Height <= 0 || parameters.Model.Width <= 0 || parameters.Model.Height <= 0)
            return;

        var worldCenterX = parameters.Model.PositionX + parameters.Model.Width / 2;
        var worldCenterY = parameters.Model.PositionY + parameters.Model.Height / 2;

        var screenCenterX = Diagram.Width / 2.0;
        var screenCenterY = Diagram.Height / 2.0;

        var newPanX = screenCenterX  - worldCenterX;
        var newPanY = screenCenterY  - worldCenterY;

        Diagram.SetZoom(1);
        Diagram.SetPan((int)newPanX, (int)newPanY);
    }



    /// <inheritdoc />
    // TODO: This is bugged!
    public virtual void FitToScreen(FitToScreenParameters parameters)
    {
        bool VisiblePredicate(IVisible x) => parameters.IncludeInvisible || x.IsVisible;

        // Collect all bounds from nodes, groups, and ports that pass visibility filter
        var allBounds = Diagram.AllNodes.Where(VisiblePredicate).Select(x => x.GetBounds())
            .Concat(Diagram.AllGroups.Where(VisiblePredicate).Select(x => x.GetBounds()))
            .Concat(Diagram.AllPorts.Where(VisiblePredicate).Select(x => x.GetBounds())).ToList();

        if (allBounds.Count == 0)
            return;

        // Compute combined bounding box
        var minX = allBounds.Min(b => b.Left);
        var minY = allBounds.Min(b => b.Top);
        var maxX = allBounds.Max(b => b.Right);
        var maxY = allBounds.Max(b => b.Bottom);

        var totalWidth = Math.Abs(maxX - minX);
        var totalHeight = Math.Abs(maxY - minY);

        if (totalWidth <= 0 || totalHeight <= 0 || Diagram.Width <= 0 || Diagram.Height <= 0)
            return;

        // Add margin
        var requiredWidth = totalWidth + parameters.Margin * 2;
        var requiredHeight = totalHeight + parameters.Margin * 2;

        // Compute zoom ratios
        double zoomX = Diagram.Width / requiredWidth;
        double zoomY = Diagram.Height / requiredHeight;

        var newZoom = Math.Min(zoomX, zoomY);
        var zoomOptions = Behaviours.GetBehaviourOptions<ZoomBehaviourOptions>();

        newZoom = Math.Min(newZoom, zoomOptions.MaxZoom);

        // Compute world center and target pan
        var worldCenterX = minX + totalWidth / 2;
        var worldCenterY = minY + totalHeight / 2;

        var screenCenterX = Diagram.Width / 2;
        var screenCenterY = Diagram.Height / 2;

        var newPanX = screenCenterX - worldCenterX * newZoom;
        var newPanY = screenCenterY - worldCenterY * newZoom;

        Diagram.SetZoom(newZoom);
        Diagram.SetPan((int)newPanX, (int)newPanY);
    }
}
