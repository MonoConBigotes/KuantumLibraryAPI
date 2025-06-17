using FluentValidation;
using KuantumLibraryApi.DTOs;
using System.Text.RegularExpressions;

namespace KuantumLibraryApi.Validators
{
    public class DocumentCreateValidator : AbstractValidator<DocumentCreateDto>
    {
        public DocumentCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name es obligatorio")
                .MaximumLength(100).WithMessage("Name excede el tamaño máximo de caracteres");

            RuleFor(x => x.AuthorFullName)
                .NotEmpty().WithMessage("AuthorFullName es obligatorio")
                .MaximumLength(300).WithMessage("AuthorFullName excede el tamaño máximo de caracteres");

            RuleFor(x => x.AuthorEmail)
                .NotEmpty().WithMessage("AuthorEmail es obligatorio")
                .EmailAddress().WithMessage("AuthorEmail debe ser un correo válido")
                .MaximumLength(100).WithMessage("AuthorEmail excede el tamaño máximo de caracteres");

            RuleFor(x => x.SerialCode)
                .NotEmpty().WithMessage("SerialCode es obligatorio")
                .Must(BeValidHexadecimal).WithMessage("SerialCode debe ser un valor hexadecimal válido")
                .MaximumLength(16).WithMessage("SerialCode excede el tamaño máximo de caracteres");

            RuleFor(x => x.PublicationCode)
                .NotEmpty().WithMessage("PublicationCode es obligatorio")
                .Must(BeValidPublicationCode).WithMessage("PublicationCode debe tener un formato válido (ISO-XXXX, Ley N° XXXX, o P-XX.[AAAAMMDD])")
                .MaximumLength(100).WithMessage("PublicationCode excede el tamaño máximo de caracteres");

            RuleFor(x => x.Indexes)
                .NotEmpty().WithMessage("Debe incluir al menos un elemento en el índice")
                .Must(x => x != null && x.Count > 0).WithMessage("Debe incluir al menos un elemento en el índice");

            RuleForEach(x => x.Indexes)
                .SetValidator(new DocumentIndexValidator());
        }

        private bool BeValidHexadecimal(string serialCode)
        {
            if (string.IsNullOrEmpty(serialCode))
                return false;

            return Regex.IsMatch(serialCode, @"^[0-9A-Fa-f]+$");
        }

        private bool BeValidPublicationCode(string publicationCode)
        {
            if (string.IsNullOrEmpty(publicationCode))
                return false;

            // Formato ISO-XXXX
            if (Regex.IsMatch(publicationCode, @"^ISO-\d+$"))
                return true;

            // Formato Ley N° XXXX (con separador de miles)
            if (Regex.IsMatch(publicationCode, @"^Ley N° \d{1,3}(\.\d{3})*$"))
                return true;

            // Formato P-XX.[AAAAMMDD]
            if (Regex.IsMatch(publicationCode, @"^P-\d{2}\.\d{8}$"))
            {
                var datePart = publicationCode.Substring(4);
                if (datePart.Length == 8)
                {
                    var year = int.Parse(datePart.Substring(0, 4));
                    var month = int.Parse(datePart.Substring(4, 2));
                    var day = int.Parse(datePart.Substring(6, 2));

                    try
                    {
                        var date = new DateTime(year, month, day);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}