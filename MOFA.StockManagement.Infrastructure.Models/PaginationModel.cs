namespace MOFA.StockManagement.Infrastructure.Models
{
    public class PaginationModel<T>
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IList<T> Data { get; set; } = new List<T>();

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
    }
}
