using AutoMapper;
using Infrastructure.Entities;
using WebSite.ViewModels.Folders;

namespace WebSite.Mapping
{
    public class FolderMapper
    {
        static FolderMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Folder, FolderViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<FolderViewModel, Folder>());
        }

        public static Folder Map(CreateFolderViewModel viewModelFolder)
        {
            return new Folder()
            {
                Path = viewModelFolder.Path + viewModelFolder.Name
            };
            //Mapper.Map<CreateFolderViewModel, Folder>(viewModelFolder);
        }

        public static FolderViewModel Map(Folder folder)
        {
            return new FolderViewModel()
            {
                Path = folder.Path,
                FilesList = folder.FilesList,
                FoldersList = folder.FoldersList
            };
                //Mapper.Map<Folder, FolderViewModel>(folder);
        }
    }
}