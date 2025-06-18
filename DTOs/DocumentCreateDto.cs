using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuantumLibraryApi.DTOs
{
    // Define la clase Data Transfer Object (DTO) para la creación de un nuevo documento.
    // Esta clase se utiliza para transferir datos entre el cliente y el servidor al crear un documento.
    public class DocumentCreateDto
    {
        // Propiedad para el nombre del documento.
        // [Required] indica que este campo es obligatorio.
        // [MaxLength(100)] indica que la longitud máxima del nombre es de 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        // Propiedad para la descripción del documento.
        // [MaxLength(1000)] indica que la longitud máxima de la descripción es de 1000 caracteres.
        // Este campo no es obligatorio.
        [MaxLength(1000)]
        public string Description { get; set; }
        
        // Propiedad para el nombre completo del autor del documento.
        // [Required] indica que este campo es obligatorio.
        // [MaxLength(300)] indica que la longitud máxima del nombre del autor es de 300 caracteres.
        [Required]
        [MaxLength(300)]
        public string AuthorFullName { get; set; }
        
        // Propiedad para el correo electrónico del autor del documento.
        // [Required] indica que este campo es obligatorio.
        // [MaxLength(100)] indica que la longitud máxima del correo electrónico es de 100 caracteres.
        // Se podría añadir una validación de formato de email aquí si fuera necesario ([EmailAddress]).
        [Required]
        [MaxLength(100)]
        public string AuthorEmail { get; set; }
        
        // Propiedad para el código serial del documento.
        // [Required] indica que este campo es obligatorio.
        // [MaxLength(16)] indica que la longitud máxima del código serial es de 16 caracteres.
        [Required]
        [MaxLength(16)]
        public string SerialCode { get; set; }
        
        // Propiedad para el código de publicación del documento.
        // [Required] indica que este campo es obligatorio.
        // [MaxLength(100)] indica que la longitud máxima del código de publicación es de 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string PublicationCode { get; set; }
        
        // Propiedad para la lista de índices del documento.
        // Cada elemento de la lista es un objeto DocumentIndexDto.
        // [Required] indica que la lista de índices es obligatoria.
        // [MinLength(1)] indica que la lista debe contener al menos un índice.
        [Required]
        [MinLength(1)]
        public List<DocumentIndexDto> Indexes { get; set; }
    }
}
