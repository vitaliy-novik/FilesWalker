using System.Collections.Generic;
using System.IO;

namespace Infrastructure.Entities
{
    public class Folder
    {
        public Folder()
        {
            FoldersList = new List<DirectoryInfo>();
            FilesList = new List<FileInfo>();
        }

        public string Path { get; set; }

        public List<DirectoryInfo> FoldersList { get; set; }
        
        public List<FileInfo> FilesList { get; set; } 
    }
}
