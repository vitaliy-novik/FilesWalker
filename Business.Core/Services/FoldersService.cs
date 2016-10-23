using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Interface.Services;
using Infrastructure.Entities;

namespace Business.Core.Services
{
    public class FoldersService : IFoldersService
    {
        private static DriveInfo[] allDrives;

        static FoldersService()
        {
            allDrives = DriveInfo.GetDrives();
        }

        public Folder GetDirectories(IUser user)
        {
            Folder folder = new Folder();
            foreach (DriveInfo driveInfo in FoldersService.allDrives)
            {
                folder.FoldersList.Add(driveInfo.RootDirectory);
            }

            return folder;
        }

        public Folder GetDirectories(IUser user, string path)
        {
            Folder folder = new Folder();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            folder.FoldersList = new List<DirectoryInfo>(directoryInfo.GetDirectories());
            folder.FilesList = new List<FileInfo>(directoryInfo.GetFiles());
            folder.Path = path;

            return folder;
        }

        public void CreateFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
    }
}
