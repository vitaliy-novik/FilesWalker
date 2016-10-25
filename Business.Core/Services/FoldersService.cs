using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            folder.Path = path;

            path = ReplaceDrive(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            folder.FoldersList = new List<DirectoryInfo>(directoryInfo.GetDirectories());
            folder.FilesList = new List<FileInfo>(directoryInfo.GetFiles());
            
            return folder;
        }

        public void CreateFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = ReplaceDrive(path);
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public void CreateFile(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            path = ReplaceDrive(path);
            FileInfo fileInfo = new FileInfo(path + "/" + fileName);
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }
        }

        public void DeleteFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = ReplaceDrive(path);
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
        }

        public void DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = ReplaceDrive(path);
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }


        public void DeleteFolders(string path, IEnumerable<string> folders)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (folders == null)
            {
                throw new ArgumentNullException("folders");
            }

            path = ReplaceDrive(path);
            FileAttributes attr;
            string directory;
            foreach (string folder in folders)
            {
                directory = path + folder;
                attr = File.GetAttributes(directory);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    DeleteFolder(directory);
                }
                else
                {
                    DeleteFile(directory);
                }
                
            }
        }

        public void RenameFolder(string path, string oldName, string newName)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrEmpty(oldName))
            {
                throw new ArgumentNullException("oldName");
            }

            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException("newName");
            }

            path = ReplaceDrive(path);
            string newDirectory = path + "/" + newName;
            DirectoryInfo dirInfo = new DirectoryInfo(path + "/" + oldName);
            if (dirInfo.Exists && !Directory.Exists(newDirectory))
            {
                dirInfo.MoveTo(newDirectory);
            }
        }

        public void CopyTo(string path, string source, string target)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }

            path = ReplaceDrive(path);
            string oldPath = path + "/" + source;
            string newPath = path + "/" + target + "/" + source;
            FileAttributes attr = File.GetAttributes(oldPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                MoveFolder(oldPath, newPath);
            }
            else
            {
                MoveFile(oldPath, newPath);
            }
        }

        private void MoveFolder(string source, string target)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(source);
            if (dirInfo.Exists && !Directory.Exists(target))
            {
                dirInfo.MoveTo(target);
            }

            dirInfo.Delete(true);
        }

        private void MoveFile(string source, string target)
        {
            FileInfo fileInfo = new FileInfo(source);
            if (fileInfo.Exists && !Directory.Exists(target))
            {
                fileInfo.MoveTo(target);
            }

            fileInfo.Delete();
        }

        private string ReplaceDrive(string path)
        {
            string[] folders = path.Split('/');
            string drive = folders[0] + ":";
            StringBuilder newPath = new StringBuilder();
            if (allDrives.Any(dr => dr.Name.StartsWith(drive)))
            {
                newPath.Append(allDrives.FirstOrDefault(dr => dr.Name.StartsWith(drive)).Name);
                for (int i = 1; i < folders.Length; ++i)
                {
                    newPath.Append(folders[i]);
                    newPath.Append("/");
                }
            }

            if (newPath.Length == 0)
            {
                return path;
            }

            return newPath.ToString();
        }
    }
}
