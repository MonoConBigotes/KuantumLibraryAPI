using KuantumLibraryApi.Models; // Importa el espacio de nombres para los modelos.
using System.Collections.Generic; // Importa el espacio de nombres para usar List<T>.
using System.Linq; // Importa el espacio de nombres para usar LINQ.

namespace KuantumLibraryApi.DTOs
{
    // Define la clase DocumentDto.
    public class DocumentDto
    {
        // Propiedad para el ID del documento.
        public int Id { get; set; }
        // Propiedad para el nombre del documento.
        public string Name { get; set; }
        // Propiedad para la descripción del documento.
        public string Description { get; set; }
        // Propiedad para el nombre completo del autor.
        public string AuthorFullName { get; set; }
        // Propiedad para el email del autor.
        public string AuthorEmail { get; set; }
        // Propiedad para el código serial del documento.
        public string SerialCode { get; set; }
        // Propiedad para el código de publicación del documento.
        public string PublicationCode { get; set; }
        // Propiedad para la fecha de creación del documento.
        public DateTime CreatedAt { get; set; }
        // Propiedad para la fecha de actualización del documento (puede ser nula).
        public DateTime? UpdatedAt { get; set; }
        // Propiedad para la lista de índices del documento.
        public List<DocumentIndexDto> Indexes { get; set; }

        // Método estático para convertir un objeto Document a DocumentDto.
        public static DocumentDto FromDocument(Document document)
        {
            // Retorna una nueva instancia de DocumentDto.
            return new DocumentDto
            {
                // Asigna el Id del documento al DTO.
                Id = document.Id,
                // Asigna el Name del documento al DTO.
                Name = document.Name,
                // Asigna la Description del documento al DTO.
                Description = document.Description,
                // Asigna el AuthorFullName del documento al DTO.
                AuthorFullName = document.AuthorFullName,
                // Asigna el AuthorEmail del documento al DTO.
                AuthorEmail = document.AuthorEmail,
                // Asigna el SerialCode del documento al DTO.
                SerialCode = document.SerialCode,
                // Asigna el PublicationCode del documento al DTO.
                PublicationCode = document.PublicationCode,
                // Asigna el CreatedAt del documento al DTO.
                CreatedAt = document.CreatedAt,
                // Asigna el UpdatedAt del documento al DTO.
                UpdatedAt = document.UpdatedAt,
                // Convierte la lista de PageIndices del documento a una lista de DocumentIndexDto.
                Indexes = document.PageIndices?.Select(pi => new DocumentIndexDto
                {
                    // Asigna el Id del índice de página al DTO del índice.
                    Id = pi.Id,
                    // Asigna el Name (título) del índice de página al DTO del índice.
                    Title = pi.Name,
                    // Asigna la Page (número de página) del índice de página al DTO del índice.
                    PageNumber = pi.Page,
                    // Asigna el CreatedAt del índice de página al DTO del índice.
                    CreatedAt = pi.CreatedAt
                }).ToList() // Convierte el resultado a una lista.
            };
        }
    }
}