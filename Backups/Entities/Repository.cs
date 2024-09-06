using System.IO.Compression;
using Backups.Interfaces;
using Backups.Tools;
using Zio;

namespace Backups.Entities;

public abstract class Repository
{
    private readonly UPath _path;
    private readonly IFileSystem _fileSystem;
    private int _restorePointsCounter;

    protected Repository(UPath path, IFileSystem fileSystem)
    {
        _path = path;
        _fileSystem = fileSystem;
        _restorePointsCounter = 0;
    }

    public UPath RepoPath => _path;
    public IFileSystem FileSystem => _fileSystem;

    public string GetStorageName()
    {
        return $"Storage{_restorePointsCounter}";
    }

    public void IncreaseCounter()
    {
        _restorePointsCounter++;
    }

    public Storage Archive(List<IBackupObject> backupObjects)
    {
        var path = UPath.Combine(RepoPath.FullName, $"RestorePoint{_restorePointsCounter++}");
        FileSystem.CreateDirectory(path);
        var storage = UPath.Combine(path.FullName, GetStorageName());
        using Stream stream = FileSystem.OpenFile(storage.FullName, FileMode.Create, FileAccess.ReadWrite);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new Visitor(FileSystem, archive);

        foreach (IBackupObject backupObject in backupObjects)
        {
            backupObject.Accept(visitor);
        }

        return new Storage(storage);
    }

    public Storage Archive(IBackupObject backupObject, int storageNumber)
    {
        if (backupObject == null)
        {
            throw new NullReferenceException();
        }

        var path = UPath.Combine(RepoPath.FullName, $"RestorePoint{_restorePointsCounter}");
        var storage = UPath.Combine(path.FullName, $"{storageNumber}");
        Stream stream = FileSystem.OpenFile(storage.FullName, FileMode.Create, FileAccess.ReadWrite);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        using Stream fileStream = FileSystem.OpenFile(backupObject.Path.FullName, FileMode.Open, FileAccess.ReadWrite);
        return new Storage(storage);
    }
}