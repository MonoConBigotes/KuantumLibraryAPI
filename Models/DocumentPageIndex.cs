using System.ComponentModel.DataAnnotations;

namespace KuantumLibraryApi.Models
{
    public class DocumentPageIndex
    {
        public int Id { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Page { get; set; }

        public DateTime CreatedAt { get; set; }

        public Document Document { get; set; }
    }
}