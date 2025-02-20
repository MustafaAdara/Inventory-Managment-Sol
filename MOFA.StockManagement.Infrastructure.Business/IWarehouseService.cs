using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IWarehouseService : IServiceBase<Warehouse, WarehouseModel, Guid>
    {
        public Task<PaginationModel<WarehouseModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true);
    }
    public class WarehouseService : ServiceBase<Warehouse, WarehouseModel, Guid>, IWarehouseService
    {
        private readonly ITrackableRepository<Warehouse, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public WarehouseService(
            ITrackableRepository<Warehouse, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginationModel<WarehouseModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            try
            {

                var totalCount = await _repository.CountSearchAsync(searchBy);
                var entities = await _repository.SelectSearchAsync(searchBy, page, sortBy, pageSize, sortAsc);

                var result = _mapper.Map<IEnumerable<WarehouseModel>>(entities);
                var paginationModel = new PaginationModel<WarehouseModel>()
                {
                    CurrentPage = page,
                    Data = (IList<WarehouseModel>)result,
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

    }
}
