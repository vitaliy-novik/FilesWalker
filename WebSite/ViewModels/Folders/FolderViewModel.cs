using System.Collections.Generic;
using System.IO;

namespace WebSite.ViewModels.Folders
{
    public class FolderViewModel
    {
        public string Path { get; set; }

        public List<DirectoryInfo> FoldersList { get; set; }

        public List<FileInfo> FilesList { get; set; } 
    }
}