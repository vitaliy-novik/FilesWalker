using System;
using System.IO;
using Business.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Core.Tests.Services
{
    [TestClass]
    public class FoldersServiceTests
    {
        private const string initialDirectory = "../../FoldersServiceTestFolder";
        private readonly string[] directories = { "1", "2", "3" };
        private readonly string[] files = { "1.txt", "2.html", "3.cs" };
        private FoldersService foldersService = new FoldersService();

        [TestInitialize]
        public void InitializeTest()
        {
            Directory.CreateDirectory(initialDirectory);
            DirectoryInfo directory = new DirectoryInfo(initialDirectory);
            foreach (string subDirectory in directories)
            {
                directory.CreateSubdirectory(subDirectory);
            }

            foreach (string file in files)
            {
                File.Create(Path.Combine(initialDirectory, file)).Close();
            }
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            Directory.Delete(initialDirectory, true);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void GetDirectories_InvalidPath_ExceptionIsThrown()
        {
            string path = "someInvalidPath";

            foldersService.GetDirectories(path);
        }

        [TestMethod]
        public void CreateFolder_ValidPath_DirectorieCreated()
        {
            string folderName = "newFolder";

            string path = Path.Combine(initialDirectory, folderName);
            
            foldersService.CreateFolder(path);

            DirectoryInfo directory = new DirectoryInfo(path);

            Assert.IsTrue(directory.Exists);
        }

        [TestMethod]
        public void CreateFile_ValidPath_FileCreated()
        {
            string fileName = "newFile";

            foldersService.CreateFile(initialDirectory, fileName);

            FileInfo fileInfo = new FileInfo(Path.Combine(initialDirectory, fileName));

            Assert.IsTrue(fileInfo.Exists);
        }

        [TestMethod]
        public void DeleteDirectorie_ValidPath_DirectorieDeleted()
        {
            string path = Path.Combine(initialDirectory, directories[0]);

            foldersService.DeleteFolder(path);

            DirectoryInfo directory = new DirectoryInfo(path);

            Assert.IsFalse(directory.Exists);
        }
    }
}
