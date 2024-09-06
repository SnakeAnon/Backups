using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algo;

public class SplitAlgo : IAlgorithm
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

        int counter = 1;
        IEnumerable<Storage> list = backupObjects.Select(backupObject => repository.Archive(backupObject, counter++));
        repository.IncreaseCounter();
        return list;
    }
}