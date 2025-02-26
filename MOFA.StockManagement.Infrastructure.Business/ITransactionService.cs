using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface ITransactionService : IServiceBase<Transaction, TransactionModel, Guid>
    {
    }
    public class TransactionService : ServiceBase<Transaction, TransactionModel, Guid>, ITransactionService
    {
        private readonly ITrackableRepository<Transaction, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public TransactionService(
            ITrackableRepository<Transaction, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



    }
}
