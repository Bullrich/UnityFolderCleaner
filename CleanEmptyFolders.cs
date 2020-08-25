using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FolderCleaner
{
    public class CleanEmptyFolders : EditorWindow
    {
        [MenuItem("Window/Tools/Clean Empty Folders")]
        private static void Cleanup()
        {
            var emptyDirectories = GetEmptyDirectories();

            foreach (var directory in emptyDirectories)
            {
                if (directory.Exists)
                {
                    directory.Delete(true);
                }
            }

            if (emptyDirectories.Count > 0)
            {
                var emptyDirectoryNames = emptyDirectories.Select(GetRootDirectoryName);
                Debug.Log("Deleted Folders:\n" + string.Join("\n", emptyDirectoryNames));
            }
            else
            {
                Debug.Log("No empty directories found");
            }

            AssetDatabase.Refresh();
        }

        public static IReadOnlyList<DirectoryInfo> GetEmptyDirectories()
        {
            var directoryInfo = new DirectoryInfo(Application.dataPath);

            var foldersToIgnore = directoryInfo.GetDirectories(".git", SearchOption.AllDirectories);
            var projectDirectories = directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories)
                .Where(x => ShouldScan(x.FullName, foldersToIgnore.Select(s => s.FullName)));

#if FOLDER_CLEANER_VERBOSE
            Debug.Log("Directories to scan:\n" + string.Join("\n",
                projectDirectories.Select(GetRootDirectoryName)));
#else
            Debug.Log($"Scanning {projectDirectories.Count()} directories");
#endif

            var directoriesToDelete = projectDirectories.Where(IsDirectoryEmpty);

            // Order them to ensure that we delete them from nested to root
            return directoriesToDelete.OrderByDescending(dtd => dtd.FullName.Length).ToArray();
        }

        public static string GetRootDirectoryName(DirectoryInfo dirInfo)
        {
            return dirInfo.FullName.Substring(dirInfo.FullName.IndexOf("Assets", StringComparison.Ordinal));
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
}