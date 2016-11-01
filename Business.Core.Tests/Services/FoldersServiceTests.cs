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
        public void CreateFolder_ValidPath_DirectoryCreated()
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
        public void DeleteDirectorie_ValidPath_DirectoryDeleted()
        {
            string path = Path.Combine(initialDirectory, directories[0]);

            foldersService.DeleteFolder(path);

            DirectoryInfo directory = new DirectoryInfo(path);

            Assert.IsFalse(directory.Exists);
        }

        [TestMethod]
        public void DeleteFile_ValidPath_FileDeleted()
        {
            string path = Path.Combine(initialDirectory, files[0]);

            foldersService.DeleteFile(path);

            FileInfo fileInfo = new FileInfo(path);

            Assert.IsFalse(fileInfo.Exists);
        }

        [TestMethod]
        public void CopyTo_ValidFilePath_FileCopied()
        {
            string path = initialDirectory;
            string source = files[0];
            string target = directories[0];

            foldersService.CopyTo(path, source, target);

            FileInfo fileInfo = new FileInfo(Path.Combine(path, target, source));

            Assert.IsTrue(fileInfo.Exists);
        }

        [TestMethod]
        public void CopyTo_ValidFolderPath_FolderCopied()
        {
            string path = initialDirectory;
            string source = directories[0];
            string target = directories[1];

            foldersService.CopyTo(path, source, target);

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(path, target, source));

            Assert.IsTrue(directoryInfo.Exists);
        }

        [TestMethod]
        public void Rename_ValidFilePath_FileRenamed()
        {
            string newName = "new Name";

            foldersService.Rename(initialDirectory, files[0], newName);

            FileInfo newFileInfo = new FileInfo(Path.Combine(initialDirectory, newName));
            FileInfo oldFileInfo = new FileInfo(Path.Combine(initialDirectory, files[0]));

            Assert.IsTrue(newFileInfo.Exists && !oldFileInfo.Exists);
        }

        [TestMethod]
        public void Rename_ValidFolderPath_FolderRenamed()
        {
            string newName = "new Name";

            foldersService.Rename(initialDirectory, directories[0], newName);

            DirectoryInfo newFolderInfo = new DirectoryInfo(Path.Combine(initialDirectory, newName));
            DirectoryInfo oldFolderInfo = new DirectoryInfo(Path.Combine(initialDirectory, directories[0]));

            Assert.IsTrue(newFolderInfo.Exists && !oldFolderInfo.Exists);
        }
    }
}
