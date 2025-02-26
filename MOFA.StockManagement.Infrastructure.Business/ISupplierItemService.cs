using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface ISupplierItemService : IServiceBase<SupplierItem, SupplierItemModel, Guid>
    {
    }
    public class SupplierItemService : ServiceBase<SupplierItem, SupplierItemModel, Guid>, ISupplierItemService
    {
        private readonly ITrackableRepository<SupplierItem, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public SupplierItemService(
            ITrackableRepository<SupplierItem, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



    }
}
