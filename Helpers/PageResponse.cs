namespace ProductValidation.Helpers
{
    public class PageResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }

        public PageResponse(List<T> data, int count, int pageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}