using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Business.Interface.Services;
using Infrastructure.Entities;

namespace Business.Core.Services
{
    /// <summary>
    /// Class for file system operations
    /// </summary>
    public class FoldersService : IFoldersService
    {
        /// <summary>
        /// All logical drives on a computer.
        /// </summary>
        private static DriveInfo[] allDrives;

        /// <summary>
        /// Initializes drives
        /// </summary>
        static FoldersService()
        {
            allDrives = DriveInfo.GetDrives();
        }

        /// <summary>
        /// Get root directories on a computer.
        /// </summary>
        /// <returns>Drives list in <see cref="Folder"/></returns>
        public Folder GetDirectories()
        {
            Folder folder = new Folder();
            foreach (DriveInfo driveInfo in FoldersService.allDrives)
            {
                folder.FoldersList.Add(driveInfo.RootDirectory);
            }
            
            return folder;
        }

        /// <summary>
        /// Get content of the directory specified by path
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>Directories and files in the directory</returns>
        public Folder GetDirectories(string path)
        {
            Folder folder = new Folder {Path = path};

            path = ReplaceDrive(path);
            
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            folder.FoldersList = new List<DirectoryInfo>(directoryInfo.EnumerateDirectories());
            folder.FilesList = new List<FileInfo>(directoryInfo.EnumerateFiles());
            
            return folder;
        }

        /// <summary>
        /// Create directory with specifid path
        /// </summary>
        /// <param name="path">New drectory path</param>
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

        /// <summary>
        /// Create file in the directry specified by path
        /// </summary>
        /// <param name="path">Path to the target directory</param>
        /// <param name="fileName">New file name</param>
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

        /// <summary>
        /// Delete directory specified by path
        /// </summary>
        /// <param name="path">Target directory path</param>
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

        /// <summary>
        /// Delete file specified by path
        /// </summary>
        /// <param name="path">Target file path</param>
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

        /// <summary>
        /// Delete a list of files and directories in the specified directory
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <param name="folders">List of files and directories names</param>
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

        /// <summary>
        /// Rename file or directory in specified by path directory
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="oldName">Name of target file or directory</param>
        /// <param name="newName">New name</param>
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

        /// <summary>
        /// Move file or directorie in specified catalog to another directory in this catalog
        /// </summary>
        /// <param name="path">Path to the catalog</param>
        /// <param name="source">File or directory name</param>
        /// <param name="target">Target directory name</param>
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

        /// <summary>
        /// Define if path correspond to the directory or file and perform specified action
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="fileAction">Action to perform for file</param>
        /// <param name="directoryAction">Action to perform for directory</param>
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

        /// <summary>
        /// Format path according to the OS standarts
        /// </summary>
        /// <param name="path">Initial path</param>
        /// <returns>Formated path</returns>
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
