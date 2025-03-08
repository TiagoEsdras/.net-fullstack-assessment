namespace EmployeeMaintenance.Application.DTOs.Request
{
    public class PaginationResponse
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public static PaginationResponse PaginationInfo(PaginationRequest paginationRequest, int totalItems, int totalPages)
        {
            return new PaginationResponse
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = paginationRequest.PageSize,
                CurrentPage = paginationRequest.PageNumber
            };
        }
    }
}