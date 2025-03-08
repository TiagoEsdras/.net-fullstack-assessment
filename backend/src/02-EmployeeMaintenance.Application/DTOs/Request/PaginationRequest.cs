namespace EmployeeMaintenance.Application.DTOs.Request
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;

        public const int MaxPageSize = 50;

        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}