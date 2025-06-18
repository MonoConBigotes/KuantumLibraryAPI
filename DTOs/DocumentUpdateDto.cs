namespace KuantumLibraryApi.DTOs
{
    // Define la clase DocumentUpdateDto.
    // Hereda de DocumentCreateDto, lo que significa que tiene todas sus propiedades.
    // Se utiliza para transferir datos al actualizar un documento existente.
    public class DocumentUpdateDto : DocumentCreateDto
    {
        // Propiedad para el ID del documento que se va a actualizar.
        public int Id { get; set; }
    }
}