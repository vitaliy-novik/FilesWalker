using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Infrastructure.Entities;
using WebSite.Models;
using WebSite.ViewModels.Account;
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

        public static Folder Map(FolderViewModel viewModelFolder)
        {
            return Mapper.Map<FolderViewModel, Folder>(viewModelFolder);
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