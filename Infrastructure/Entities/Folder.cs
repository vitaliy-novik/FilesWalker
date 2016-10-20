using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
