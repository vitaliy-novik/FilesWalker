
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    /// <summary>
    /// Interface to perform file system operations
    /// </summary>
    public interface IFoldersService
    {
        /// <summary>
        /// Get root directories on a computer.
        /// </summary>
        Folder GetDirectories();

        /// <summary>
        /// Get content of the directory specified by path
        /// </summary>
        Folder GetDirectories(string path);

        /// <summary>
        /// Create directory with specifid path
        /// </summary>
        /// <param name="path">New drectory path</param>
        void CreateFolder(string path);

        /// <summary>
        /// Create file in the directry specified by path
        /// </summary>
        /// <param name="path">Path to the target directory</param>
        /// <param name="fileName">New file name</param>
        void CreateFile(string path, string fileName);

        /// <summary>
        /// Delete directory specified by path
        /// </summary>
        /// <param name="path">Target directory path</param>
        void DeleteFolder(string path);

        /// <summary>
        /// Delete a list of files and directories in the specified directory
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <param name="folders">List of files and directories names</param>
        void DeleteFolders(string path, IEnumerable<string> folders);

        /// <summary>
        /// Rename file or directory in specified by path directory
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="oldName">Name of target file or directory</param>
        /// <param name="newName">New name</param>
        void Rename(string path, string oldName, string newName);

        /// <summary>
        /// Move file or directorie in specified catalog to another directory in this catalog
        /// </summary>
        /// <param name="path">Path to the catalog</param>
        /// <param name="source">File or directory name</param>
        /// <param name="target">Target directory name</param>
        void CopyTo(string path, string source, string target);
    }
}
