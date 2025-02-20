using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class DetailsModelBase<TIAppService, TViewModel, TKey> : PageModelBase<TIAppService, TViewModel, TKey>
        where TIAppService : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
    {
        [BindProperty]
        public TViewModel? Input { get; set; }

        public DetailsModelBase(TIAppService appService) : base(appService)
        {
        }

        public async Task<IActionResult> OnGetAsync(TKey id)
        {
            GetRtl();
            Input = await GetViewModel(id);

            if (Input == null)
                return NotFound();

            await InitPageAsync(id);

            BreadcrumbGenerator();

            return Page();
        }
        protected virtual async Task<TViewModel?> GetViewModel(TKey id)
        {
            return await _appService.GetDetailsAsync(id);
        }

        protected virtual async Task InitPageAsync(TKey id)
        {
            await Task.CompletedTask;
        }
    }
}
