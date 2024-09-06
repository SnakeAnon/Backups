using Backups.Entities;
using Zio;

namespace Backups.Interfaces;

public interface IBackupTask
{
    Repository Repository { get; }
    Backup Backup { get; }
    IBackupObject AddBackupObject(UPath path);
    void RemoveBackupObject(IBackupObject backupObject);
    RestorePoint Run();
}