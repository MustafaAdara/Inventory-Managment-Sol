using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Services;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Infrastructure.Business.Mapper;
using MOFA.StockManagement.Infrastructure.Queries;
using MOFA.StockManagement.Domain.Patterns.Services;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IServiceBase<TEntity, TModel, in TKey> : IService<TEntity, DbContextBase>
        where TEntity : EntityBase<TKey>
        where TModel : ModelEntityBase<TKey>
        where TKey : notnull
    {
        Task<TModel> AddAsync(TModel model);
        Task<TModel> ModifyAsync(TModel model);
        Task ModifyAsync(TKey id, Action<TEntity> func);

        Task<bool> RemoveAsync(TKey id);

        Task<IEnumerable<TModel>> SelectAsync();
        Task<TModel?> SelectByIdAsync(TKey id);
        Task<TModel?> DetailsAsync(TKey id);
        Task<TModel?> FirstOrDefaultAsync();
        Task<PaginationModel<TModel>> SelectPaginationAsync(int page, string sortBy, int pageSize = 10, bool sortAsc = true);
        Task<PaginationModel<TModel>> SelectPaginationSearchAsync(string type, string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true);

    }

    public class ServiceBase<TEntity, TModel, TKey> : Service<TEntity, DbContextBase>, IDisposable, IServiceBase<TEntity, TModel, TKey> where TEntity : EntityBase<TKey> where TModel : ModelEntityBase<TKey> where TKey : notnull
    {
        private readonly ITrackableRepository<TEntity, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;

        protected ServiceBase(ITrackableRepository<TEntity, DbContextBase> repository,
            IMapper mapper,
            IUnitOfWork<DbContextBase> unitOfWork) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            try
            {
                _repository.Insert(entity);

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TModel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TModel> ModifyAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            try
            {
                _repository.Update(entity);

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TModel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task ModifyAsync(TKey id, Action<TEntity> func)
        {
            try
            {
                await _repository.ModifyAsync(id, func);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public virtual async Task<bool> RemoveAsync(TKey id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Task<IEnumerable<TModel>> SelectAsync()
        {
            var entities = _repository.SelectAsync<TEntity, TKey, DbContextBase>();
            return _mapper.MapAsync<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        }

        public virtual Task<TModel?> SelectByIdAsync(TKey id)
        {
            var entity = _repository.SelectByIdAsync<TEntity, TKey, DbContextBase>(id);
            return _mapper.MapAsync<TEntity?, TModel?>(entity);
        }
        /// <summary>
        /// basic select to be overriden to include related entities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TModel?> DetailsAsync(TKey id)
        {
            var entity = _repository.SelectByIdAsync<TEntity, TKey, DbContextBase>(id);
            return _mapper.MapAsync<TEntity?, TModel?>(entity);
        }
        public virtual Task<TModel?> FirstOrDefaultAsync()
        {
            var entity = _repository.FirstOrDefaultAsync<TEntity, TKey, DbContextBase>();
            return _mapper.MapAsync<TEntity?, TModel?>(entity);
        }

        public virtual async Task<PaginationModel<TModel>> SelectPaginationAsync(int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            try
            {
                var totalCount = await _repository.CountAsync<TEntity, TKey, DbContextBase>();

                var entities = await _repository.SelectAsync<TEntity, TKey, DbContextBase>(page, sortBy, pageSize, sortAsc);

                var result = _mapper.Map<IEnumerable<TModel>>(entities);
                var paginationModel = new PaginationModel<TModel>()
                {
                    CurrentPage = page,
                    Data = (IList<TModel>)result,
                    PageSize = pageSize,
                    Count = totalCount
                };
                return paginationModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Task<PaginationModel<TModel>> SelectPaginationSearchAsync(string type, string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            System.GC.Collect();
            System.GC.SuppressFinalize(this);
        }
    }
}
