using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class ListPartialModelBase<TIAppService, TViewModel, TKey> : PagePartialModelBase<TIAppService, TViewModel, TKey>
        where TIAppService : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
    {
        public PaginationViewModel<TViewModel>? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? SortAsc { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        public ListPartialModelBase(TIAppService appService, string sortBy, bool sortAsc = true, int pageSize = 10) : base(appService)
        {
            if (SortBy == null)
                SortBy = sortBy;
            if (SortAsc == null)
                SortAsc = sortAsc;
            if (PageSize == null)
                PageSize = pageSize;
        }
        public async virtual Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            if (PageIndex == null)
                PageIndex = pageIndex;
            GetRtl();
            Input = await GetViewModel(pageIndex);

            if (Input == null)
                return NotFound();

            await InitPageAsync();

            return Page();
        }
        protected virtual async Task<PaginationViewModel<TViewModel>?> GetViewModel(int? pageIndex)
        {
            return await _appService.SearchAsync(page: PageIndex ?? 1, pageSize: PageSize ?? 10, sortBy: SortBy, sortAsc: SortAsc ?? true, type: Type, searchby: SearchBy);
        }
        protected virtual async Task InitPageAsync()
        {
            await Task.CompletedTask;
        }
    }
}
