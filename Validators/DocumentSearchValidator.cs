using FluentValidation; // Importa la biblioteca FluentValidation para crear reglas de validación.
using KuantumLibraryApi.DTOs; // Importa el espacio de nombres de los DTOs, incluyendo DocumentSearchDto.
using System.Text.RegularExpressions; // Importa el espacio de nombres para usar expresiones regulares.

namespace KuantumLibraryApi.Validators
{
    // Define la clase DocumentSearchValidator que hereda de AbstractValidator<DocumentSearchDto>.
    // Esta clase se usa para validar los criterios de búsqueda de documentos.
    public class DocumentSearchValidator : AbstractValidator<DocumentSearchDto>
    {
        // Constructor de la clase. Aquí se definen las reglas de validación.
        public DocumentSearchValidator()
        {
            // Regla para la propiedad Page.
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("La página debe ser mayor que 0"); // Debe ser un número mayor que 0.

            // Regla para la propiedad PageSize.
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0") // Debe ser un número mayor que 0.
                .LessThanOrEqualTo(100).WithMessage("El tamaño de página no puede ser mayor que 100"); // No debe ser mayor que 100.

            // Regla para la propiedad Id.
            RuleFor(x => x.Id)
                .GreaterThan(0).When(x => x.Id.HasValue) // Si Id tiene un valor, este debe ser mayor que 0.
                .WithMessage("El ID debe ser mayor que 0"); // Mensaje de error si la condición no se cumple.

            // Regla para la propiedad SerialCode.
            RuleFor(x => x.SerialCode)
                .Must(BeValidHexadecimal).When(x => !string.IsNullOrEmpty(x.SerialCode)) // Si SerialCode no es nulo ni vacío, debe ser un hexadecimal válido (usa el método BeValidHexadecimal).
                .WithMessage("SerialCode debe ser un valor hexadecimal válido"); // Mensaje de error si la condición no se cumple.
        }

        // Método privado para validar si un string es un código hexadecimal válido.
        private bool BeValidHexadecimal(string serialCode)
        {
            // Si el código es nulo o vacío, se considera válido para esta regla específica,
            // ya que la validación se aplica solo cuando el campo tiene un valor (controlado por .When()).
            if (string.IsNullOrEmpty(serialCode))
                return true;

            // Comprueba si el string contiene solo caracteres hexadecimales (0-9, A-F, a-f).
            return Regex.IsMatch(serialCode, @"^[0-9A-Fa-f]+$");
        }
    }
}