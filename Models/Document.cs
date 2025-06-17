using System.ComponentModel.DataAnnotations;

namespace KuantumLibraryApi.Models
{
    public class Document
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string AuthorFullName { get; set; }
        
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string AuthorEmail { get; set; }
        
        [Required]
        [MaxLength(16)]
        public string SerialCode { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string PublicationCode { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public ICollection<DocumentPageIndex> PageIndices { get; set; }
    }
}