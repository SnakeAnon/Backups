using Backups.Entities;
using Zio;
using Zio.FileSystems;

namespace Backups.Repo;

public class MemoryRepository : Repository
{
    public MemoryRepository(UPath path)
        : base(path, new MemoryFileSystem())
    {
    }
}