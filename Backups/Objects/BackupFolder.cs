using Backups.Interfaces;
using Backups.Tools;
using Zio;

namespace Backups.Objects;

public class BackupFolder : IBackupObject
{
    private readonly List<IBackupObject> _objects;
    private UPath _path;

    public BackupFolder(UPath path)
    {
        _path = path;
        _objects = new List<IBackupObject>();
    }

    public UPath Path => _path;
    public IEnumerable<IBackupObject> Objects => _objects;

    public void Accept(Visitor visitor)
    {
        if (visitor == null)
        {
            throw new NullReferenceException();
        }

        visitor.Visit(this);
    }
}