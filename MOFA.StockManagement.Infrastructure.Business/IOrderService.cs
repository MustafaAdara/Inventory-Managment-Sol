using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IOrderService : IServiceBase<Order, OrderModel, Guid>
    {
        public Task<PaginationModel<OrderModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true);
    }
    public class OrderService : ServiceBase<Order, OrderModel, Guid>, IOrderService
    {
        private readonly ITrackableRepository<Order, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public OrderService(
            ITrackableRepository<Order, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginationModel<OrderModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            try
            {

                var totalCount = await _repository.CountSearchAsync(searchBy);
                var entities = await _repository.SelectSearchAsync(searchBy, page, sortBy, pageSize, sortAsc);

                var result = _mapper.Map<IEnumerable<OrderModel>>(entities);
                var paginationModel = new PaginationModel<OrderModel>()
                {
                    CurrentPage = page,
                    Data = (IList<OrderModel>)result,
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
