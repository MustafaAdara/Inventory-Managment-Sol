using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class IndexModelBase<TIAppService, TViewModel, TKey> : PageModelBase<TIAppService, TViewModel, TKey>
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

        public IndexModelBase(TIAppService appService, string sortBy, bool sortAsc = true) : base(appService)
        {
            if (SortBy == null)
                SortBy = sortBy;
            if (SortAsc == null)
                SortAsc = sortAsc;
        }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            GetRtl();
            Input = await GetViewModel(pageIndex);

            if (Input == null)
                return NotFound();

            await InitPageAsync();

            BreadcrumbGenerator();

            return Page();
        }
        protected virtual async Task<PaginationViewModel<TViewModel>?> GetViewModel(int? pageIndex)
        {
            return await _appService.SearchAsync(page: pageIndex ?? 1, sortBy: SortBy, sortAsc: SortAsc ?? true, type: Type, searchby: SearchBy);
        }

        protected virtual async Task InitPageAsync()
        {
            await Task.CompletedTask;
        }
    }
}
