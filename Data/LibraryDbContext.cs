using KuantumLibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KuantumLibraryApi.Data
{
    // Define la clase del contexto de la base de datos que hereda de DbContext de Entity Framework Core.
    public class LibraryDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del DbContext.
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        
        // DbSet para la entidad Document, representa la tabla de documentos en la base de datos.
        public DbSet<Document> Documents { get; set; }
        // DbSet para la entidad DocumentPageIndex, representa la tabla de índices de página de los documentos.
        public DbSet<DocumentPageIndex> DocumentPageIndices { get; set; }

        // Método que se llama durante la creación del modelo de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración del filtro de consulta global para la entidad Document.
            // Esto asegura que las consultas a Documents solo devuelvan aquellos que no han sido eliminados lógicamente (DeletedAt es null).
            modelBuilder.Entity<Document>()
                .HasQueryFilter(d => d.DeletedAt == null);

            // Configuración de la relación uno a muchos entre Document y DocumentPageIndex.
            modelBuilder.Entity<Document>()
                .HasMany(d => d.PageIndices)
                .WithOne(p => p.Document)
                .HasForeignKey(p => p.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            // Itera sobre todas las entidades del modelo para aplicar la convención de nombres snake_case.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Convertir nombre de tabla a snake_case
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Convertir nombres de columnas a snake_case
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }
            }
        }
    }

    // Clase estática para métodos de extensión de cadenas.
    public static class StringExtensions
    {
        // Método de extensión para convertir una cadena de PascalCase o camelCase a snake_case.
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var builder = new StringBuilder();
            builder.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    builder.Append('_');
                    builder.Append(char.ToLower(input[i]));
                }
                else
                {
                    builder.Append(input[i]);
                }
            }

            return builder.ToString();
        }
    }
}