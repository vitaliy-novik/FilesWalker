
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IFoldersService
    {
        Folder GetDirectories();

        Folder GetDirectories(string path);

        void CreateFolder(string path);

        void CreateFile(string path, string fileName);

        void DeleteFolder(string path);

        void DeleteFolders(string path, IEnumerable<string> folders);

        void Rename(string path, string oldName, string newName);

        void CopyTo(string path, string source, string target);
    }
}
