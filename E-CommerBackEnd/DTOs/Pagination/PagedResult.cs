namespace E_Commerce.DTOs.Pagination
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
