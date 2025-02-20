using Microsoft.AspNetCore.Mvc;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.Presentation.Extension.Resources;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class CreatePartialModelBase<TIAppService, TViewModel, TKey> : PagePartialModelBase<TIAppService, TViewModel, TKey>
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

        public CreatePartialModelBase(TIAppService appService, string? returnUrl = null, string? area = null, string? srcvalue = null, string? dstvalue = null, string? listId = null) : base(appService)
        {
            _area = area;
            _returnUrl = returnUrl;
            _srcvalues = srcvalue;
            _dstvalues = dstvalue;
            _listId = listId;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            GetRtl();
            Input = await GetViewModel();

            await InitPageAsync();

            return Page();
        }
        protected virtual async Task<TViewModel?> GetViewModel()
        {
            return await Task.FromResult<TViewModel?>(null);
        }
        protected virtual async Task InitPageAsync()
        {
            await Task.CompletedTask;
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            await PrepInputsync();

            if (!ModelState.IsValid)
            {
                
                await InitPageAsync();

                return Page();
            }

            try
            {
                Input = await _appService.CreateAsync(Input!);
            }
            catch (Exception e)
            {
                await InitPageAsync();

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
