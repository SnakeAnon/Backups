using Backups.Entities;
using Backups.Interfaces;
using Backups.Objects;
using Backups.Tools;
using Zio;

namespace Backups.Facade;

public class BackupTask : IBackupTask
{
    private readonly List<IBackupObject> _backupObjects;
    public BackupTask(IAlgorithm algorithm, Repository repository)
    {
        _backupObjects = new List<IBackupObject>();
        Algorithm = algorithm;
        Repository = repository;
    }

    public IEnumerable<IBackupObject> Objects => _backupObjects;
    public IAlgorithm Algorithm { get; }
    public Repository Repository { get; }
    public Backup Backup { get; } = new ();

    public IBackupObject AddBackupObject(UPath path)
    {
        if (Repository.FileSystem.DirectoryExists(path))
        {
            var folder = new BackupFolder(path);
            _backupObjects.Add(folder);
            return folder;
        }
        else if (Repository.FileSystem.FileExists(path))
        {
            var file = new BackupFile(path);
            _backupObjects.Add(file);
            return file;
        }

        throw new Exception("wrong path");
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        ContainerTool.CheckForNonExist(_backupObjects, backupObject);
        _backupObjects.Remove(backupObject);
    }

    public RestorePoint Run()
    {
        var point = new RestorePoint(Algorithm.Run(_backupObjects, Repository).ToList());
        Backup.AddPoint(point);
        return point;
    }
}