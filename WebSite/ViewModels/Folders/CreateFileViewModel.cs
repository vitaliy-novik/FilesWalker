using System.ComponentModel.DataAnnotations;
using WebSite.Attributes;

namespace WebSite.ViewModels.Folders
{
    public class CreateFileViewModel
    {
        [Required]
        [FileName("A file name can't contain any of the following characters: \" \\ / : < > ? | *")]
        public string Name { get; set; }

        public string Path { get; set; }
    }
}