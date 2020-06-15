using System.Linq;
using NUnit.Framework;

namespace FolderCleaner.Tests
{
    public class EmptyFoldersTest
    {
        [Test]
        public void EnsureThereAreNoEmptyDirectories()
        {
            var emptyDirectories =
                CleanEmptyFolders.GetEmptyDirectories().Select(CleanEmptyFolders.GetRootDirectoryName);
            Assert.IsEmpty(emptyDirectories);
        }
    }
}