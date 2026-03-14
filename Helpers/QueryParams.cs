namespace ProductValidation.Helpers
{
    public class QueryParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;

        public string? FilterBy { get; set; }

        public string? FilterValue { get; set; }
    }
}