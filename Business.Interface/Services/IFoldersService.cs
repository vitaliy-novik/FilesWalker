﻿
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IFoldersService
    {
        Folder GetDirectories(IUser user);

        Folder GetDirectories(IUser user, string path);
    }
}
