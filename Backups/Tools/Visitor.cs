using System.IO.Compression;
using Backups.Interfaces;
using Backups.Objects;
using Zio;

namespace Backups.Tools;

public class Visitor
{
    private Stack<ZipArchive> _zipArchives;
    private IFileSystem _fileSystem;
    public Visitor(IFileSystem fileSystem, ZipArchive archive)
    {
        _fileSystem = fileSystem;
        _zipArchives = new Stack<ZipArchive>();
        _zipArchives.Push(archive);
    }

    public void Visit(BackupFile file)
    {
        if (file == null)
        {
            throw new NullReferenceException();
        }

        ZipArchive archive = _zipArchives.Peek();
        using Stream stream = _fileSystem.OpenFile(file.Path.FullName, FileMode.Open, FileAccess.ReadWrite);
        string fileName = file.Path.FullName;
        string newFileName = fileName[(fileName.LastIndexOf(UPath.DirectorySeparator) + 1) ..];
        using Stream archiveEntry = archive.CreateEntry(newFileName).Open();
        stream.CopyTo(archiveEntry);
    }

    public void Visit(BackupFolder folder)
    {
        if (folder == null)
        {
            throw new NullReferenceException();
        }

        ZipArchive archive = _zipArchives.Peek();
        string folderName = folder.Path.FullName;
        using Stream archiveEntry =
            archive.CreateEntry(folderName[(folderName.LastIndexOf(UPath.DirectorySeparator) + 1) ..] + ".zip").Open();
        _zipArchives.Push(archive);
        foreach (IBackupObject backupObject in folder.Objects.ToList())
        {
            backupObject.Accept(this);
        }

        _zipArchives.Pop();
    }
}