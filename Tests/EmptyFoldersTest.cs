using NUnit.Framework;

namespace FolderCleaner.Tests
{
    public class EmptyFoldersTest
    {
        [Test]
        public void EnsureAllProjectDirectoriesAreEmpty()
        {
            Assert.IsEmpty(CleanEmptyFolders.GetEmptyDirectories());
        }
    }
}