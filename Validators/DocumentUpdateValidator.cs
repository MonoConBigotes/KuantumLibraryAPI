using FluentValidation; // Importa la biblioteca FluentValidation para crear reglas de validación.
using KuantumLibraryApi.DTOs; // Importa el espacio de nombres de los DTOs, incluyendo DocumentUpdateDto.

namespace KuantumLibraryApi.Validators
{
    // Define la clase DocumentUpdateValidator que hereda de AbstractValidator<DocumentUpdateDto>.
    // Esta clase se usa para validar los datos al actualizar un documento existente.
    public class DocumentUpdateValidator : AbstractValidator<DocumentUpdateDto>
    {
        // Constructor de la clase. Aquí se definen las reglas de validación.
        public DocumentUpdateValidator()
        {
            // Incluye todas las reglas de validación definidas en DocumentCreateValidator.
            // Esto es útil porque DocumentUpdateDto hereda de DocumentCreateDto y comparte muchas de sus propiedades.
            Include(new DocumentCreateValidator());

            // Regla para la propiedad Id.
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id es obligatorio") // No debe estar vacío.
                .GreaterThan(0).WithMessage("Id debe ser mayor que 0"); // Debe ser un número mayor que 0.
        }
    }
}