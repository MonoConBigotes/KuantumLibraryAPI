using FluentValidation;
using KuantumLibraryApi.Data;
using KuantumLibraryApi.DTOs;
using KuantumLibraryApi.Models;
using KuantumLibraryApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuantumLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        // Inyección de dependencias para acceder a la base de datos y validadores
        private readonly LibraryDbContext _context;
        private readonly IValidator<DocumentCreateDto> _createValidator;
        private readonly IValidator<DocumentUpdateDto> _updateValidator;
        private readonly IValidator<DocumentSearchDto> _searchValidator;

        public DocumentsController(
            LibraryDbContext context,
            IValidator<DocumentCreateDto> createValidator,
            IValidator<DocumentUpdateDto> updateValidator,
            IValidator<DocumentSearchDto> searchValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _searchValidator = searchValidator;
        }

        // Acción para obtener un documento específico por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            var document = await _context.Documents
                .Include(d => d.PageIndices) // Incluye las páginas relacionadas
                .FirstOrDefaultAsync(d => d.Id == id && d.DeletedAt == null);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(DocumentDto.FromDocument(document)); // Devuelve el documento en formato DTO
        }

        // Acción para buscar documentos utilizando filtros pasados como parámetros query
        [HttpGet]
        public async Task<ActionResult<PagedResult<DocumentDto>>> GetDocuments([FromQuery] DocumentSearchDto searchDto)
        {
            var validationResult = await _searchValidator.ValidateAsync(searchDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new {
                    error = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            var query = _context.Documents.Where(d => d.DeletedAt == null).AsQueryable();

            // Aplica filtros según los parámetros de búsqueda
            if (searchDto.Id.HasValue)
            {
                query = query.Where(d => d.Id == searchDto.Id.Value);
            }

            if (!string.IsNullOrEmpty(searchDto.SerialCode))
            {
                query = query.Where(d => d.SerialCode == searchDto.SerialCode);
            }

            if (!string.IsNullOrEmpty(searchDto.PublicationCode))
            {
                query = query.Where(d => d.PublicationCode == searchDto.PublicationCode);
            }

            if (!string.IsNullOrEmpty(searchDto.Search))
            {
                query = query.Where(d =>
                    d.AuthorFullName.Contains(searchDto.Search) ||
                    d.AuthorEmail.Contains(searchDto.Search));
            }

            var totalCount = await query.CountAsync();
            var documents = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();

            var result = new PagedResult<DocumentDto>
            {
                Items = documents.Select(DocumentDto.FromDocument).ToList(),
                TotalCount = totalCount,
                PageNumber = searchDto.Page,
                PageSize = searchDto.PageSize
            };

            return Ok(result); // Devuelve el resultado paginado de documentos
        }

        // Acción para crear un documento nuevo
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new {
                    error = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var document = new Document
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    AuthorFullName = dto.AuthorFullName,
                    AuthorEmail = dto.AuthorEmail,
                    SerialCode = dto.SerialCode,
                    PublicationCode = dto.PublicationCode,
                    CreatedAt = DateTime.UtcNow,
                    PageIndices = dto.Indexes.Select(pi => new DocumentPageIndex
                    {
                        Name = pi.Title,
                        Page = pi.PageNumber,
                        CreatedAt = DateTime.UtcNow
                    }).ToList()
                };

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(
                    nameof(GetDocument),
                    new { id = document.Id },
                    DocumentDto.FromDocument(document)); // Devuelve el documento creado
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = new[] { "Error al crear el documento" } });
            }
        }

        // Acción para actualizar un documento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateDto dto)
        {
            dto.Id = id;
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new {
                    error = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var document = await _context.Documents
                    .Include(d => d.PageIndices)
                    .FirstOrDefaultAsync(d => d.Id == id && d.DeletedAt == null);

                if (document == null)
                {
                    return NotFound();
                }

                // Actualiza los detalles del documento
                document.Name = dto.Name;
                document.Description = dto.Description;
                document.AuthorFullName = dto.AuthorFullName;
                document.AuthorEmail = dto.AuthorEmail;
                document.SerialCode = dto.SerialCode;
                document.PublicationCode = dto.PublicationCode;
                document.UpdatedAt = DateTime.UtcNow;

                // Actualiza los índices de página
                _context.DocumentPageIndices.RemoveRange(document.PageIndices);
                document.PageIndices = dto.Indexes.Select(pi => new DocumentPageIndex
                {
                    Name = pi.Title,
                    Page = pi.PageNumber,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(DocumentDto.FromDocument(document)); // Devuelve el documento actualizado
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = new[] { "Error al actualizar el documento" } });
            }
        }

        // Acción para eliminar un documento (borrado lógico)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var document = await _context.Documents
                    .FirstOrDefaultAsync(d => d.Id == id && d.DeletedAt == null);

                if (document == null)
                {
                    return NotFound();
                }

                document.DeletedAt = DateTime.UtcNow; // Marca el documento como eliminado

                var indices = await _context.DocumentPageIndices
                    .Where(pi => pi.DocumentId == id)
                    .ToListAsync();

                _context.DocumentPageIndices.RemoveRange(indices);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return NoContent(); // No devuelve contenido al eliminar exitosamente
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = new[] { "Error al eliminar el documento" } });
            }
        }
    }

    // Clase para representar resultados paginados
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}