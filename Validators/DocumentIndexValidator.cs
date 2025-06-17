using FluentValidation;
using KuantumLibraryApi.DTOs;

namespace KuantumLibraryApi.Validators
{
    public class DocumentIndexValidator : AbstractValidator<DocumentIndexDto>
    {
        public DocumentIndexValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título del índice es obligatorio")
                .MaximumLength(100).WithMessage("El título del índice excede el tamaño máximo de caracteres");

            RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("El número de página es obligatorio")
                .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0");
        }
    }
}