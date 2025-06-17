using System;

namespace KuantumLibraryApi.DTOs
{
    public class DocumentSearchDto
    {
        public int? Id { get; set; }
        public string? SerialCode { get; set; }
        public string? PublicationCode { get; set; }
        public string? Search { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}