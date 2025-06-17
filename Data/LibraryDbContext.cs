using KuantumLibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KuantumLibraryApi.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentPageIndex> DocumentPageIndices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración del filtro de consulta para soft delete
            modelBuilder.Entity<Document>()
                .HasQueryFilter(d => d.DeletedAt == null);

            // Configuración de la relación
            modelBuilder.Entity<Document>()
                .HasMany(d => d.PageIndices)
                .WithOne(p => p.Document)
                .HasForeignKey(p => p.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            // Aplicar snake_case a todas las entidades
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

    public static class StringExtensions
    {
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