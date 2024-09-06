using Backups.Interfaces;
using Backups.Tools;
using Zio;

namespace Backups.Objects;

public class BackupFile : IBackupObject
{
    private UPath _path;

    public BackupFile(UPath path)
    {
        _path = path;
    }

    public UPath Path => _path;

    public void Accept(Visitor visitor)
    {
        if (visitor == null)
        {
            throw new NullReferenceException();
        }

        visitor.Visit(this);
    }
}