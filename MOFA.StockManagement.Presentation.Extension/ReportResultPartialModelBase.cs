using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class ReportResultPartialModelBase<TIAppService, TViewModel, TKey, TFactors> : PagePartialModelBase<TIAppService, TViewModel, TKey>
        where TIAppService : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
        where TFactors : class
    {
        public PaginationViewModel<TViewModel>? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? SortAsc { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        public string _factorsDataName;

        public ReportResultPartialModelBase(TIAppService appService, string factorsDataName, string sortBy, bool sortAsc = true, int pageSize = 10) : base(appService)
        {
            _factorsDataName = factorsDataName;
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
            if (TempData.Keys.Contains(_factorsDataName))
            {
                TFactors factors = System.Text.Json.JsonSerializer.Deserialize<TFactors>((string)TempData[_factorsDataName]);
                TempData.Keep(_factorsDataName);
                Input = await GetViewModel(factors, pageIndex);

                if (Input == null)
                    return NotFound();
            }
            else Input = new PaginationViewModel<TViewModel>() { Data = new List<TViewModel>() { } };

            await InitPageAsync();

            return Page();
        }
        protected virtual async Task<PaginationViewModel<TViewModel>?> GetViewModel(TFactors factors, int? pageIndex)
        {
            var report = await _appService.ReportResultAsync(factors, page: PageIndex ?? 1, pageSize: PageSize ?? 10, sortBy: SortBy, sortAsc: SortAsc ?? true);
            return report;
        }
        protected virtual async Task<PaginationViewModel<TViewModel>?> GetPage(int pageIndex)
        {
            TFactors factors = System.Text.Json.JsonSerializer.Deserialize<TFactors>((string)TempData[_factorsDataName]);
            TempData.Keep(_factorsDataName);
            return await _appService.ReportResultAsync(factors, page: pageIndex, pageSize: PageSize ?? 10, sortBy: SortBy, sortAsc: SortAsc ?? true);
        }
        protected virtual async Task InitPageAsync()
        {
            await Task.CompletedTask;
        }
    }
}
