using System.ComponentModel.DataAnnotations;
using WebSite.Attributes;

namespace WebSite.ViewModels.Folders
{
    public class RenameFolderViewModel
    {
        public string Path { get; set; }

        public string OldName { get; set; }

        [Required]
        [FileName("A file name can't contain any of the following characters: \" \\ / : < > ? | *")]
        [Display(Name = "New name")]
        public string NewName { get; set; }
    }
}