using AutoMapper;
using MOFA.StockManagement.Api.Services.Controllers.Base;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Infrastructure.Models;

namespace MOFA.StockManagement.Api.Services.Controllers
{
    public class WarehousesController : ControllerBase<IWarehouseService, Warehouse, WarehouseModel, Guid>
    {
        private readonly IWarehouseService _service;
        private readonly IMapper _mapper;
        public WarehousesController(IWarehouseService service, LinkGenerator linkGenerator, IMapper mapper) : base(service, linkGenerator, mapper)
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
