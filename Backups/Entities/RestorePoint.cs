using Backups.Tools;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<Storage> _storages;

    public RestorePoint()
    {
        _storages = new List<Storage>();
    }

    public RestorePoint(List<Storage> storages)
    {
        _storages = storages;
    }

    public void AddStorage(Storage storage)
    {
        if (storage == null)
        {
            throw new NullReferenceException();
        }

        ContainerTool.CheckForRepeat(_storages, storage);
        _storages.Add(storage);
    }

    public void DeleteStorage(Storage storage)
    {
        if (storage == null)
        {
            throw new NullReferenceException();
        }

        ContainerTool.CheckForNonExist(_storages, storage);
        _storages.Remove(storage);
    }
}