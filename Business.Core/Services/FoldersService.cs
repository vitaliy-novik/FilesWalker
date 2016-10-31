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

        public Folder GetDirectories()
        {
            Folder folder = new Folder();
            foreach (DriveInfo driveInfo in FoldersService.allDrives)
            {
                folder.FoldersList.Add(driveInfo.RootDirectory);
            }
            
            return folder;
        }

        public Folder GetDirectories(string path)
        {
            Folder folder = new Folder();
            folder.Path = path;

            path = ReplaceDrive(path);
            
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            folder.FoldersList = new List<DirectoryInfo>(directoryInfo.EnumerateDirectories());
            folder.FilesList = new List<FileInfo>(directoryInfo.EnumerateFiles());
            
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
            FileInfo fileInfo = new FileInfo(Path.Combine(path, fileName));
            if (!fileInfo.Exists)
            {
                fileInfo.Create().Close();
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
            foreach (string folder in folders)
            {
                string directory = path + folder;
                ExecuteFor(directory, () => DeleteFile(directory), () => DeleteFolder(directory));
            }
        }

        public void Rename(string path, string oldName, string newName)
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
            string oldPath = Path.Combine(path, oldName);
            string newPath = Path.Combine(path, newName);
            ExecuteFor(oldPath, () => RenameFile(oldPath, newPath), () => RenameFolder(oldPath, newPath));
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
            string oldPath = Path.Combine(path, source);
            string newPath = Path.Combine(path + Path.DirectorySeparatorChar + target, source);
            ExecuteFor(oldPath, () => MoveFile(oldPath, newPath), () => MoveFolder(oldPath, newPath));
        }

        private void RenameFolder(string oldPath, string newPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
            if (dirInfo.Exists && !Directory.Exists(newPath))
            {
                dirInfo.MoveTo(newPath);
            }
        }

        private void RenameFile(string oldPath, string newPath)
        {
            FileInfo fileInfo = new FileInfo(oldPath);
            if (fileInfo.Exists && !File.Exists(newPath))
            {
                fileInfo.MoveTo(newPath);
            }
        }

        private void ExecuteFor(string path, Action fileAction, Action directoryAction)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                directoryAction();
            }
            else
            {
                fileAction();
            }
        }

        private void MoveFolder(string source, string target)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(source);
            if (dirInfo.Exists && !Directory.Exists(target))
            {
                dirInfo.MoveTo(target);
            }
        }

        private void MoveFile(string source, string target)
        {
            FileInfo fileInfo = new FileInfo(source);
            if (fileInfo.Exists && !Directory.Exists(target))
            {
                fileInfo.MoveTo(target);
            }
        }

        private string ReplaceDrive(string path)
        {
            string[] folders = path.Split('/').Where(f => !string.IsNullOrEmpty(f)).ToArray();
            string drive = folders[0] + Path.VolumeSeparatorChar;
            StringBuilder newPath = new StringBuilder();
            if (allDrives.Any(dr => dr.Name.StartsWith(drive)))
            {
                newPath.Append(allDrives.FirstOrDefault(dr => dr.Name.StartsWith(drive)).Name);
                for (int i = 1; i < folders.Length; ++i)
                {
                    newPath.Append(folders[i]);
                    newPath.Append(Path.DirectorySeparatorChar);
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
