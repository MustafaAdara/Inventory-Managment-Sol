using System.Text.Json.Serialization;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class PaginationViewModel<T>
    {
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("data")]
        public IList<T> Data { get; set; } = default!;

        [JsonIgnore]
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        [JsonIgnore]
        public bool ShowPrevious => CurrentPage > 1;

        [JsonIgnore]
        public bool ShowNext => CurrentPage < TotalPages;
    }
}
