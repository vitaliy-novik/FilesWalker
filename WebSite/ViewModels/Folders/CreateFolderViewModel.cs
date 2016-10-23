using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Folders
{
    public class CreateFolderViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Path { get; set; }
    }
}