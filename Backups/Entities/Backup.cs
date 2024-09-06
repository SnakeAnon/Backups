using Backups.Tools;

namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public void AddPoint(RestorePoint point)
    {
        if (point == null)
        {
            throw new NullReferenceException();
        }

        ContainerTool.CheckForRepeat(_restorePoints, point);

        _restorePoints.Add(point);
    }

    public void DeletePoint(RestorePoint point)
    {
        if (point == null)
        {
            throw new NullReferenceException();
        }

        ContainerTool.CheckForNonExist(_restorePoints, point);

        _restorePoints.Remove(point);
    }
}