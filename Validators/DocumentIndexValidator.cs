using FluentValidation; // Importa la biblioteca FluentValidation para crear reglas de validación.
using KuantumLibraryApi.DTOs; // Importa el espacio de nombres de los DTOs, incluyendo DocumentIndexDto.

namespace KuantumLibraryApi.Validators
{
    // Define la clase DocumentIndexValidator que hereda de AbstractValidator<DocumentIndexDto>.
    // Esta clase se usa para validar los datos de un índice de documento.
    public class DocumentIndexValidator : AbstractValidator<DocumentIndexDto>
    {
        // Constructor de la clase. Aquí se definen las reglas de validación.
        public DocumentIndexValidator()
        {
            // Regla para la propiedad Title.
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título del índice es obligatorio") // No debe estar vacío.
                .MaximumLength(100).WithMessage("El título del índice excede el tamaño máximo de caracteres"); // Longitud máxima de 100.

            // Regla para la propiedad PageNumber.
            RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("El número de página es obligatorio") // No debe estar vacío.
                .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0"); // Debe ser un número mayor que 0.
        }
    }
}