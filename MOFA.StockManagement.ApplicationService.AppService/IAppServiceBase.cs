using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.ApplicationService.AppService.Extension;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IAppServiceBase<TViewModel, TKey> where TViewModel : ViewModelBase<TKey>
    {
        Task<TViewModel?> GetByIdAsync(TKey id);
        Task<TViewModel?> GetDetailsAsync(TKey id);
        Task<IList<TViewModel>?> GetAsync();
        Task<PaginationViewModel<TViewModel>?> SearchAsync(int page, string sortBy, int pageSize = 10, bool sortAsc = true, string? type = null, string? searchby = null);
        Task<PaginationViewModel<TViewModel>?> ReportResultAsync<TFactors>(TFactors factors, int page, string sortBy, int pageSize = 10, bool sortAsc = true);
        Task<TViewModel?> CreateAsync(TViewModel model);
        Task ModifyAsync(TKey id, TViewModel model);
        Task RemoveAsync(TKey id);
    }

    public class AppServiceBase<TViewModel, TKey> : IAppServiceBase<TViewModel, TKey>
        where TViewModel : ViewModelBase<TKey>
    {
        private readonly IHttpClientHelper<TViewModel> _helper;
        protected readonly string BaseUri;

        public AppServiceBase(IHttpClientHelper<TViewModel> helper, string baseUri)
        {
            _helper = helper;
            BaseUri = baseUri;
        }

        #region Public Methods

        public virtual Task<TViewModel?> GetByIdAsync(TKey? id)
        {
            return _helper.FindAsync($"{BaseUri}/{id}");
        }
        public virtual Task<TViewModel?> GetDetailsAsync(TKey? id)
        {
            return _helper.FindAsync($"{BaseUri}/Details/{id}");
        }
        public virtual Task<IList<TViewModel>?> GetAsync()
        {
            return _helper.SelectAsync($"{BaseUri}");
        }
        public virtual Task<PaginationViewModel<TViewModel>?> SearchAsync(int page, string sortBy, int pageSize = 10, bool sortAsc = true, string? type = null, string? searchby = null)
        {
            return _helper.FindAsync<PaginationViewModel<TViewModel>>($"{BaseUri}/Page/{page}/{pageSize}/{sortBy}/{(sortAsc ? "true" : "false")}?type={type ?? ""}&searchBy={searchby ?? ""}");
        }
        public virtual Task<PaginationViewModel<TViewModel>?> ReportResultAsync<TFactors>(TFactors factors, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            return _helper.FindAsync<PaginationViewModel<TViewModel>, TFactors>($"{BaseUri}/Report/{page}/{pageSize}/{sortBy}/{(sortAsc ? "true" : "false")}", factors);
        }
        public virtual Task<TViewModel?> CreateAsync(TViewModel model)
        {
            return _helper.AddAsync($"{BaseUri}", model);
        }
        public virtual Task ModifyAsync(TKey? id, TViewModel model)
        {
            return _helper.ModifyAsync($"{BaseUri}/{id}", model);
        }
        public virtual Task RemoveAsync(TKey? id)
        {
            return _helper.DeleteAsync($"{BaseUri}/{id}");
        }

        #endregion
    }
}
