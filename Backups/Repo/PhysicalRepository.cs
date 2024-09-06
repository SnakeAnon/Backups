using Backups.Entities;
using Zio;
using Zio.FileSystems;

namespace Backups.Repo;

public class PhysicalRepository : Repository
{
    public PhysicalRepository(UPath path)
        : base(path, new PhysicalFileSystem())
    {
    }
}