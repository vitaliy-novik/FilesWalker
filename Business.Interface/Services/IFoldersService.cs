
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IFoldersService
    {
        Folder GetDirectories(IUser user);

        Folder GetDirectories(IUser user, string path);

        void CreateFolder(string path);

        void DeleteFolder(string path);

        void DeleteFolders(string path, IEnumerable<string> folders);

        void RenameFolder(string path, string oldName, string newName);

        void CopyTo(string path, string source, string target);
    }
}
