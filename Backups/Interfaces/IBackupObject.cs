using Backups.Tools;
using Zio;

namespace Backups.Interfaces;

public interface IBackupObject
{
    UPath Path { get; }
    void Accept(Visitor visitor);
}