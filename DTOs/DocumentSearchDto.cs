using System; // Importa el espacio de nombres System, que proporciona funcionalidades básicas.

namespace KuantumLibraryApi.DTOs
{
    // Define la clase DocumentSearchDto.
    // Se utiliza para encapsular los criterios de búsqueda de documentos.
    public class DocumentSearchDto
    {
        // Propiedad para el ID del documento a buscar (puede ser nulo).
        public int? Id { get; set; }
        // Propiedad para el código serial del documento a buscar (puede ser nulo).
        public string? SerialCode { get; set; }
        // Propiedad para el código de publicación del documento a buscar (puede ser nulo).
        public string? PublicationCode { get; set; }
        // Propiedad para un término de búsqueda general (puede ser nulo).
        public string? Search { get; set; }
        // Propiedad para el número de página de los resultados (valor por defecto es 1).
        public int Page { get; set; } = 1;
        // Propiedad para el tamaño de la página (cantidad de resultados por página, valor por defecto es 10).
        public int PageSize { get; set; } = 10;
    }
}