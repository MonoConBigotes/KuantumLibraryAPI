namespace KuantumLibraryApi.DTOs
{
    // Define la clase DocumentIndexDto.
    // Representa los datos de un índice de documento para transferencia.
    public class DocumentIndexDto
    {
        // Propiedad para el ID del índice.
        public int Id { get; set; }
        // Propiedad para el título del índice.
        public string Title { get; set; }
        // Propiedad para el número de página donde se encuentra el índice.
        public int PageNumber { get; set; }
        // Propiedad para la fecha de creación del índice.
        public DateTime CreatedAt { get; set; }
    }
}