using FluentValidation;
using KuantumLibraryApi.DTOs;

namespace KuantumLibraryApi.Validators
{
    public class DocumentUpdateValidator : AbstractValidator<DocumentUpdateDto>
    {
        public DocumentUpdateValidator()
        {
            Include(new DocumentCreateValidator());

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id es obligatorio")
                .GreaterThan(0).WithMessage("Id debe ser mayor que 0");
        }
    }
}