using Backups.Entities;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    IEnumerable<Storage> Run(List<IBackupObject> backupObjects, Repository repository);
}