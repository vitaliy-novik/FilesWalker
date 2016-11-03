using Infrastructure.Entities;
using WebSite.ViewModels.Folders;

namespace WebSite.Mapping
{
    public class FolderMapper
    {
        public static Folder Map(CreateFolderViewModel viewModelFolder)
        {
            return new Folder()
            {
                Path = viewModelFolder.Path + "/" + viewModelFolder.Name
            };
        }

        public static FolderViewModel Map(Folder folder)
        {
            return new FolderViewModel()
            {
                Path = folder.Path,
                FilesList = folder.FilesList,
                FoldersList = folder.FoldersList
            };
        }

    }
}