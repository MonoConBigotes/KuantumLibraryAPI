using FluentValidation;
using KuantumLibraryApi.DTOs;
using System.Text.RegularExpressions;

namespace KuantumLibraryApi.Validators
{
    // Validador para el DTO DocumentCreateDto usando FluentValidation
    public class DocumentCreateValidator : AbstractValidator<DocumentCreateDto>
    {
        public DocumentCreateValidator()
        {
            // Regla para el campo Name:
            // - No puede estar vacío
            // - Longitud máxima de 100 caracteres
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name es obligatorio")
                .MaximumLength(100).WithMessage("Name excede el tamaño máximo de caracteres");

            // Regla para el campo AuthorFullName:
            // - No puede estar vacío
            // - Longitud máxima de 300 caracteres
            RuleFor(x => x.AuthorFullName)
                .NotEmpty().WithMessage("AuthorFullName es obligatorio")
                .MaximumLength(300).WithMessage("AuthorFullName excede el tamaño máximo de caracteres");

            // Regla para el campo AuthorEmail:
            // - No puede estar vacío
            // - Debe ser un email válido
            // - Longitud máxima de 100 caracteres
            RuleFor(x => x.AuthorEmail)
                .NotEmpty().WithMessage("AuthorEmail es obligatorio")
                .EmailAddress().WithMessage("AuthorEmail debe ser un correo válido")
                .MaximumLength(100).WithMessage("AuthorEmail excede el tamaño máximo de caracteres");

            // Regla para el campo SerialCode:
            // - No puede estar vacío
            // - Debe ser un valor hexadecimal válido (validado por el método BeValidHexadecimal)
            // - Longitud máxima de 16 caracteres
            RuleFor(x => x.SerialCode)
                .NotEmpty().WithMessage("SerialCode es obligatorio")
                .Must(BeValidHexadecimal).WithMessage("SerialCode debe ser un valor hexadecimal válido")
                .MaximumLength(16).WithMessage("SerialCode excede el tamaño máximo de caracteres");

            // Regla para el campo PublicationCode:
            // - No puede estar vacío
            // - Debe tener un formato válido (validado por el método BeValidPublicationCode)
            // - Longitud máxima de 100 caracteres
            RuleFor(x => x.PublicationCode)
                .NotEmpty().WithMessage("PublicationCode es obligatorio")
                .Must(BeValidPublicationCode).WithMessage("PublicationCode debe tener un formato válido (ISO-XXXX, Ley N° XXXX, o P-XX.[AAAAMMDD])")
                .MaximumLength(100).WithMessage("PublicationCode excede el tamaño máximo de caracteres");

            // Regla para el campo Indexes:
            // - No puede estar vacío
            // - Debe contener al menos un elemento
            RuleFor(x => x.Indexes)
                .NotEmpty().WithMessage("Debe incluir al menos un elemento en el índice")
                .Must(x => x != null && x.Count > 0).WithMessage("Debe incluir al menos un elemento en el índice");

            // Aplica un validador específico para cada elemento de la colección Indexes
            RuleForEach(x => x.Indexes)
                .SetValidator(new DocumentIndexValidator());
        }

        // Método para validar si un string es un valor hexadecimal válido
        private bool BeValidHexadecimal(string serialCode)
        {
            if (string.IsNullOrEmpty(serialCode))
                return false;

            // Usa una expresión regular para verificar que solo contenga dígitos 0-9 y letras A-F (mayúsculas o minúsculas)
            return Regex.IsMatch(serialCode, @"^[0-9A-Fa-f]+$");
        }

        // Método para validar el formato del PublicationCode
        private bool BeValidPublicationCode(string publicationCode)
        {
            if (string.IsNullOrEmpty(publicationCode))
                return false;

            // Formato ISO-XXXX (donde XXXX son dígitos)
            if (Regex.IsMatch(publicationCode, @"^ISO-\d+$"))
                return true;

            // Formato Ley N° XXXX (con separador de miles opcional)
            if (Regex.IsMatch(publicationCode, @"^Ley N° \d{1,3}(\.\d{3})*$"))
                return true;

            // Formato P-XX.[AAAAMMDD] (donde XX son dígitos y AAAAMMDD es una fecha válida)
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
                        // Intenta crear una fecha para validar que sea una fecha válida
                        var date = new DateTime(year, month, day);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            // Si no coincide con ninguno de los formatos válidos, retorna false
            return false;
        }
    }
}