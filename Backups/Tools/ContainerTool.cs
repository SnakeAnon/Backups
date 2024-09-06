using System.Collections.ObjectModel;

namespace Backups.Tools;

public static class ContainerTool
{
    public static void CheckForRepeat<T>(List<T> collection, T t)
    {
        if (collection.Contains(t))
        {
            throw new Exception("Element already in collection");
        }
    }

    public static void CheckForNonExist<T>(List<T> collection, T t)
    {
        if (!collection.Contains(t))
        {
            throw new Exception("There is no such element in collection");
        }
    }
}