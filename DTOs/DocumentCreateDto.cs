using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuantumLibraryApi.DTOs
{
    public class DocumentCreateDto
    {
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
        public string AuthorEmail { get; set; }
        
        [Required]
        [MaxLength(16)]
        public string SerialCode { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string PublicationCode { get; set; }
        
        [Required]
        [MinLength(1)]
        public List<DocumentIndexDto> Indexes { get; set; }
    }
}