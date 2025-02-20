using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.Presentation.Extension.Resources;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class EditModelBase<TIAppService, TViewModel, TKey> : PageModelBase<TIAppService, TViewModel, TKey>
        where TIAppService : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
    {
        [BindProperty]
        public TViewModel? Input { get; set; }
        private readonly string _returnUrl;
        private readonly string _area;
        private readonly string? _srcvalues;
        private readonly string? _dstvalues;

        public EditModelBase(TIAppService appService, string returnUrl, string area, string? srcvalue = null, string? dstvalue = null) : base(appService)
        {
            _area = area;
            _returnUrl = returnUrl;
            _srcvalues = srcvalue;
            _dstvalues = dstvalue;
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
            return await _appService.GetByIdAsync(id);
        }
        protected virtual async Task InitPageAsync(TKey id)
        {
            await Task.CompletedTask;
        }
        public virtual async Task<IActionResult> OnPostAsync(TKey id)
        {
            await PrepInputsync();

            if (!ModelState.IsValid)
            {
                Input = await _appService.GetByIdAsync(id);
                await InitPageAsync(id);
                BreadcrumbGenerator();

                return Page();
            }

            try
            {
                await _appService.ModifyAsync(id, Input!);
            }
            catch (Exception e)
            {
                Input = await _appService.GetByIdAsync(id);
                await InitPageAsync(id);
                BreadcrumbGenerator();

                var errorMsg = GlobalSharedResource.Error_Unknown;

                if (e.Message.Contains("duplicate", StringComparison.InvariantCultureIgnoreCase))
                {
                    errorMsg = GlobalSharedResource.Error_Duplicate;
                }

                ModelState.AddModelError("", errorMsg);

                return Page();
            }
            Dictionary<string, string> routevalue = new Dictionary<string, string>()
            {
                { "area",_area }
            };
            if (_dstvalues != null && _srcvalues != null)
                _srcvalues.Split(",").ToList().Select((v, i) => new { v, i }).ToList().ForEach(r => routevalue.Add(_dstvalues.Split(",")[r.i], Request.RouteValues[_srcvalues.Split(",")[r.i]]!.ToString()!));

            return RedirectToPage(_returnUrl, routevalue);
        }
        protected virtual async Task PrepInputsync()
        {
            ModelState.Clear();
            TryValidateModel(Input!);
            await Task.CompletedTask;
        }
    }
}
