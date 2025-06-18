using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para usar atributos de validación.

namespace KuantumLibraryApi.Models
{
    // Define la clase DocumentPageIndex.
    // Representa un índice de página para un documento en la base de datos.
    public class DocumentPageIndex
    {
        // Propiedad para el ID único del índice de página (clave primaria).
        public int Id { get; set; }

        // Propiedad para el ID del documento al que pertenece este índice.
        // [Required] indica que es obligatorio.
        [Required]
        public int DocumentId { get; set; }

        // Propiedad para el nombre o título del índice.
        // [Required] indica que es obligatorio.
        // [MaxLength(100)] limita la longitud máxima a 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Propiedad para el número de página donde se encuentra este índice.
        // [Required] indica que es obligatorio.
        [Required]
        public int Page { get; set; }

        // Propiedad para la fecha y hora de creación del índice.
        public DateTime CreatedAt { get; set; }

        // Propiedad de navegación al objeto Document al que este índice está asociado.
        public Document Document { get; set; }
    }
}