using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IItemUnitOfMeasureService : IServiceBase<ItemUnitOfMeasure, ItemUnitOfMeasureModel, Guid>
    {
    }
    public class ItemUnitOfMeasureService : ServiceBase<ItemUnitOfMeasure, ItemUnitOfMeasureModel, Guid>, IItemUnitOfMeasureService
    {
        private readonly ITrackableRepository<ItemUnitOfMeasure, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public ItemUnitOfMeasureService(
            ITrackableRepository<ItemUnitOfMeasure, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



    }
}
