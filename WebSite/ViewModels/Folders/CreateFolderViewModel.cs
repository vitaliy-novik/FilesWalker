using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Folders
{
    public class CreateFolderViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}