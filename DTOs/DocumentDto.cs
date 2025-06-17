using KuantumLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace KuantumLibraryApi.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorEmail { get; set; }
        public string SerialCode { get; set; }
        public string PublicationCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<DocumentIndexDto> Indexes { get; set; }

        public static DocumentDto FromDocument(Document document)
        {
            return new DocumentDto
            {
                Id = document.Id,
                Name = document.Name,
                Description = document.Description,
                AuthorFullName = document.AuthorFullName,
                AuthorEmail = document.AuthorEmail,
                SerialCode = document.SerialCode,
                PublicationCode = document.PublicationCode,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                Indexes = document.PageIndices?.Select(pi => new DocumentIndexDto
                {
                    Id = pi.Id,
                    Title = pi.Name,
                    PageNumber = pi.Page,
                    CreatedAt = pi.CreatedAt
                }).ToList()
            };
        }
    }
}