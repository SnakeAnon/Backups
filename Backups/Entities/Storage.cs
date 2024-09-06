using Zio;

namespace Backups.Entities;

public class Storage
{
    private readonly UPath _path;

    public Storage(UPath path)
    {
        _path = path;
    }

    public UPath Path => _path;
}