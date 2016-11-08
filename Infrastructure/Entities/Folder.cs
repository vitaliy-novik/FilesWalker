using System.Collections.Generic;
using System.IO;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Class represents file system directory content
    /// </summary>
    public class Folder
    {
        public Folder()
        {
            FoldersList = new List<DirectoryInfo>();
            FilesList = new List<FileInfo>();
        }

        /// <summary>
        /// Path to th e directory
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Internal directory foldrs list
        /// </summary>
        public List<DirectoryInfo> FoldersList { get; set; }
        
        /// <summary>
        /// Internal directory files
        /// </summary>
        public List<FileInfo> FilesList { get; set; } 
    }
}
