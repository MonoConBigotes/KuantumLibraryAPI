using FluentValidation;
using KuantumLibraryApi.DTOs;
using System.Text.RegularExpressions;

namespace KuantumLibraryApi.Validators
{
    public class DocumentSearchValidator : AbstractValidator<DocumentSearchDto>
    {
        public DocumentSearchValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("La página debe ser mayor que 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0")
                .LessThanOrEqualTo(100).WithMessage("El tamaño de página no puede ser mayor que 100");

            RuleFor(x => x.Id)
                .GreaterThan(0).When(x => x.Id.HasValue)
                .WithMessage("El ID debe ser mayor que 0");

            RuleFor(x => x.SerialCode)
                .Must(BeValidHexadecimal).When(x => !string.IsNullOrEmpty(x.SerialCode))
                .WithMessage("SerialCode debe ser un valor hexadecimal válido");
        }

        private bool BeValidHexadecimal(string serialCode)
        {
            if (string.IsNullOrEmpty(serialCode))
                return true;

            return Regex.IsMatch(serialCode, @"^[0-9A-Fa-f]+$");
        }
    }
}