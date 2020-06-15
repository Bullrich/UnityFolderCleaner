using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CleanEmptyFolders : EditorWindow
{
    [MenuItem("Window/Tools/Clean Empty Folders")]
    private static void Cleanup()
    {
        var emptyDirectories = GetEmptyDirectories();

        var emptyDirectoryNames = emptyDirectories.Select(ed => ed.FullName);

        foreach (var directory in emptyDirectories)
        {
            if (directory.Exists)
            {
                directory.Delete(true);
            }
        }

        Debug.Log("Deleted Folders:\n" +
                  (emptyDirectories.Count > 0 ? string.Join("\n", emptyDirectoryNames) : "NONE"));
        AssetDatabase.Refresh();
    }

    private static IReadOnlyList<DirectoryInfo> GetEmptyDirectories()
    {
        var directoriesToDelete = new List<DirectoryInfo>();

        var directoryInfo = new DirectoryInfo(Application.dataPath);

        var foldersToIgnore = directoryInfo.GetDirectories(".git", SearchOption.AllDirectories);
        var projectDirectories = directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories)
            .Where(x => ShouldScan(x.FullName, foldersToIgnore.Select(s => s.FullName)));

        Debug.Log("Directories to scan:\n" + string.Join("\n", projectDirectories.Select(pd => pd.FullName)));

        foreach (var subDirectory in projectDirectories)
        {
            if (IsDirectoryEmpty(subDirectory))
            {
                directoriesToDelete.Add(subDirectory);
            }
        }

        // Order them to ensure that we delete them from nested to root
        return directoriesToDelete.OrderByDescending(dtd => dtd.FullName.Length).ToArray();
    }

    private static bool ShouldScan(string folder, IEnumerable<string> ignored)
    {
        if (ignored.Any(folder.StartsWith))
        {
            return false;
        }

        return true;
    }

    private static bool IsDirectoryEmpty(DirectoryInfo subDirectory)
    {
        if (!subDirectory.Exists)
        {
            return false;
        }

        var filesInSubDirectory = subDirectory.GetFiles("*.*", SearchOption.AllDirectories);

        if (filesInSubDirectory.Length == 0 || filesInSubDirectory.All(t => t.FullName.EndsWith(".meta")))
        {
            return true;
        }

        return false;
    }
}