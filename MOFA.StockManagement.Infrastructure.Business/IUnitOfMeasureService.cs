using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IUnitOfMeasureService : IServiceBase<UnitOfMeasure, UnitOfMeasureModel, Guid>
    {
        public Task<PaginationModel<UnitOfMeasureModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true);
    }
    public class UnitOfMeasureService : ServiceBase<UnitOfMeasure, UnitOfMeasureModel, Guid>, IUnitOfMeasureService
    {
        private readonly ITrackableRepository<UnitOfMeasure, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _UnitOfWork;
        public UnitOfMeasureService(
            ITrackableRepository<UnitOfMeasure, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> UnitOfWork)
                            : base(repository, mapper, UnitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _UnitOfWork = UnitOfWork;
        }


        public async Task<PaginationModel<UnitOfMeasureModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            try
            {

                var totalCount = await _repository.CountSearchAsync(searchBy);
                var entities = await _repository.SelectSearchAsync(searchBy, page, sortBy, pageSize, sortAsc);

                var result = _mapper.Map<IEnumerable<UnitOfMeasureModel>>(entities);
                var paginationModel = new PaginationModel<UnitOfMeasureModel>()
                {
                    CurrentPage = page,
                    Data = (IList<UnitOfMeasureModel>)result,
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
