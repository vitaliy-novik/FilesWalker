using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Folders
{
    public class RenameFolderViewModel
    {
        public string Path { get; set; }

        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}