using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algo;

public class SingleAlgo : IAlgorithm
{
    public IEnumerable<Storage> Run(List<IBackupObject> backupObjects, Repository repository)
    {
        if (backupObjects == null)
        {
            throw new NullReferenceException();
        }

        if (repository == null)
        {
            throw new NullReferenceException();
        }

        return new List<Storage> { repository.Archive(backupObjects) };
    }
}