using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para usar atributos de validación.

namespace KuantumLibraryApi.Models
{
    // Define la clase Document, que representa la entidad de un documento en la base de datos.
    public class Document
    {
        // Propiedad para el ID único del documento (clave primaria).
        public int Id { get; set; }
        
        // Propiedad para el nombre del documento.
        // [Required] indica que es obligatorio.
        // [MaxLength(100)] limita la longitud máxima a 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        // Propiedad para la descripción del documento.
        // [MaxLength(1000)] limita la longitud máxima a 1000 caracteres.
        [MaxLength(1000)]
        public string Description { get; set; }
        
        // Propiedad para el nombre completo del autor.
        // [Required] indica que es obligatorio.
        // [MaxLength(300)] limita la longitud máxima a 300 caracteres.
        [Required]
        [MaxLength(300)]
        public string AuthorFullName { get; set; }
        
        // Propiedad para el correo electrónico del autor.
        // [Required] indica que es obligatorio.
        // [MaxLength(100)] limita la longitud máxima a 100 caracteres.
        // [EmailAddress] valida que el formato sea de un correo electrónico.
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string AuthorEmail { get; set; }
        
        // Propiedad para el código serial del documento.
        // [Required] indica que es obligatorio.
        // [MaxLength(16)] limita la longitud máxima a 16 caracteres.
        [Required]
        [MaxLength(16)]
        public string SerialCode { get; set; }
        
        // Propiedad para el código de publicación del documento.
        // [Required] indica que es obligatorio.
        // [MaxLength(100)] limita la longitud máxima a 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string PublicationCode { get; set; }
        
        // Propiedad para la fecha y hora de creación del documento.
        public DateTime CreatedAt { get; set; }
        // Propiedad para la fecha y hora de la última actualización del documento (puede ser nula).
        public DateTime? UpdatedAt { get; set; }
        // Propiedad para la fecha y hora en que el documento fue eliminado lógicamente (puede ser nula).
        public DateTime? DeletedAt { get; set; }
        
        // Propiedad de navegación para la colección de índices de página asociados a este documento.
        public ICollection<DocumentPageIndex> PageIndices { get; set; }
    }
}