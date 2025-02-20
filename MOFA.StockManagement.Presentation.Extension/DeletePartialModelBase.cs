using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.Presentation.Extension.Resources;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class DeletePartialModelBase<TIAppService, TViewModel, TKey> : PagePartialModelBase<TIAppService, TViewModel, TKey>
        where TIAppService : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
    {
        [BindProperty]
        public TViewModel? Input { get; set; }
        private readonly string? _returnUrl;
        private readonly string? _area;
        private readonly string? _srcvalues;
        private readonly string? _dstvalues;
        private readonly string? _listId;

        public DeletePartialModelBase(TIAppService appService, string? returnUrl = null, string? area = null, string? srcvalue = null, string? dstvalue = null, string? listId = null) : base(appService)
        {
            _area = area;
            _returnUrl = returnUrl;
            _srcvalues = srcvalue;
            _dstvalues = dstvalue;
            _listId = listId;
        }
        public async Task<IActionResult> OnGetAsync(TKey id)
        {
            GetRtl();
            Input = await GetViewModel(id);

            if (Input == null)
                return NotFound();

            await InitPageAsync(id);

            return Page();
        }
        protected virtual async Task<TViewModel?> GetViewModel(TKey id)
        {
            var fa = await _appService.GetByIdAsync(id);
            return fa;
        }
        protected virtual async Task InitPageAsync(TKey id)
        {
            await Task.CompletedTask;
        }
        public virtual async Task<IActionResult> OnPostAsync(TKey id)
        {
            await PrepInputsync();
            try
            {
                await _appService.RemoveAsync(id);
            }
            catch (Exception e)
            {
                Input = await _appService.GetByIdAsync(id);
                await InitPageAsync(id);

                var errorMsg = GlobalSharedResource.Error_Unknown;

                if (e.Message.Contains("duplicate", StringComparison.InvariantCultureIgnoreCase))
                {
                    errorMsg = GlobalSharedResource.Error_Duplicate;
                }

                ModelState.AddModelError("", errorMsg);

                return Page();
            }
            if (_area != null && _returnUrl != null)
            {
                Dictionary<string, string> routevalue = new Dictionary<string, string>()
                {
                    { "area",_area }
                };
                if (_dstvalues != null && _srcvalues != null)
                    _srcvalues.Split(",").ToList().Select((v, i) => new { v, i }).ToList().ForEach(r => routevalue.Add(_dstvalues.Split(",")[r.i], Request.RouteValues[_srcvalues.Split(",")[r.i]]!.ToString()!));

                return Content(Url.Page(_returnUrl, routevalue)!);
            }
            else
            {
                return Content(_listId!);
            }
        }
        protected virtual async Task PrepInputsync()
        {
            ModelState.Clear();
            TryValidateModel(Input!);
            await Task.CompletedTask;
        }
    }
}
