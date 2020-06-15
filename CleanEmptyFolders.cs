using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CleanEmptyFolders : EditorWindow
{
    private static string deletedFolders;

    [MenuItem("Window/Tools/Clean Empty Folders")]
    private static void Cleanup()
    {
        deletedFolders = string.Empty;

        var directoryInfo = new DirectoryInfo(Application.dataPath);

        var foldersToIgnore = directoryInfo.GetDirectories(".git", SearchOption.AllDirectories);
        var projectDirectories = directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories)
            .Where(x => ShouldScan(x.FullName, foldersToIgnore.Select(s => s.FullName)));

        foreach (var subDirectory in projectDirectories)
        {
            if (subDirectory.Exists)
            {
                ScanDirectory(subDirectory);
            }
        }

        Debug.Log("Deleted Folders:\n" + (deletedFolders.Length > 0 ? deletedFolders : "NONE"));
        AssetDatabase.Refresh();
    }

    private static bool ShouldScan(string folder, IEnumerable<string> ignored)
    {
        if (ignored.Any(folder.StartsWith))
        {
            return false;
        }

        return true;
    }

    private static string ScanDirectory(DirectoryInfo subDirectory)
    {
        Debug.Log("Scanning Directory: " + subDirectory.FullName);

        var filesInSubDirectory = subDirectory.GetFiles("*.*", SearchOption.AllDirectories);

        if (filesInSubDirectory.Length == 0 ||
            !filesInSubDirectory.Any(t => t.FullName.EndsWith(".meta") == false))
        {
            deletedFolders += subDirectory.FullName + "\n";
            subDirectory.Delete(true);
        }

        return deletedFolders;
    }
}