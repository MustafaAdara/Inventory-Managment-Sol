using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IStockBalanceService : IServiceBase<StockBalance, StockBalanceModel, Guid>
    {
    }
    public class StockBalanceService : ServiceBase<StockBalance, StockBalanceModel, Guid>, IStockBalanceService
    {
        private readonly ITrackableRepository<StockBalance, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public StockBalanceService(
            ITrackableRepository<StockBalance, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



    }
}
